using ExtraExplosives.Projectiles.RegenBomb;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives.RegenBomb
{
    public class MediumRegenItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Medium Regeneration Bomb");
            Tooltip.SetDefault("Regenerates everything in a 10 block radius");
        }

        public override void SafeSetDefaults()
        {
            item.consumable = false;
            item.width = 20;
            item.height = 20;
            item.damage = 0;
            item.knockBack = 0;
            item.shoot = ModContent.ProjectileType<MediumRegenProjectile>();
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
        }
    }
}