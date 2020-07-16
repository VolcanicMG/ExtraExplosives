using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class MegaExplosiveProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Mega_Explosive_";
		protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MegaExplosive");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 65;
			radius = 40;
			projectile.tileCollide = true;
			projectile.width = 32;
			projectile.height = 38;
			projectile.aiStyle = 16;
			projectile.friendly = false;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 500;
			explodeSounds = new LegacySoundStyle[2];
			for (int num = 1; num <= explodeSounds.Length; num++)
            {
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

			/* ===== ABOUT THE BOMB SOUND =====
			 * 
			 * Because the KillTile() and KillWall() methods used in CreateExplosion()
			 * produce a lot of sounds, the bomb's own explosion sound is difficult to
			 * hear. The solution to eliminate those unnecessary sounds is to alter
			 * the fields of each Tile that the explosion affects, but this creates
			 * additional problems (no dropped Tile items, adjacent Tiles not updating
			 * their sprites, etc). I've decided to ignore doing the changes because
			 * it would entail making the same changes to multiple projectiles and the
			 * projectile template.
			 * 
			 * -- V8_Ninja
			 */

			//Create Bomb Dust
			CreateDust(projectile.Center, 800);

			Explosion();
			ExplosionDamage();

			//Create Bomb Damage
			//ExplosionDamage(40f * 1.5f, projectile.Center, 600, 70, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 40);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(4.0f, 0.0f);
			Vector2 gVel2 = new Vector2(0.0f, -4.0f);
			gVel1 = gVel1.RotatedBy(projectile.rotation);
			gVel2 = gVel2.RotatedBy(projectile.rotation);
			for (int num = 0; num < 4; num++)
			{
				Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1, mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale * 1.5f);
				Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2, mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale * 1.5f);
				gVel1 = gVel1.RotatedBy(Math.PI / 4);
				gVel2 = gVel2.RotatedBy(Math.PI / 4);
			}
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
							WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
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
						updatedPosition = new Vector2(position.X - 900 / 2, position.Y - 900 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 900, 900, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 15f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.486842f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 900 / 2, position.Y - 900 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 900, 900, 203, 0f, 0f, 0, new Color(255, 255, 255), 15f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 900 / 2, position.Y - 900 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 900, 900, 31, 0f, 0f, 0, new Color(255, 255, 255), 15f)];
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