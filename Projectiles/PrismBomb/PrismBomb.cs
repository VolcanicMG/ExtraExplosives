using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.PrismBomb
{
    public class PrismBomb : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        public override void SafeSetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.ranged = true;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;
            Projectile.rotation += 0.1f;

            if (Projectile.velocity.Y < -10) { Projectile.velocity.Y = -10; }
            else if (Projectile.velocity.Y > 10) { Projectile.velocity.Y = 10; }
        }

        public override void Kill(int timeleft)
        {
            Projectile.NewProjectile(Projectile.Center, Vector2.Zero, Mod.Find<ModProjectile>("PrismBombPrism").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.Center, Vector2.Zero, Mod.Find<ModProjectile>("PrismExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
    }
}