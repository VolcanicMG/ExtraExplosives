using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    internal class NPCProjectile : ModProjectile    // This is only used by CE as an attack, ignoring
    {
        private const int PickPower = 0;
        private int Radius = 6;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NPCProjectile");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 10;
            projectile.height = 32;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100;
            projectile.damage = 150;
        }

        public override void AI()
        {
            if (projectile.timeLeft <= 3)
            {
                projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                projectile.position = projectile.Center;
                //projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                //projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = 250;
                projectile.height = 250;
                projectile.Center = projectile.position;
                //projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                //projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                projectile.damage = 150;
                projectile.knockBack = 10f;
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f * 2f, projectile.Center, 150, 40, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 5);

            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 10;
            projectile.height = 10;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            //Create Bomb Dust
            ExplosionDust(Radius, projectile.Center, new Color(255, 255, 255), new Color(189, 24, 22), 1);
        }

        /*private void CreateExplosion(Vector2 position, int radius)
		{
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
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
    }
}