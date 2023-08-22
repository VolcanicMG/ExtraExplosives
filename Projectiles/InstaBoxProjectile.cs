using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class InstaBoxProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InstaBoxProjectile");
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 0;
            Projectile.damage = 0;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)Projectile.Center.X, (int)Projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(Projectile.Center, 4);

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

                    if (WorldGen.TileEmpty(xPosition, yPosition)) //Runs when a tile is empty
                    {
                        if (x == -radius || x == radius) //Left and Right
                        {
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.Dirt);
                        }
                        if (y == -radius || y == radius) //Top and Bottom
                        {
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.Dirt);
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
                        if (Vector2.Distance(dust.position, Projectile.position) > 50) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 0f;
                            dust.noLight = true;
                        }
                    }
                    //------------
                }
            }
        }
    }
}