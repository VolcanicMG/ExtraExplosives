using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class HeavyBombProjectile : ModProjectile
	{
		private const int PickPower = 50;
		private const string gore = "Gores/Explosives/heavy_gore";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("HeavyBomb");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.width = 13;
			projectile.height = 19;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1000;
		}

		public override bool OnTileCollide(Vector2 old)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item37, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			ExplosionDamage(2f * 2f, projectile.Center, 30, 40, projectile.owner);

			//Create Bomb Explosion
			Vector2 position = projectile.Center;
			int radius = 2;

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
							WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
						}
					}
				}
			}

			//Create Bomb Dust
			for (int i = 0; i < 10; i++)
			{
				if (Main.rand.NextFloat() < DustAmount)
				{
					Dust dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X - (30 / 2), position.Y - 0), 30, 30, 1, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
				}
			}

			return true;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 500);

			//Create Bomb Damage
			ExplosionDamage(20f * 1.5f, projectile.Center, 500, 40, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 20);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(0f, 2f);
			Vector2 gVel2 = new Vector2(-2f, 2f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(gore + "2"), projectile.scale);
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
					if (Main.rand.NextFloat() < 0.5f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 10f)];
						dust.noGravity = true;
						dust.fadeIn = 2.486842f;
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.5f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 203, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.5f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 31, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------
				}
			}
		}
	}
}