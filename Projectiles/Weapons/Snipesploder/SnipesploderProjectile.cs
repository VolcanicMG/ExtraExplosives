using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.Snipesploder
{
    public class SnipesploderProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snipesploder Ordinance");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            
        }

        public override bool PreAI()
        {
            return true;
        }

        public override void AI()
        {
        }

        public override void PostAI()
        {
        }
    }
}