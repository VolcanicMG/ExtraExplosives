namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class LargeRegenProjectile : RegenBombProjectile
    {

        public override void RegenDefaults()
        {
            projectile.extraUpdates = 10;
            velocity = 0.0005f;
            SetRadius(20);
        }
    }
}