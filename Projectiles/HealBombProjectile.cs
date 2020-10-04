using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class HealBombProjectile : ExplosiveProjectile
	{
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heal Bomb");
		}

		public override void SafeSetDefaults()
		{
			pickPower = 0;
			radius = 20;
			projectile.tileCollide = true;
			projectile.width = 26;
			projectile.height = 22;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			//projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 150;
			projectile.damage = 0;
			projectile.knockBack = 0;
		}

		public override void Kill(int timeLeft)
		{
			//Create Bomb Sound
			Main.PlaySound(SoundID.DD2_DarkMageCastHeal, (int)projectile.Center.X, (int)projectile.Center.Y);

			//Create Bomb Dust
			CreateDust(projectile.Center, radius + 50);

			//Create Bomb Damage
			ExplosionDamage();

			//Create Bomb Gore
			Vector2 gVel1 = new Vector2(-1f, 0f);
			Vector2 gVel2 = new Vector2(0f, -1f);
			Gore.NewGore(projectile.position, gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
			Gore.NewGore(projectile.position, gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
		}

		public override void ExplosionDamage()
		{
			foreach (Player player in Main.player)
			{
				if (player == null || player.whoAmI == 255 || !player.active) return;
				if (!CanHitPlayer(player)) continue;
				float dist = Vector2.Distance(player.Center, projectile.Center);
				int dir = (dist > 0) ? 1 : -1;
				if (dist / 16f <= radius && Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.player[player.whoAmI].HealEffect(25, true);
				}
			}
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
						//updatedPosition = new Vector2(position.X, position.Y);

						updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);
						dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 8, 0f, 0.5263162f, 0, new Color(255, 0, 50), 4.539474f)];
						if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
						else
						{
							dust.shader = GameShaders.Armor.GetSecondaryShader(91, Main.LocalPlayer);
							dust.noGravity = true;
							dust.fadeIn = 2.5f;
						}

					}
				}
			}
		}
	}
}