using ExtraExplosives.Projectiles;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    
    // In development, here only so i dont lose it in the merge
    // More or less completely ignore this and its projectile for now it doesnt work
    public class RegenBombItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regeneration Bomb");
            Tooltip.SetDefault("Regenerates the world");
        }

        public override string Texture { get; } = "ExtraExplosives/Items/Explosives/LargeExplosiveItem";

        public override void SafeSetDefaults()
        {
            item.consumable = false;
            item.width = 20;
            item.height = 20;
            item.damage = 0;
            item.knockBack = 0;
            item.shoot = ModContent.ProjectileType<RegenBombProjectile>();
            item.useTime = 15;
            item.useAnimation = 1;
            item.useStyle = 1;
        }
    }
}