using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class BlastShielding : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blast Shielding");
            Tooltip.SetDefault("Made of decommissioned Doomsday bunkers");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }
    }
}