using IL.Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ProjectileID = Terraria.ID.ProjectileID;

namespace ExtraExplosives.Projectiles
{
    public class MushroomProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MushroomProjectile");
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 18;
            projectile.timeLeft = 3000;
            projectile.friendly = true;
            projectile.light = 10;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.myPlayer != projectile.owner) return;
            Player player = Main.player[projectile.owner];
            if (projectile.position.X < player.Center.X - Main.screenWidth / 2 ||
                projectile.position.X > player.Center.X + Main.screenWidth / 2 ||
                projectile.position.Y < player.Center.Y - Main.screenHeight / 2 ||
                projectile.position.Y > player.Center.Y + Main.screenHeight / 2)
            {
                projectile.Kill();
            }
            
            // Does nothing, just float, will add code to emulate the Vanilla NPC
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Kill(0);
        }

        public override void Kill(int timeLeft)
        {
            return;
        }
    }
}