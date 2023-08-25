using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class ClusterBombChildProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ClusterBomb");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 9;
            Projectile.tileCollide = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Dust
            DustEffects();

            Explosion();

            ExplosionDamage();

        }
    }
}