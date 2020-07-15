using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class HydromiteProjectile : ExplosiveProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hydromite");
		}

		public override void SafeSetDefaults()
		{
			radius = 10;
			projectile.tileCollide = true;
			projectile.width = 10;
			projectile.height = 32;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 100;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			Explosion();

			//Create Bomb Dust
			CreateDust(projectile.Center, 100);
		}

		public override void Explosion()
		{
			Vector2 position = projectile.Center;
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
					{
						if (WorldGen.TileEmpty((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f)))
						{
							Main.tile[xPosition, yPosition].liquidType(0);
							Main.tile[xPosition, yPosition].liquid = 128;
							WorldGen.SquareTileFrame(xPosition, yPosition, true);
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
						updatedPosition = new Vector2(position.X - 168 / 2, position.Y - 168 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 168, 168, 188, 0.2631581f, 0f, 0, new Color(0, 42, 255), 3.815789f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
						}
					}
					//------------
				}
			}
		}
	}
}