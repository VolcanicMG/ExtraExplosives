namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class LargeRegenProjectile : RegenBombProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/LargeExplosiveProjectile";

        public override void SafeSetDefaults()
        {
            base.SafeSetDefaults();
            projectile.extraUpdates = 20;
            SetRadius(20);
        }
    }
}