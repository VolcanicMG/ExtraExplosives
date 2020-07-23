namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class MegaRegenProjectile : RegenBombProjectile
    {

        public override void RegenDefaults()
        {
            projectile.extraUpdates = 40;
            velocity = 0.00025f;
            SetRadius(40);
        }
    }
}