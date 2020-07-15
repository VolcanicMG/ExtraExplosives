using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class SmallExplosiveProjectile : ExplosiveProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SmallExplosive");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 35;
			radius = 5;
			projectile.tileCollide = true;
			projectile.width = 26;
			projectile.height = 28;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 200;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			CreateDust(projectile.Center, 15);
			
			Explosion();
			
			ExplosionDamage();
			//Create Bomb Damage
			//ExplosionDamage(5f * 2f, projectile.Center, 120, 25, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 5);

			//Create Bomb Dust
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
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.5f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.486842f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.48f)
					{
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 203, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.8f)
					{
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
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