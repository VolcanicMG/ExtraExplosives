using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss.BossProjectiles
{
	public class BossDazedBombProjectile : ModProjectile
	{
		private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
		private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dazed Bomb");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true; //checks to see if the projectile can go through tiles
			projectile.width = 28;   //This defines the hitbox width
			projectile.height = 44;	//This defines the hitbox height
			projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
			projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
			projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
			projectile.timeLeft = 150; //The amount of time the projectile is alive for
<<<<<<< HEAD
=======
			projectile.damage = 150;
>>>>>>> Charlie's-Uploads
		}

		public override void AI()
		{
			if (++projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}

			if (projectile.timeLeft <= 3)
			{
				projectile.hostile = true;
				projectile.friendly = false;
				projectile.tileCollide = false;
				// Set to transparent. This projectile technically lives as  transparent for about 3 frames
				projectile.alpha = 255;
				// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
				projectile.position = projectile.Center;
				//projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				//projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 450;
				projectile.height = 450;
				projectile.Center = projectile.position;
				//projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				//projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
				projectile.damage = 80;
				projectile.knockBack = 10f;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (projectile.timeLeft <= 3)
			{
				target.AddBuff(BuffID.Dazed, 500);
			}
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, 500);

			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 10;
			projectile.height = 10;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

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
						updatedPosition = new Vector2(position.X - 500 / 2, position.Y - 500 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 500, 500, 64, 0f, 0f, 0, new Color(255, 226, 0), 5f)];
						dust.noGravity = true;
						dust.fadeIn = 2.5f;

					}

					//Dust 2
					if (Main.rand.NextFloat() < 0.6f)
					{
						updatedPosition = new Vector2(position.X - 500 / 2, position.Y - 500 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 500, 500, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
						dust.noGravity = true;
						dust.noLight = true;
					}

					//Dust 3
					if (Main.rand.NextFloat() < 0.3f)
					{
						updatedPosition = new Vector2(position.X - 500 / 2, position.Y - 500 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 500, 500, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
						dust.noGravity = true;
						dust.noLight = true;
					}
				}
			}
		}
	}
}