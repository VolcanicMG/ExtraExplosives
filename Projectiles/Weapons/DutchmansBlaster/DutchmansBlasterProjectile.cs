namespace ExtraExplosives.Projectiles.Weapons.DutchmansBlaster
{
    public class DutchmansBlasterProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc { get; } = "n/a";
        protected override string goreFileLoc { get; } = "n/a";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cannon Ball");
        }

        public override void SafeSetDefaults()
        {
            projectile.height = 10;
            projectile.width = 10;
            projectile.damage = 40;
            projectile.knockBack = 2;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.aiStyle = 16;
            projectile.timeLeft = 1000;
            projectile.scale = 0.5f;
            projectile.penetrate = 8;
        }
    }
}