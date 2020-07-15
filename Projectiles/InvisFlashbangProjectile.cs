using ExtraExplosives.Buffs;
using ExtraExplosives.Items.Explosives;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
	internal class InvisFlashbangProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("InvisFlashbangProjectile");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = false;
			projectile.width = 10;
			projectile.height = 20;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 10;
			projectile.Opacity = 0f;
			projectile.scale = 45 * 2; //DamageRadius
		}

		public override string Texture => "ExtraExplosives/Projectiles/ExplosionDamageProjectile";

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Confused, 300);
			target.AddBuff(BuffID.Confused, 300);
			target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);

			base.OnHitNPC(target, damage, knockback, crit);
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			//Main.NewText("Knockback " + projectile.knockBack);
			//Main.NewText("Direction " + FlashbangItem.Direction);
			//Main.NewText("PlayerDirection " + target.direction);
			if (target.whoAmI == projectile.owner)
			{
				//Applying debuffs
				target.AddBuff(BuffID.Confused, 300);
				target.AddBuff(BuffID.Dazed, 300);
				target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);

				//Direction testing
				if (target.direction == 1 && FlashbangItem.Direction == 1 && projectile.knockBack == 0) //left side
				{
					//Main.NewText("Hit on the left");
				}
				if (target.direction == 1 && FlashbangItem.Direction == -1 && projectile.knockBack == 0) //left side
				{
					//Main.NewText("Hit on the left");
				}
				if (target.direction == -1 && FlashbangItem.Direction == -1 && projectile.knockBack >= 1) //right side
				{
					//Main.NewText("Hit on the right");
				}
				if (target.direction == -1 && FlashbangItem.Direction == 1 && projectile.knockBack >= 1) //right side
				{
					//Main.NewText("Hit on the right");
				}
			}

			base.OnHitPlayer(target, damage, crit);
		}
	}
}