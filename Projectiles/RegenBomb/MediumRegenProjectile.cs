namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class MediumRegenProjectile : RegenBombProjectile
    {

        public override void RegenDefaults()
        {
            projectile.extraUpdates = 10;
            velocity = 0.001f;
            SetRadius(10);
        }
    }
}