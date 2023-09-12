using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class CritterBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "critter_gore";

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            radius = 10;
            Projectile.tileCollide = true;
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.damage = 0;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 10);

            //Create Bomb Dust
            CreateDust(Projectile.Center, 50);

            ExplosionTileDamage();
            ExplosionEntityDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(3f, 3f);
            Vector2 gVel2 = new Vector2(-3f, -3f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }

        public override void ExplosionTileDamage()
        {
            Vector2 position = Projectile.Center;
            int spread = 0;
            int pick = 0;
            int[] variety = { 442, 443, 445, 446, 447, 448, 539, 444 }; //442:GoldenBird - 443:GoldenBunny - 445:GoldenFrog - 446:GoldenGrasshopper - 447:GoldenMouse - 539:GoldenSquirrel - 448:GoldenWorm - 444:GoldenButterfly

            for (int i = 0; i <= radius; i++)
            {
                spread = Main.rand.Next(1200); //Random spread

                pick = variety[Main.rand.Next(variety.Length)];

                NPC.NewNPC(Projectile.GetSource_FromThis(), (int)position.X + (spread - 600), (int)position.Y, pick, 0, 0f, 0f, 0f, 0f, 255); //Spawn
                spread = 0;
            }
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 600 / 2, position.Y - 100 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 600, 100, 1, 0f, 0f, 0, new Color(159, 255, 0), 1.776316f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 300) dust.active = false;
                        else
                        {
                            dust.noLight = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(112, Main.LocalPlayer);
                            dust.fadeIn = 1.697368f;
                        }
                    }
                    //------------
                }
            }
        }
    }
}