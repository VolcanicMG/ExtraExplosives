using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	internal class CritterBombProjectile : ExplosiveProjectile
	{
		protected override string explodeSoundsLoc => "n/a";
		protected override string goreFileLoc => "Gores/Explosives/critter_gore";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CritterBomb");
		}

		public override void SafeSetDefaults()
		{
			radius = 10;
			projectile.tileCollide = true;
			projectile.width = 10;
			projectile.height = 32;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 100;
			projectile.damage = 0;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Center, 10);

			//Create Bomb Dust
			CreateDust(projectile.Center, 50);

			Explosion();
			ExplosionDamage();

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(3f, 3f);
			Vector2 gVel2 = new Vector2(-3f, -3f);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
			Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
		}

		public override void Explosion()
		{
			Vector2 position = projectile.Center;
			int spread = 0;
			int pick = 0;
			int[] variety = { 442, 443, 445, 446, 447, 448, 539, 444 }; //442:GoldenBird - 443:GoldenBunny - 445:GoldenFrog - 446:GoldenGrasshopper - 447:GoldenMouse - 539:GoldenSquirrel - 448:GoldenWorm - 444:GoldenButterfly

			for (int i = 0; i <= radius; i++)
			{
				spread = Main.rand.Next(1200); //Random spread

				pick = variety[Main.rand.Next(variety.Length)];

				NPC.NewNPC((int)position.X + (spread - 600), (int)position.Y, pick, 0, 0f, 0f, 0f, 0f, 255); //Spawn
				spread = 0;
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
						updatedPosition = new Vector2(position.X - 600 / 2, position.Y - 600 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 600, 100, 1, 0f, 0f, 0, new Color(159, 255, 0), 1.776316f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noLight = true;
							dust.shader = GameShaders.Armor.GetSecondaryShader(112, Main.LocalPlayer);
							dust.fadeIn = 1.697368f;
						}
					}
					//------------
				}
			}
		}
	}
}