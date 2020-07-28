namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class SmallRegenProjectile : RegenBombProjectile
    {
        public override void RegenDefaults()
        {
            projectile.extraUpdates = 5;
            velocity = 0.002f;
            SetRadius(5);
        }
    }
}