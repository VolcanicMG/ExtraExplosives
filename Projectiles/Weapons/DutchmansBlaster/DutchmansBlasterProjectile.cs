using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.DutchmansBlaster
{
    public class DutchmansBlasterProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc { get; } = "n/a";
        protected override string goreName { get; } = "n/a";

        public override void SafeSetDefaults()
        {
            //We want pierce so use the bullet
            AIType = ProjectileID.Bullet;

            radius = 3;
            pickPower = -2;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.height = 10;
            Projectile.width = 10;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 600;
            Projectile.scale = 1f;
            Projectile.penetrate = 8;
        }

        public override void Kill(int timeLeft)
        {
            ManualExplode(SoundID.Item14, autoKill: false);
        }
    }
}