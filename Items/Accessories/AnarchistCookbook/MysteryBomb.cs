using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class MysteryBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mystery Bomb");
            // Tooltip.SetDefault("20% chance to not consume explosives");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = 4000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().MysteryBomb = true;
        }
    }
}