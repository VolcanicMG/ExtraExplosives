using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class ReactivePlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reactive Plating");
            Tooltip.SetDefault("Made of decommissioned Doomsday bunkers");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }
    }
}