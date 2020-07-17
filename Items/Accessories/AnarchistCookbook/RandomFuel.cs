using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class RandomFuel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Random Fuel");
            Tooltip.SetDefault("Questionable but flammable");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = 4000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }
    }
}