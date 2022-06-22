using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class Spike_Anarchy : ModProjectile    // This is only used by CE as an attack, ignoring
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spike_Anarchy");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 18;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 100;
            projectile.damage = 20;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override void Kill(int timeLeft)
        {
            Dust dust;
            dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 2, 2, 6, 0f, 0, 0, Scale: 1.5f)];
        }
    }
}