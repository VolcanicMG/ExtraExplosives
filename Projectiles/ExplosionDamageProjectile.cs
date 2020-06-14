using ExtraExplosives.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
	public class ExplosionDamageProjectile : ModProjectile
	{
		//Variables:
		internal static float DamageRadius;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ExplosionDamage");
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = false;
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 5;
			projectile.Opacity = 0f;
			projectile.scale = DamageRadius; //DamageRadius
			//projectile.scale = 5;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (ExtraExplosives.NukeHit == true)
			{
				target.AddBuff(ModContent.BuffType<RadiatedDebuff>(), 5000);
			}
			return true;
		}
	}
}