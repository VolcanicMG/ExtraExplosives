using ExtraExplosives.Buffs;
using ExtraExplosives.Items.Explosives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
			DisplayName.SetDefault("InvisFlashbangProjectile");
		}

		public override void SafeSetDefaults()
		{
			radius = 90;
			projectile.tileCollide = true;
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

		public override void Kill(int timeLeft)
		{
			ExplosionDamage();
		}

		public override string Texture => "ExtraExplosives/Projectiles/FlashbangProjectile";

		/*public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
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
		}*/

		public override void ExplosionDamage()
		{
			if (Main.player[projectile.owner].EE().ExplosiveCrit > Main.rand.Next(1, 101)) crit = true;
			foreach (NPC npc in Main.npc)
			{
				float dist = Vector2.Distance(npc.Center, projectile.Center);
				if (dist/16f <= radius)
				{
					npc.AddBuff(BuffID.Confused, 300);
					npc.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
				}
			}

			foreach (Player player in Main.player)
			{
				Main.NewText(player.whoAmI);
				if (player == null || player.whoAmI == 255 || !player.active) continue;
				if (!CanHitPlayer(player)) continue;
				if (player.EE().BlastShielding &&
				    player.EE().BlastShieldingActive) continue;
				float dist = Vector2.Distance(player.Center, projectile.Center);
				if (dist/16f <= radius)
				{
					player.AddBuff(BuffID.Confused, 300);
					player.AddBuff(BuffID.Dazed, 300);
					player.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);	
				}
				if (Main.netMode != 0)
				{
				}
			}
		}
	}
}