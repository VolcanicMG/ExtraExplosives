using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	internal class BunnyiteProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bunnyite");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.width = 10;
			projectile.height = 32;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 80;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			CreateExplosion(projectile.Center, 2);

			//Create Bomb Dust
			CreateDust(projectile.Center, 50);
		}

		private void CreateExplosion(Vector2 position, int radius)
		{
			int bunnies = 200;

			for (int x = 0; x < bunnies; x++)
			{
				NPC.NewNPC((int)position.X + Main.rand.Next(1000) - 500, (int)position.Y, NPCID.Bunny, 0, 0f, 0f, 0f, 0f, 255); //Spawn
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
						updatedPosition = new Vector2(position.X - 800 / 2, position.Y - 100 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 800, 100, 112, 0f, 0f, 0, new Color(255, 0, 0), 1.447368f)];
						dust.shader = GameShaders.Armor.GetSecondaryShader(36, Main.LocalPlayer);
						dust.fadeIn = 1.144737f;
					}
					//------------
				}
			}
		}
	}
}