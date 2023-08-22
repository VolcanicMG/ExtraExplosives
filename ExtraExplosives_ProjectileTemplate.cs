using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class ExtraExplosives_ProjectileTemplate : ModProjectile
    {
        private const int PickPower = 0;
        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile"; //DELETE THIS LINE AFTER YOU COPY/PASTE ###################################

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ExtraExplosives_ProjectileTemplate");
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)Projectile.Center.X, (int)Projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(5f, Projectile.Center, 70, 20, Projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(Projectile.Center, 2);

            //Create Bomb Dust
            CreateDust(Projectile.Center, 10);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        ushort tile = Main.tile[xPosition, yPosition].TileType;
                        if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            //-----===THIS IS WHERE THE BOMBS UNIQUE CODE GOES===-----\\

                            //-----===########################################===-----\\
                        }
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
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 6, 0f, 0.5f, 0, new Color(255, 0, 0), 4f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0f;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
    }
}