using ExtraExplosives.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class ExplosionDamageProjectile : ExplosiveProjectile    // Deprecated class, will be deleted
    {
        //Variables:
        internal static float DamageRadius;
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        public override string Texture { get; } = "ExtraExplosives/Projectiles/InvisibleProjectile";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ExplosionDamage");
        }

        public override void SafeSetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
            Projectile.Opacity = 0f;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.scale = DamageRadius; //DamageRadius
                                             //projectile.scale = 5;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (!crit && Main.player[Projectile.owner].GetModPlayer<ExtraExplosivesPlayer>().CrossedWires &&
                Main.rand.Next(5) == 0)
            {
                crit = true;
            }
            Main.NewText((int)((damage + Main.player[Projectile.owner].EE().DamageBonus) * Main.player[Projectile.owner].EE().DamageMulti));
            base.OnHitPlayer(target, (int)((damage + Main.player[Projectile.owner].EE().DamageBonus) * Main.player[Projectile.owner].EE().DamageMulti), crit);
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