namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class MegaRegenProjectile : RegenBombProjectile
    {

        public override void RegenDefaults()
        {
            projectile.extraUpdates = 35;
            velocity = 0.00025f;
            SetRadius(40);
        }
    }
}