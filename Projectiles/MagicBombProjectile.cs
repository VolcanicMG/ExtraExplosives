using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class MagicBombProjectile : ExplosiveProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Bomb");
        }

        public override void SafeSetDefaults()
        {
            projectile.CloneDefaults(29);
            pickPower = 0;
            radius = 5;
        }
        public override void Kill(int timeLeft)
		{
			mod.Logger.DebugFormat("Damage {0}", projectile.damage);
			//Create Bomb Sound
			Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Damage
			ExplosionDamage();

			//Create Bomb Explosion
			Explosion();

			//Create Bomb Dust
			CreateDust(projectile.Center, 15);
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
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.5f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 16) dust.active = false;
						else
						{
							dust.noGravity = true;
							dust.fadeIn = 2.486842f;
						}
					}
					//------------

					//---Dust 2---
					if (Main.rand.NextFloat() < 0.48f)
					{
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 203, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
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
						updatedPosition = new Vector2(position.X - 120 / 2, position.Y - 120 / 2);

						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 120, 120, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
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