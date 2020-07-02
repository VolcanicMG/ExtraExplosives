using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class LargeExplosiveProjectile : ModProjectile
	{
		private const int PickPower = 50;
		private const string gore = "Gores/Explosives/basic-explosive_gore";
		private LegacySoundStyle[] explodeSounds;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("LargeExplosive");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.width = 32;
			projectile.height = 38;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 400;
			explodeSounds = new LegacySoundStyle[3];
			for (int num = 1; num <= explodeSounds.Length; num++)
            {
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Explosives/Large_Explosive_" + num);
            }
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 500);

			//Create Bomb Damage
			ExplosionDamage(20f * 2f, projectile.Center, 450, 40, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 20);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(-2f, 0f);
			Vector2 gVel2 = new Vector2(0f, -2f);
			gVel1 = gVel1.RotatedBy(projectile.rotation);
			gVel2 = gVel2.RotatedBy(projectile.rotation);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1, mod.GetGoreSlot(gore + "1"), projectile.scale * 1.5f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2, mod.GetGoreSlot(gore + "2"), projectile.scale * 1.5f);
			gVel1 = gVel1.RotatedBy(Math.PI / 2);
			gVel2 = gVel2.RotatedBy(Math.PI / 2);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1, mod.GetGoreSlot(gore + "1"), projectile.scale * 1.5f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2, mod.GetGoreSlot(gore + "2"), projectile.scale * 1.5f);
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
					if (Main.rand.NextFloat() < 0.2f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 10f)];
						dust.noGravity = true;
						dust.fadeIn = 2.5f;
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.2f)
					{
						updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 203, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.2f)
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