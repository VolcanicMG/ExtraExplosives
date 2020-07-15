using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class BoomerangProjectile : ExplosiveProjectile		//TODO Recode so it works with the Bombedier class
	{
		private bool HitSomeThing;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BOOMerang");
		}

		public override void SafeSetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
			projectile.damage = 46;
			projectile.friendly = true;
			aiType = ProjectileID.EnchantedBoomerang;
		}

		//public override bool OnTileCollide(Vector2 oldVelocity)
		//{
		//	projectile.Kill();
		//	return base.OnTileCollide(oldVelocity);
		//}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			HitSomeThing = true;

			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			ExplosionDamage();

			//Create Bomb Dust
			CreateDust(projectile.Center, 10);

			//projectile.Kill();
		}

		public override void Kill(int timeLeft)
		{
			if (Main.rand.NextFloat() < .2f && HitSomeThing == false)
			{
				//Create Bomb Sound
				Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

				//Create Bomb Damage
				ExplosionDamage();

				//Create Bomb Dust
				CreateDust(projectile.Center, 10);

				projectile.Kill();
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
						updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 6, 0f, 0.5f, 0, new Color(255, 0, 0), 4f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 0f;
							dust.noLight = true;
						}
					}
					//------------
				}
			}
		}
	}
}