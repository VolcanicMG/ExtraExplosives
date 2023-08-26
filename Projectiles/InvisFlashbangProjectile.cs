using ExtraExplosives.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class InvisFlashbangProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("InvisFlashbangProjectile");
        }

        public override void SafeSetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 10;
            Projectile.Opacity = 0f;
            Projectile.scale = 45 * 2; //DamageRadius
            Projectile.tileCollide = false;
        }

        public override void DangerousSetDefaults()
        {
            Projectile.friendly = true; // have to put these here because they are set to false by the explosive projectile for various reasons
            Projectile.hostile = true;
        }

        public override string Texture => "ExtraExplosives/Projectiles/FlashbangProjectile";

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Confused, 300);
            target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            //Main.NewText($"{target.direction} {projectile.Center.X} {target.Center.X} {projectile.Center.X - target.Center.X}");
            if (target.whoAmI == Projectile.owner && target.direction * (Projectile.Center.X - target.Center.X) > 0)
            {
                target.AddBuff(BuffID.Confused, 300);
                target.AddBuff(BuffID.Dazed, 300);
                target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
            }
            
            if (target.direction * (Projectile.Center.X - target.Center.X) > 0 && info.PvP)
            {
	            target.AddBuff(BuffID.Confused, 300);
	            target.AddBuff(BuffID.Dazed, 300);
	            target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
            }
        }

        /*public override void Kill(int timeLeft)		// Old code left in just in case
		{
			//ExplosionDamage();
		}

		public override string Texture => "ExtraExplosives/Projectiles/FlashbangProjectile";

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
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
				if (projectile.knockBack == 0)
				{
					
				}
				/*if (target.direction == 1 && FlashbangItem.Direction == 1 && projectile.knockBack == 0) //left side total 2
				{
					target.AddBuff(BuffID.Confused, 300);
					target.AddBuff(BuffID.Dazed, 300);
					target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
					//Main.NewText("Hit on the left");
				}

				if (target.direction == 1 && FlashbangItem.Direction == -1 && projectile.knockBack == 0) //left side total 0
				{
					target.AddBuff(BuffID.Confused, 300);
					target.AddBuff(BuffID.Dazed, 300);
					target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
					//Main.NewText("Hit on the left");
				}

				if (target.direction == -1 && FlashbangItem.Direction == -1 && projectile.knockBack >= 1) //right side total -1
				{
					target.AddBuff(BuffID.Confused, 300);
					target.AddBuff(BuffID.Dazed, 300);
					target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
					//Main.NewText("Hit on the right");
				}

				if (target.direction == -1 && FlashbangItem.Direction == 1 && projectile.knockBack >= 1) //right side total 1
				{
					target.AddBuff(BuffID.Confused, 300);
					target.AddBuff(BuffID.Dazed, 300);
					target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
					//Main.NewText("Hit on the right");
				}
			}

			base.OnHitPlayer(target, damage, crit);
		}*/
    }
}