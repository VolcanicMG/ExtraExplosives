namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class MediumRegenProjectile : RegenBombProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/MediumExplosiveProjectile";

        public override void SafeSetDefaults()
        {
            base.SafeSetDefaults();
            projectile.extraUpdates = 10;
            SetRadius(10);
        }
    }
}