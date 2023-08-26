using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class MushroomProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("MushroomProjectile");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 18;
            Projectile.timeLeft = 3000;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.light = .8f;
            Projectile.tileCollide = false;
            Projectile.damage = 100;
        }

        public override void AI()
        {
            if (Main.myPlayer != Projectile.owner) return;
            Player player = Main.player[Projectile.owner];
            if (Projectile.position.X < player.Center.X - Main.screenWidth / 2 ||
                Projectile.position.X > player.Center.X + Main.screenWidth / 2 ||
                Projectile.position.Y < player.Center.Y - Main.screenHeight / 2 ||
                Projectile.position.Y > player.Center.Y + Main.screenHeight / 2)
            {
                Projectile.Kill();
            }

            // Does nothing, just float, will add code to emulate the Vanilla NPC
        }

        //public override void OnHitPlayer(Player target, int damage, bool crit)
        //{
        //    return;
        //}

        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{
        //    target.StrikeNPC(damage, knockback, projectile.direction, crit);
        //    projectile.Kill();
        //}

        //public override void Kill(int timeLeft)
        //{

        //}
    }
}