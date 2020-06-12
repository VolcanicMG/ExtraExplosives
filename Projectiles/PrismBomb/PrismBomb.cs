using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace ExtraExplosives.Projectiles.PrismBomb
{
    public class PrismBomb : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;

        }
        public override void AI()
        {
            projectile.velocity.Y += 0.2f;
            projectile.rotation += 0.1f;

            if (projectile.velocity.Y < -10) { projectile.velocity.Y = -10; }
            else if (projectile.velocity.Y > 10) { projectile.velocity.Y = 10; }

            }
        public override void Kill(int timeleft)
        { Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("PrismBombPrism"), projectile.damage, 0f, projectile.owner);
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("PrismExplosion"), projectile.damage, 0f, projectile.owner);
        }


    }
}
