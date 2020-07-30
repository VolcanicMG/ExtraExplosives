using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.TrashCannon
{
    public class TrashCannonProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc { get; } = "n/a";
        protected override string goreFileLoc { get; } = "n/a";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trash Cannon Projectile");
            Main.projFrames[projectile.type] = 12;
        }

        public override void SafeSetDefaults()
        {
            radius = 3;
            pickPower = -2;
            projectile.height = 22;
            projectile.width = 22;
            projectile.aiStyle = 0;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.frame = Main.rand.Next(13);
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
    }
}