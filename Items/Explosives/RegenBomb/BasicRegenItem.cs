using ExtraExplosives.Projectiles.RegenBomb;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives.RegenBomb
{
    public class BasicRegenItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Regeneration Bomb");
            Tooltip.SetDefault("Regenerates everything in a 2 block radius");
        }
        
                
        public override void SafeSetDefaults()
        {
            item.consumable = false;
            item.width = 20;
            item.height = 20;
            item.damage = 0;
            item.knockBack = 0;
            item.shoot = ModContent.ProjectileType<BasicRegenProjectile>();
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
        }
    }
}