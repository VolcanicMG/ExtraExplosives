using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class RandomFuel : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Random Fuel");
            // Tooltip.SetDefault("Makes explosives detonate twice");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = 4000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().RandomFuel = true;
        }
    }
}