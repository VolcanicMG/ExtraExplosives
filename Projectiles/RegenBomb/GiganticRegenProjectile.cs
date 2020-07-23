namespace ExtraExplosives.Projectiles.RegenBomb
{
    public class GiganticRegenProjectile : RegenBombProjectile
    {

        public override void RegenDefaults()
        {
            projectile.extraUpdates = 80;
            velocity = 0.000125f;     // yes it does have to be this small
            SetRadius(80);
        }
    }
}