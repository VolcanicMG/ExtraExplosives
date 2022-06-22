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
            Item.width = 20;
            Item.height = 26;
            Item.value = 20000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().ShortFuse = true;
        }
    }
}