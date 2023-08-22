using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.PrismBomb
{
    public class PrismExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.height = 117;
            Projectile.width = 92;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;

            Projectile.penetrate = -1;
            Main.projFrames[Projectile.type] = 5;
            Projectile.ai[1] = 0;

            Projectile.damage = 100;
            Projectile.alpha = 160;
        }

        public override void AI()
        {
            Projectile.ai[1] += 1;
            if (Projectile.ai[1] % 5 == 0)
            {
                if (Projectile.frame == 4) { Projectile.active = false; }
                else
                {
                    Projectile.alpha -= 30;
                    Projectile.frame += 1;
                }
            }
        }
    }
}