using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.Snipesploder
{
    public class SnipesploderProjectile : ModProjectile
    {
        private float LifeTime    // Tracks how long the projectile has been alive
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 31;
            Projectile.rotation = (float)Math.Atan(Projectile.velocity.Y / Projectile.velocity.X);
        }

        public override bool PreAI()
        {
            LifeTime++;
            Player player = Main.player[Projectile.owner];
            Vector2 playerHandPos = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.Center = playerHandPos;
            //float angle = (float) Math.Acos(1 / Vector2.Distance(player.Center, projectile.Center));
            //Main.NewText(angle + " " + Vector2.Distance(player.Center, projectile.Center));
            return true;
        }

        public override void AI()
        {
            if ((int)LifeTime == 5)
            {
                Projectile.frame++;

                Player player = Main.player[Projectile.owner];
                Vector2 playerHandPos = player.RotatedRelativePoint(player.MountedCenter, true);
                Vector2 aim = Vector2.Normalize(Main.MouseWorld - playerHandPos);
                if (aim.HasNaNs())
                {
                    aim = -Vector2.UnitY;
                }

                //Main.NewText("Spawned from projectile");
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, aim * 10, ProjectileID.Dynamite, 0, 0, 255);
            }

            if ((int)LifeTime > 30)
            {
                Projectile.Kill();
            }
        }
    }
}