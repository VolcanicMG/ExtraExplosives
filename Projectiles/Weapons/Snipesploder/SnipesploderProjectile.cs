using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.Snipesploder
{
    public class SnipesploderProjectile : ModProjectile
    {
        private float LifeTime    // Tracks how long the projectile has been alive
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snipesploder");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 1;
            projectile.timeLeft = 31;
            projectile.rotation = (float) Math.Atan(projectile.velocity.Y / projectile.velocity.X);
        }

        public override bool PreAI()
        {
            LifeTime++;
            Player player = Main.player[projectile.owner];
            Vector2 playerHandPos = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.Center = playerHandPos;
            //float angle = (float) Math.Acos(1 / Vector2.Distance(player.Center, projectile.Center));
            //Main.NewText(angle + " " + Vector2.Distance(player.Center, projectile.Center));
            return true;
        }

        public override void AI()
        {
            if ((int)LifeTime == 5)
            {
                projectile.frame++;
                
                Player player = Main.player[projectile.owner];
                Vector2 playerHandPos = player.RotatedRelativePoint(player.MountedCenter, true);
                Vector2 aim = Vector2.Normalize(Main.MouseWorld - playerHandPos);
                if (aim.HasNaNs()) {
                    aim = -Vector2.UnitY;
                }
                
                Main.NewText("Spawned from projectile");
                Projectile.NewProjectile(projectile.Center, aim * 10, ProjectileID.Dynamite, 0, 0, 255);
            }

            if ((int) LifeTime > 30)
            {
                projectile.Kill();
            }
        }
    }
}