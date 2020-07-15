using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class DaBombProjectile : ExplosiveProjectile
	{
		//Variables:
		public bool buffActive;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DaBomb");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 50;
			radius = 20;
			projectile.tileCollide = true;
			projectile.width = 22;
			projectile.height = 42;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = 20;
			projectile.timeLeft = 400;

			buffActive = true;
		}

		public override void PostAI()
		{
			Player player = Main.player[projectile.owner];

			if (buffActive == true)
			{
				player.AddBuff(mod.BuffType("ExtraExplosivesDaBombBuff"), 50, false);
			}

			base.PostAI();
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];

			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(player.Center, 400);

			//Create Bomb Damage
			//ExplosionDamage(20f * 1.5f, player.Center, 400, 200, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(player.Center, 20);

			Explosion();
			ExplosionDamage();
			
			//Disables the debuff
			buffActive = false;
		}

		/*public override void Explosion()
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
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							if (CanBreakTiles) //User preferences dictates if this bomb can break tiles
							{
								WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
								if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
							}
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
					if (Main.rand.NextFloat() < 0.2f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 10f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.486842f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.2f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 203, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.2f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 31, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//------------
				}
			}
		}
	}
}