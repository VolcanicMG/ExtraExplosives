using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.Snipesploder
{
    public class SnipesploderProjectile : ModProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Items/Explosives/BasicExplosiveItem";

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
            projectile.width = 62;
            projectile.height = 24;
            projectile.aiStyle = 20;
        }

        public override bool PreAI()
        {
            Main.NewText("Projectile alive");
            LifeTime++;
            Player player = Main.player[projectile.owner];
            Vector2 playerHandPos = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.Center = playerHandPos;
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