using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class ShortFuze : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Short Fuze");
            Tooltip.SetDefault("\"But why is it misspelled?\" -An annoying person");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.value = 20000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Pink;
            item.accessory = true;
            item.social = false;
        }
    }
}