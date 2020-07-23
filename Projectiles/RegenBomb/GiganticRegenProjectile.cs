namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class GiganticRegenProjectile : RegenBombProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/GiganticExplosiveProjectile";

        public override void SafeSetDefaults()
        {
            base.SafeSetDefaults();
            projectile.extraUpdates = 80;
            SetRadius(80);
        }
    }
}