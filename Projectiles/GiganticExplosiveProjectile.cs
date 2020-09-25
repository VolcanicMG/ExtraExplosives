using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class GiganticExplosiveProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Gigantic_Explosion_";
		protected override string goreFileLoc => "Gores/Explosives/gigantic-explosive_gore";
		private LegacySoundStyle fuseSound;
		private bool fusePlayed = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("GiganticExplosive");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 70;
			radius = 80;
			projectile.tileCollide = true;
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 800;
			//projectile.scale = 1.5f;
			fuseSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + "Wick");
			if (!Main.dedServ)
            {
				fuseSound = fuseSound.WithVolume(0.5f);
			}
			explodeSounds = new LegacySoundStyle[2];
			for (int num = 1; num <= explodeSounds.Length; num++)
            {
				explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
		}

		public override void AI()
		{
			if (!fusePlayed)
			{
				Main.PlaySound(fuseSound, (int) projectile.Center.X, (int) projectile.Center.Y);
				fusePlayed = true;
			}
			projectile.rotation = 0;
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
			//CreateDust(projectile.Center, 300);

			
			Explosion();
			ExplosionDamage();
			CreateDust(projectile.Center, 1000);
			//Create Bomb Damage
			//ExplosionDamage(80f * 1.5f, projectile.Center, 1000, 100, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 80);

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(0f, 3f);
			Vector2 gVel2 = new Vector2(-3f, -3f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
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
						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

						dust = Main.dust[Dust.NewDust(updatedPosition, radius * 16, radius * 16, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 5)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
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
						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

						dust = Main.dust[Dust.NewDust(updatedPosition, radius * 16, radius * 16, 203, 0f, 0f, 0, new Color(255, 255, 255), 5)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
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
						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

						dust = Main.dust[Dust.NewDust(updatedPosition, radius * 16, radius * 16, 31, 0f, 0f, 0, new Color(255, 255, 255), 5)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
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