namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class MegaRegenProjectile : RegenBombProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/MegaExplosiveProjectile";

        public override void SafeSetDefaults()
        {
            base.SafeSetDefaults();
            projectile.extraUpdates = 40;
            SetRadius(40);
        }
    }
}