using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class WallBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "";
        protected override string goreName => "";
        //private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            pickPower = -2;
            radius = 30;
            Projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            Projectile.width = 10;   //This defines the hitbox width
            Projectile.height = 10; //This defines the hitbox height
            Projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 80; //The amount of time the projectile is alive for
            Projectile.damage = 0;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;
            explodeSounds = new SoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void AI()
        {
            Projectile.rotation = 0;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], Projectile.Center);

            /* ===== ABOUT THE BOMB SOUND =====
			 * 
			 * Because the KillTile() and KillWall() methods used in CreateExplosion()
			 * produce a lot of sounds, the bomb's own explosion sound is difficult to
			 * hear. The solution to eliminate those unnecessary sounds is to alter
			 * the fields of each Tile that the explosion affects, but this creates
			 * additional problems (no dropped Tile items, adjacent Tiles not updating
			 * their sprites, etc). I've decided to ignore doing the changes because
			 * it would entail making the same changes to multiple projectiles and the
			 * projectile template.
			 * 
			 * -- V8_Ninja
			 */

            //Create Bomb Explosion
            ExplosionTileDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(4.0f, 4.0f);
            Vector2 gVel2 = new Vector2(0.0f, -4.0f);
            Vector2 gVel3 = new Vector2(-4.0f, 0.0f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel3), gVel3.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }

        public override void ExplosionTileDamage()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 position = new Vector2(Projectile.Center.X / 16f, Projectile.Center.Y / 16f);    // Converts to tile cords for convenience

            radius = (int)((radius + player.EE().RadiusBonus) * player.EE().RadiusMulti);
            for (int x = -radius;
                x <= radius;
                x++)
            {
                //int x = (int)(i + position.X);
                for (int y = -radius;
                    y <= radius;
                    y++)
                {
                    //int y = (int)(j + position.Y);
                    int i = (int)(x + position.X);
                    int j = (int)(y + position.Y);
                    if (!WorldGen.InWorld(i, j)) continue;
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        Tile tile = Framing.GetTileSafely(i, j);

                        WorldGen.KillWall(i, j, false); //This destroys Walls
                    }
                }
            }
        }
    }
}