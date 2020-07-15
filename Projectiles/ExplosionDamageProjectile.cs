using ExtraExplosives.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
	public class ExplosionDamageProjectile : ExplosiveProjectile	// Deprecated class, will be deleted
	{
		//Variables:
		internal static float DamageRadius;

		public override string Texture { get; } = "ExtraExplosives/Projectiles/InvisibleProjectile";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ExplosionDamage");
		}

		public override void SafeSetDefaults()
		{
			projectile.tileCollide = false;
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 5;
			projectile.Opacity = 0f;
			projectile.ranged = true;
			projectile.scale = DamageRadius; //DamageRadius
			//projectile.scale = 5;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)	
		{
			if (!crit && Main.player[projectile.owner].GetModPlayer<ExtraExplosivesPlayer>().CrossedWires &&
			    Main.rand.Next(5) == 0)
			{
				crit = true;
			}
			Main.NewText((int)((damage + Main.player[projectile.owner].EE().DamageBonus) * Main.player[projectile.owner].EE().DamageMulti));
			base.OnHitPlayer(target, (int)((damage + Main.player[projectile.owner].EE().DamageBonus) * Main.player[projectile.owner].EE().DamageMulti), crit);
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