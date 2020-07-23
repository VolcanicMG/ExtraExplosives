namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class BasicRegenProjectile : RegenBombProjectile
    {
        public override void RegenDefaults()
        {
            projectile.extraUpdates = 2;
            velocity = 0.005f;
            SetRadius(2);
        }
    }
}