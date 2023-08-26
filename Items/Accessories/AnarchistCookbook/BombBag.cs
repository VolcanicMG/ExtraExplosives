using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class BombBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bomb Bag");
            /* Tooltip.SetDefault("50% chance to throw a second explosive\n" +
                               "Does not consume a second item"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 34;
            Item.value = 20000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BombBag = true;
        }
    }
}