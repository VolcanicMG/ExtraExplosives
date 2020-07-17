namespace ExtraExplosives.Projectiles
{
    public class BombCloakProjectile : MediumExplosiveProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/InvisibleProjectile";

        public override void Explosion()
        {
        }
    }
}