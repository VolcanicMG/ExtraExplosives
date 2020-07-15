using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class C4Projectile : ExplosiveProjectile
	{
		//Variables:
		private bool freeze;

		private Vector2 positionToFreeze;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("C4");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 70;
			radius = 2;
			projectile.tileCollide = true;
			projectile.width = 32;
			projectile.height = 40;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = Int32.MaxValue;
			//projectile.extraUpdates = 1;
		}

		public override bool OnTileCollide(Vector2 old)
		{
			if (!freeze)
			{
				freeze = true;
				positionToFreeze = new Vector2(projectile.position.X, projectile.position.Y);
				projectile.width = 64;
				projectile.height = 40;
				projectile.position.X = positionToFreeze.X;
				projectile.position.Y = positionToFreeze.Y;
				projectile.velocity.X = 0;
				projectile.velocity.Y = 0;
				//projectile.rotation = 0;
			}
			
			return true;
		}

		public override void PostAI()
		{
			if (projectile.owner == Main.myPlayer)
			{
				var player = Main.player[projectile.owner].GetModPlayer<ExtraExplosivesPlayer>();

				if (player.detonate == true)
				{
					projectile.Kill();
				}
			}

			if (freeze == true)
			{
				projectile.position.X = positionToFreeze.X;
				projectile.position.Y = positionToFreeze.Y;
				projectile.velocity.X = 0;
				projectile.velocity.Y = 0;
			}
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 550);
			
			Explosion();
			ExplosionDamage();
		}

		public override void Explosion()
		{
			if (Main.player[projectile.owner].EE().BombardEmblem) return;
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
						updatedPosition = new Vector2(position.X - 360 / 2, position.Y - 360 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 360, 360, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
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
						updatedPosition = new Vector2(position.X - 642 / 2, position.Y - 642 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 642, 642, 56, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(91, Main.LocalPlayer);
						}
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 560 / 2, position.Y - 560 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 560, 560, 6, 0f, 0.5263162f, 0, new Color(255, 150, 0), 5f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
							dust.fadeIn = 3f;
						}
					}
					//------------

					//---Dust 4---
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 157 / 2, position.Y - 157 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 157, 157, 55, 0f, 0f, 0, new Color(255, 100, 0), 3.552631f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
						}
					}
					//------------
				}
			}
		}
	}
}