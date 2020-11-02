using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class BoomerangProjectile : ExplosiveProjectile		//TODO Recode so it works with the Bombedier class
	{
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "n/a";
		private bool HitSomeThing;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BOOMerang");
		}

		public override void DangerousSetDefaults()
		{
			radius = 5;
			projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
			projectile.damage = 46;
			projectile.melee = false;
			projectile.ranged = false;
			projectile.magic = false;
			projectile.thrown = false;
			projectile.minion = false;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.hostile = false;
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


			//Create Bomb Dust
			CreateDust(projectile.Center, 50);

			ExplosionDamage();

			//projectile.Kill();
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];

			if (Main.rand.NextFloat() < .2f && HitSomeThing == false)
			{
				//Create Bomb Sound
				Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

				//Create Bomb Damage
				if(!player.EE().BlastShielding &&
					!player.EE().BlastShieldingActive)
				{
					player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), projectile.direction);
				}

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
						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 6, 0f, 0.5f, 0, new Color(255, 0, 0), 4f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
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