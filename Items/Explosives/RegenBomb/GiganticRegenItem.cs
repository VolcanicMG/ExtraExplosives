using ExtraExplosives.Projectiles.RegenBomb;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class GiganticRegenItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigantic Regeneration Bomb");
            Tooltip.SetDefault("Regenerates everything in a 80 block radius");
        }

        public override string Texture { get; } = "ExtraExplosives/Items/Explosives/GiganticExplosiveItem";

        public override void SafeSetDefaults()
        {
            item.consumable = false;
            item.width = 20;
            item.height = 20;
            item.damage = 0;
            item.knockBack = 0;
            item.shoot = ModContent.ProjectileType<GiganticRegenProjectile>();
            item.useTime = 15;
            item.useAnimation = 1;
            item.useStyle = 1;
        }
    }
}