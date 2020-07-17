namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class BasicRegenProjectile : RegenBombProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/BasicExplosiveProjectile";

        public override void SafeSetDefaults()
        {
            base.SafeSetDefaults();
            projectile.extraUpdates = 2;
            SetRadius(2);
        }
    }
}