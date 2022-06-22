using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class ShortFuse : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Short Fuse");
            Tooltip.SetDefault("Halves fuse length of explosives");    //\"But why is it misspelled?\" -An annoying person
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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().ShortFuse = true;
        }
    }
}