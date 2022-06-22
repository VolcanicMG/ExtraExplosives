using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class BombardierEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bombard Emblem");
            Tooltip.SetDefault("Disabled block damage from friendly explosives\n" +
                               "15% increase to explosive damage");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.value = 5000;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().BombardEmblem = true;
        }
    }
}