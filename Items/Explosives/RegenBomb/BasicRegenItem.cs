using ExtraExplosives.Projectiles.RegenBomb;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class BasicRegenItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
                {
                    DisplayName.SetDefault("Basic Regeneration Bomb");
                    Tooltip.SetDefault("Regenerates everything in a 2 block radius");
                }
        
                public override string Texture { get; } = "ExtraExplosives/Items/Explosives/BasicExplosiveItem";
        
                public override void SafeSetDefaults()
                {
                    item.consumable = false;
                    item.width = 20;
                    item.height = 20;
                    item.damage = 0;
                    item.knockBack = 0;
                    item.shoot = ModContent.ProjectileType<BasicRegenProjectile>();
                    item.useTime = 15;
                    item.useAnimation = 1;
                    item.useStyle = 1;
                }
    }
}