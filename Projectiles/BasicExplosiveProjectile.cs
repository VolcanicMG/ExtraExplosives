using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class BasicExplosiveProjectile : ExplosiveProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BasicExplosive");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 40;
			radius = 3;
			projectile.tileCollide = true;
			projectile.width = 26;
			projectile.height = 22;
			projectile.aiStyle = 16;
			projectile.friendly = false;
			projectile.hostile = false;
			//projectile.penetrate = -1;
			projectile.timeLeft = 150;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 10);

			//Create Bomb Damage
			ExplosionDamage();

			//Create Bomb Explosion
			Explosion();
		}

		private void CreateDust(Vector2 position, int amount)
		{
			Dust dust;
			Vector2 updatedPosition;

			for (int i = 0; i <= amount; i++)
			{
				if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
				{
					//Dust 1
					if (Main.rand.NextFloat() < 0.9f)
					{
						//updatedPosition = new Vector2(position.X, position.Y);

						updatedPosition = projectile.Center;
						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.5f;
						}
					}

					//Dust 2
					if (Main.rand.NextFloat() < 0.6f)
					{
						//updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);

						updatedPosition = projectile.Center;
						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}

					//Dust 3
					if (Main.rand.NextFloat() < 0.3f)
					{
						//updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
						updatedPosition = projectile.Center;
						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
				}
			}
		}
	}
}