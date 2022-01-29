using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles.Rockets
{
    public class Rocket0Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket 0");
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            aiType = ProjectileID.RocketI;
        }

        public override void AI()
        {

            //dust
            float num248 = 0f;
            float num249 = 0f;

            Vector2 position71 = new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f;
            int width67 = projectile.width - 8;
            int height67 = projectile.height - 8;
            Color newColor = default(Color);
            int num250 = Dust.NewDust(position71, width67, height67, 6, 0f, 0f, 100, newColor, 1f);
            Dust dust3 = Main.dust[num250];
            dust3.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
            dust3 = Main.dust[num250];
            dust3.velocity *= 0.2f;
            Main.dust[num250].noGravity = true;
            Vector2 position72 = new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f;
            int width68 = projectile.width - 8;
            int height68 = projectile.height - 8;
            newColor = default(Color);
            num250 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, newColor, 0.5f);
            Main.dust[num250].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
            dust3 = Main.dust[num250];
            dust3.velocity *= 0.05f;

            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            projectile.knockBack = 2;  // Since no calling item exists, knockback must be set internally	(Set in Hellfire Rocket Battery)

            //Create Bomb Dust
            ExplosionDust(5, projectile.Center, new Color(255, 255, 255), new Color(189, 24, 22), 2, projectile.oldVelocity);
        }
    }
}
