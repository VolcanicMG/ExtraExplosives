using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    [AutoloadEquip(EquipType.Head)]
    public class BlastShielding : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blast Shielding");
            // Tooltip.SetDefault("Immunity to friendly explosives");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.value = 10000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.social = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BlastShielding = true;
        }
    }
}