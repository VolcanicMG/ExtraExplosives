using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class BiomeCleanerProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => null;
        protected override string goreName => null;

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            radius = 0;
            pickPower = 40;
            Projectile.tileCollide = true;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.damage = 0;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;
            explodeSounds = new SoundStyle[] {
                    new SoundStyle(explodeSoundsLoc)
            };
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Projectile.Kill();

            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 0);

            ExplosionTileDamage();

            //Create Bomb Dust
            CreateDust(Projectile.Center, 50);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-2f, 2f);
            Vector2 gVel2 = new Vector2(2f, -2f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }

        public override void ExplosionTileDamage()
        {
            Vector2 position = Projectile.Center;
            int width = 250; //Explosion Width
            int height = Main.maxTilesY; //Explosion Height

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            for (int x = -width; x < width; x++)
            {
                for (int y = -120; y <= height; y++)
                {
                    int i = (int)(x + position.X / 16.0f);
                    int j = (int)(y + position.Y / 16.0f);

                    if (WorldGen.InWorld(i, j))
                    {
                        WorldGen.Convert(i, j, 0, 2);
                        //NetMessage.SendTileSquare(-1, i, j, 1);
                    }
                }
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
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 0, 0f, 0f, 56, new Color(33, 0, 255), 5.0f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 5) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                        }
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 148, 0f, 0.2631581f, 34, new Color(255, 226, 0), 2.039474f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 5) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }
                    //------------
                }
            }
        }
    }
}