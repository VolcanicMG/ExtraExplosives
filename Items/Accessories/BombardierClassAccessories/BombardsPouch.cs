using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class BombardsPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bomber's Pouch");
            // Tooltip.SetDefault("30% chance to not consume explosives");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 50, 0, 0); ;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().BombersPouch = true;
        }
    }
}