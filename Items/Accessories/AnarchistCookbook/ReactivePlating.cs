using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class ReactivePlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reactive Plating");
            Tooltip.SetDefault("Increases explosive damage by 10%\n" +
                               "Reduces damage taken by 10%");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = 10000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().ReactivePlating = true;
        }
    }
}