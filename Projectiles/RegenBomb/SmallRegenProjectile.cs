namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class SmallRegenProjectile : RegenBombProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/SmallExplosiveProjectile";

        public override void SafeSetDefaults()
        {
            base.SafeSetDefaults();
            projectile.extraUpdates = 5;
            SetRadius(5);
        }
    }
}