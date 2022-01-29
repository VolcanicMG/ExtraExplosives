using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class RandomFuel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Random Fuel");
            Tooltip.SetDefault("Makes explosives detonate twice");
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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().RandomFuel = true;
        }
    }
}