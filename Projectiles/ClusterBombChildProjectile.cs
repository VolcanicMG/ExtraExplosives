using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class ClusterBombChildProjectile : ModProjectile
	{
		private const int PickPower = 50;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ClusterBomb");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 100;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 20);

			//Create Bomb Damage
			ExplosionDamage(9f * 2.0f, projectile.Center, 200, 40, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 9);
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
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles
							//if (Main.rand.Next(60) == 1) Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.Next(20) - 10, Main.rand.Next(20) - 10, mod.ProjectileType("SmallExplosiveProjectile"), 0, 0, projectile.owner, 0.0f, 0);
							//Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
							if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
							WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
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
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
						dust.noGravity = true;
						dust.fadeIn = 2.486842f;
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.48f)
					{
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.8f)
					{
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------
				}
			}
		}
	}
}