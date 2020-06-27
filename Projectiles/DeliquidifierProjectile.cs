using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class DeliquidifierProjectile : ModProjectile
	{
		private const int PickPower = 0;
		private const string gore = "Gores/Explosives/deliquifyer_gore";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deliquidifier");
		}

		public override void SetDefaults()
		{
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
			CreateExplosion(projectile.Center, 10);

			//Create Bomb Dust
			CreateDust(projectile.Center, 100);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(2f, 0f);
			Vector2 gVel2 = new Vector2(-2f, -2f);
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
						}

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
						dust.noGravity = true;
						dust.shader = GameShaders.Armor.GetSecondaryShader(39, Main.LocalPlayer);
						dust.noLight = false;
					}
					//------------
				}
			}
		}
	}
}

//Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water; //Breaks water instead of creating it
//Main.tile[(int)((position.X + i) / 16), (int)((position.Y + j) / 16)].liquid = 1;