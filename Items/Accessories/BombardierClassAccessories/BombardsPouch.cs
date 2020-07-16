using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class BombardsPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomber's Pouch");
            Tooltip.SetDefault("30% Chance to not consume explosives");
        }

        public override void SetDefaults()
        {
            item.accessory = true;
            item.value = 5000;
            item.rare = ItemRarityID.Orange;
            item.consumable = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().BombersPouch = true;
        }
    }
}