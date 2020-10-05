using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
	public class TornadoBombProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "n/a";
		private Vector2 vector;
		private bool done;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornado");
			Main.projFrames[projectile.type] = 5;
		}

		public override void SafeSetDefaults()
		{
			IgnoreTrinkets = true;
			projectile.tileCollide = true;
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 860;
			projectile.damage = 20;
		}

		public override void AI()
		{
			projectile.rotation = 0;

			if (++projectile.frameCounter >= 2)
			{
				projectile.frameCounter = 0;
				//projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
				if (++projectile.frame >= 5)
				{
					projectile.frame = 0;
				}
			}

			//Main.NewText(projectile.ai[1]);

			if (projectile.ai[1] >= 300f && !done)
			{
				Main.PlaySound(16, (int)projectile.Center.X, (int)projectile.Center.Y, 19, 1f, 0f);

				if (projectile.ai[1] >= 1f && !done)
				{
					int num328 = Projectile.NewProjectile(projectile.Center.X - 49, projectile.Center.Y - 4f, (0f - (float)projectile.direction) * 0.01f, 0f, ModContent.ProjectileType<TornadoBombProjectileTornado>(), projectile.damage, projectile.knockBack, Main.myPlayer, 16f, 15f); //384 //376
					NetMessage.SendData(MessageID.SyncProjectile, number: num328);
					Main.projectile[num328].netUpdate = true;

					Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Tornado"));
				}
					
				done = true;
			}

			projectile.ai[1]++;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);
			int num324 = 36;
			for (int num325 = 0; num325 < num324; num325++)
			{
				Vector2 vector17 = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f;
				Vector2 spinningpoint26 = vector17;
				double radians26 = (double)((float)(num325 - (num324 / 2 - 1)) * 6.28318548f / (float)num324);
				vector = default(Vector2);
				vector17 = spinningpoint26.RotatedBy(radians26, vector) + projectile.Center;
				Vector2 vector18 = vector17 - projectile.Center;
				int num326 = Dust.NewDust(vector17 + vector18, 0, 0, 172, vector18.X * 2f, vector18.Y * 2f, 100, new Color(192, 192, 192), 1.4f);
				Main.dust[num326].noGravity = true;
				Main.dust[num326].noLight = true;
				Main.dust[num326].velocity = vector18;
			}
		}
	}
}