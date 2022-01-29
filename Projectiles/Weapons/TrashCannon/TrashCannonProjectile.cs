using Microsoft.Xna.Framework;
using Terraria;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles.Weapons.TrashCannon
{
    //TODO Add 1-4 varients
    public class TrashCannonProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc { get; } = "n/a";    // TBD
        protected override string goreFileLoc { get; } = "n/a";         // TBD

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Garbage");    // Name used in death messages
            Main.projFrames[projectile.type] = 12;
        }

        public override void SafeSetDefaults()
        {
            radius = 2;
            pickPower = -2;    // No blocks broken
            projectile.height = 11;
            projectile.width = 11;
            projectile.aiStyle = 16;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.frame = Main.rand.Next(13);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Explosion();
            ExplosionDamage();
            ExplosionDust(radius, projectile.Center, shake: false);
            base.Kill(timeLeft);
        } 
    }
}