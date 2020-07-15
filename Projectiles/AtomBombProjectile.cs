using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class AtomBombProjectile : ExplosiveProjectile
	{
		private const string gore = "Gores/Explosives/atom_gore";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atom Bomb");
		}

		public override void SafeSetDefaults()
		{
			pickPower = -1;	// Can destroy anything
			radius = 1;
			projectile.tileCollide = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;

			drawOffsetX = -15;
			drawOriginOffsetY = -15;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			//ExplosionDamage(1, projectile.Center, 5000, 1.0f, projectile.owner);

			//Create Bomb Explosion
			//CreateExplosion(projectile.Bottom, 40);
			
			Explosion();
			
			ExplosionDamage();
			
			//Create Bomb Dust
			CreateDust(projectile.Center, 30);
			//CreateDust(projectile.Center, 30);
		}

		public override void Explosion()	// Special (more efficient) explosion, leaving it
		{
			int xPosition = (int)(projectile.Bottom.X / 16.0f);
			int yPosition = (int)(projectile.Bottom.Y / 16.0f);
			WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this make the explosion destroy tiles
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
						updatedPosition = new Vector2(position.X - 1 / 2, position.Y - 1 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 1, 1, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 15f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.486842f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 1 / 2, position.Y - 1 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 1, 1, 203, 0f, 0f, 0, new Color(255, 255, 255), 15f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
					//------------

					//---Dust 3---
					if (Main.rand.NextFloat() < 1f)
					{
						updatedPosition = new Vector2(position.X - 1 / 2, position.Y - 1 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 1, 1, 31, 0f, 0f, 0, new Color(255, 255, 255), 15f)];
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