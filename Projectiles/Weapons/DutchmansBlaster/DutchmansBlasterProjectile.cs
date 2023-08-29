namespace ExtraExplosives.Projectiles.Weapons.DutchmansBlaster
{
    public class DutchmansBlasterProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc { get; } = "n/a";
        protected override string goreName { get; } = "n/a";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cannon Ball");
        }

        public override void SafeSetDefaults()
        {
            Projectile.height = 10;
            Projectile.width = 10;
            Projectile.damage = 40;
            Projectile.knockBack = 2;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.aiStyle = 16;
            Projectile.timeLeft = 1000;
            Projectile.scale = 0.5f;
            Projectile.penetrate = 8;
        }
    }
}