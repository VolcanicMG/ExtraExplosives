﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class DeliquidifierProjectile : ExplosiveProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deliquidifier");
		}

		public override void SafeSetDefaults()
		{
			pickPower = -2;	// Override for nondestruction
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
			//CreateExplosion(projectile.Center, 10);

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
						Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water; //Removes Liquid
						WorldGen.SquareTileFrame(xPosition, yPosition, true); //Updates Area
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
						updatedPosition = new Vector2(position.X - 226 / 2, position.Y - 226 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 226, 226, 159, 0f, 0f, 0, new Color(255, 255, 255), 4.210526f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(39, Main.LocalPlayer);
							dust.noLight = false;
						}
					}
					//------------
				}
			}
		}
	}
}

//Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water; //Breaks water instead of creating it
//Main.tile[(int)((position.X + i) / 16), (int)((position.Y + j) / 16)].liquid = 1;