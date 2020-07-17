using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class BombBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Bag");
            Tooltip.SetDefault("Filled with explosives");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 34;
            item.value = 20000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Pink;
            item.accessory = true;
            item.social = false;
        }
    }
}