using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class LightweightBombshells : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightweight Bombshells");
            Tooltip.SetDefault("Doubles the initial velocity of thrown explosives");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = 4000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().LightweightBombshells = true;
        }
    }
}