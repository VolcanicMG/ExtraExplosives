using Microsoft.Xna.Framework;
using Terraria;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles.Weapons.TrashCannon
{
    //TODO Add 1-4 varients
    public class TrashCannonProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc { get; } = "n/a";    // TBD
        protected override string goreName { get; } = "n/a";         // TBD

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Garbage");    // Name used in death messages
            Main.projFrames[Projectile.type] = 12;
        }

        public override void SafeSetDefaults()
        {
            radius = 2;
            pickPower = -2;    // No blocks broken
            Projectile.height = 11;
            Projectile.width = 11;
            Projectile.aiStyle = 16;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.frame = Main.rand.Next(13);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            ExplosionTileDamage();
            ExplosionEntityDamage();
            ExplosionDust(radius, Projectile.Center, shake: false);
            base.Kill(timeLeft);
        } 
    }
}