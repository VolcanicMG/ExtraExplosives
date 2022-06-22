using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class TrollBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TrollBomb");
        }

        public override string Texture => "Terraria/Projectile_637";

        public override void SafeSetDefaults()
        {
            pickPower = 0;
            projectile.tileCollide = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2000;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity.X = 0;
            projectile.rotation = 0f;
            projectile.velocity.Y = -7;

            return false;
        }

        public override void PostAI()
        {
            projectile.velocity.X = 0;
            projectile.rotation = 0f;

            base.PostAI();
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 2);

            //Create Bomb Dust
            CreateDust(projectile.Center, 100);
        }

        /*private void CreateExplosion(Vector2 position, int radius)
		{
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
					{
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
						}
					}
				}
			}
		}*/

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
                        updatedPosition = new Vector2(position.X - 70 / 2, position.Y - 70 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 70, 70, 4, 0f, 0f, 154, new Color(255, 255, 255), 3.55f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > 35) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 0.2763158f;
                        }
                    }
                    //------------
                }
            }
        }
    }
}