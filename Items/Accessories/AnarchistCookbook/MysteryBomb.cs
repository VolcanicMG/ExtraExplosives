using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class MysteryBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystery Bomb");
            Tooltip.SetDefault("20% chance to not consume explosives");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.value = 4000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().MysteryBomb = true;
        }
    }
}