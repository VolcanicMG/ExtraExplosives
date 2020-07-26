using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class HomingRocketProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "n/a";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Follow Rocket");
			Main.projFrames[projectile.type] = 3;
		}

		public override void SafeSetDefaults()
		{
			radius = 4;
			pickPower = 0;
			projectile.tileCollide = true;
			projectile.width = 46;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 20000;
			projectile.ranged = true;
			projectile.scale = 0.9f;
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
			//anim
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				//projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
				if (++projectile.frame >= 3)
				{
					projectile.frame = 0;
				}
			}

			//dust
			float num248 = 0f;
			float num249 = 0f;

			Vector2 position71 = new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f;
			int width67 = projectile.width - 8;
			int height67 = projectile.height - 8;
		 	Color newColor = default(Color);
			int num250 = Dust.NewDust(position71, width67, height67, 6, 0f, 0f, 100, newColor, 1f);
			Dust dust3 = Main.dust[num250];
			dust3.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
			dust3 = Main.dust[num250];
			dust3.velocity *= 0.2f;
			Main.dust[num250].noGravity = true;
			Vector2 position72 = new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f;
			int width68 = projectile.width - 8;
			int height68 = projectile.height - 8;
			newColor = default(Color);
			num250 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, newColor, 0.5f);
			Main.dust[num250].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
			dust3 = Main.dust[num250];
			dust3.velocity *= 0.05f;


			//Actual AI code
			float num132 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
			float num133 = projectile.localAI[0];
			if (num133 == 0f)
			{
				projectile.localAI[0] = num132;
				num133 = num132;
			}
			float num134 = projectile.position.X;
			float num135 = projectile.position.Y;
			float num136 = 600f;
			bool flag3 = false;
			int num137 = 0;
			if (projectile.ai[1] == 0f)
			{
				for (int num138 = 0; num138 < 200; num138++)
				{
					if (Main.npc[num138].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num138 + 1)))
					{
						float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2);
						float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
						float num141 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num139) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num140);
						if (num141 < num136 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
						{
							num136 = num141;
							num134 = num139;
							num135 = num140;
							flag3 = true;
							num137 = num138;
						}
					}
				}
				if (flag3)
				{
					projectile.ai[1] = (float)(num137 + 1);
				}
				flag3 = false;
			}
			if (projectile.ai[1] > 0f)
			{
				int num142 = (int)(projectile.ai[1] - 1f);
				if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
				{
					float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
					float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
					if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num143) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num144) < 1000f)
					{
						flag3 = true;
						num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
						num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
					}
				}
				else
				{
					projectile.ai[1] = 0f;
				}
			}
			if (!projectile.friendly)
			{
				flag3 = false;
			}
			if (flag3)
			{
				float num145 = num133;
				Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num146 = num134 - vector10.X;
				float num147 = num135 - vector10.Y;
				float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
				num148 = num145 / num148;
				num146 *= num148;
				num147 *= num148;
				int num149 = 8;
				projectile.velocity.X = (projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
				projectile.velocity.Y = (projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
			}

			projectile.rotation = projectile.velocity.ToRotation();
			//projectile.spriteDirection = projectile.direction;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(10f, projectile.Center, projectile.damage, 20f, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 2);

			projectile.knockBack = 20;	// Since no calling item exists, knockback must be set internally	(Set in Hellfire Rocket Battery)
			ExplosionDamage();
			
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
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.5f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.48f)
					{
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 0.8f)
					{
						updatedPosition = new Vector2(position.X - 180 / 2, position.Y - 180 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 180, 180, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
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