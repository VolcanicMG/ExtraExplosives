using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class FollowRocketProjectile : ModProjectile
	{
		private const int PickPower = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Follow Rocket");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 20000;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();

			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.Kill();
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			projectile.Kill();
		}

		public override void AI()
		{

			Vector2 position38 = new Vector2(projectile.position.X, projectile.position.Y);
			int width35 = projectile.width / 2 / 2 / 2;
			int height35 = projectile.height / 2 / 2 / 2;
			float speedX17 = projectile.velocity.X * 0.2f;
			float speedY17 = projectile.velocity.Y * 0.2f;
			Color newColor = default(Color);
			int num110 = Dust.NewDust(position38, width35, height35, 6, speedX17, speedY17, 100, newColor, 3.5f);
			Main.dust[num110].noGravity = true;
			Dust dust3 = Main.dust[num110];
			dust3.velocity *= 1.4f;
			Vector2 position39 = new Vector2(projectile.position.X, projectile.position.Y);
			int width36 = projectile.width;
			int height36 = projectile.height;
			float speedX18 = projectile.velocity.X * 0.2f;
			float speedY18 = projectile.velocity.Y * 0.2f;
			newColor = default(Color);
			num110 = Dust.NewDust(position39, width36, height36, 6, speedX18, speedY18, 100, newColor, 1.5f);


			if (Main.myPlayer == projectile.owner && projectile.ai[0] <= 0f)
			{
				if (Main.player[projectile.owner].channel)
				{
					float num114 = 12f;

					Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num115 = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
					float num116 = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
					if (Main.player[projectile.owner].gravDir == -1f)
					{
						num116 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
					}
					float num117 = (float)Math.Sqrt((double)(num115 * num115 + num116 * num116));
					num117 = (float)Math.Sqrt((double)(num115 * num115 + num116 * num116));
					if (projectile.ai[0] < 0f)
					{
						projectile.ai[0] = projectile.ai[0];
						projectile.ai[0] += 1f;
					}

					else if (num117 > num114)
					{
						num117 = num114 / num117;
						num115 *= num117;
						num116 *= num117;
						int num118 = (int)(num115 * 1000f);
						int num119 = (int)(projectile.velocity.X * 1000f);
						int num120 = (int)(num116 * 1000f);
						int num121 = (int)(projectile.velocity.Y * 1000f);
						if (num118 != num119 || num120 != num121)
						{
							projectile.netUpdate = true;
						}

						else
						{
							projectile.velocity.X = num115;
							projectile.velocity.Y = num116;
						}
					}
					else
					{
						int num122 = (int)(num115 * 1000f);
						int num123 = (int)(projectile.velocity.X * 1000f);
						int num124 = (int)(num116 * 1000f);
						int num125 = (int)(projectile.velocity.Y * 1000f);
						if (num122 != num123 || num124 != num125)
						{
							projectile.netUpdate = true;
						}
						projectile.velocity.X = num115;
						projectile.velocity.Y = num116;
					}
				}
				else if (projectile.ai[0] <= 0f)
				{
					projectile.netUpdate = true;
					float num126 = 12f;
					Vector2 vector11 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num127 = (float)Main.mouseX + Main.screenPosition.X - vector11.X;
					float num128 = (float)Main.mouseY + Main.screenPosition.Y - vector11.Y;
					if (Main.player[projectile.owner].gravDir == -1f)
					{
						num128 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector11.Y;
					}
					float num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
					if (num129 == 0f || projectile.ai[0] < 0f)
					{
						vector11 = new Vector2(Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2), Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2));
						num127 = projectile.position.X + (float)projectile.width * 0.5f - vector11.X;
						num128 = projectile.position.Y + (float)projectile.height * 0.5f - vector11.Y;
						num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
					}
					num129 = num126 / num129;
					num127 *= num129;
					num128 *= num129;
					projectile.velocity.X = num127;
					projectile.velocity.Y = num128;
					if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
					{
						projectile.Kill();
					}

					projectile.ai[0] = 1f;
				}
			}

			projectile.rotation += 0.3f * (float)projectile.direction;

			if (projectile.velocity.X != 0f || projectile.velocity.Y != 0f)
			{
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - 2.355f;
			}
			if (projectile.velocity.Y > 16f)
			{
				projectile.velocity.Y = 16f;
			}

		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			ExplosionDamage(10f, projectile.Center, projectile.damage, 20f, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 2);

			//Create Bomb Dust
			CreateDust(projectile.Center, 100);
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
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
						dust.noGravity = true;
						dust.fadeIn = 2.5f;
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.48f)
					{
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.8f)
					{
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
					//------------
				}
			}
		}
	}
}