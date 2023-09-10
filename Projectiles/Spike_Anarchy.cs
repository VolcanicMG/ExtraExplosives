using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class Spike_Anarchy : ModProjectile    // This is only used by CE as an attack, ignoring
    {
        public override void SetDefaults()
        {
            Projectile.tileCollide = true;
            Projectile.width = 18;
            Projectile.height = 14;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 100;
            Projectile.damage = 20;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void Kill(int timeLeft)
        {
            Dust dust;
            dust = Main.dust[Terraria.Dust.NewDust(Projectile.Center, 2, 2, 6, 0f, 0, 0, Scale: 1.5f)];
        }
    }
}