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
        protected override string goreFileLoc => "";
        private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Leveler");
            //Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            pickPower = -2;
            radius = 30;
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 10;   //This defines the hitbox width
            projectile.height = 10; //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 80; //The amount of time the projectile is alive for
            projectile.damage = 0;

            drawOffsetX = -15;
            drawOriginOffsetY = -15;
            explodeSounds = new LegacySoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
        }

        public override void AI()
        {
            projectile.rotation = 0;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

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
            Explosion();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(4.0f, 4.0f);
            Vector2 gVel2 = new Vector2(0.0f, -4.0f);
            Vector2 gVel3 = new Vector2(-4.0f, 0.0f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel3), gVel3.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
        }

        public override void Explosion()
        {
            Player player = Main.player[projectile.owner];
            Vector2 position = new Vector2(projectile.Center.X / 16f, projectile.Center.Y / 16f);    // Converts to tile cords for convenience

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