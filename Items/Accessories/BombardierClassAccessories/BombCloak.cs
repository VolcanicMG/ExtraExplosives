using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class BombCloak : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Cloak");
            Tooltip.SetDefault("Explodes when you take damage");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.value = 5000;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().BombCloak = true;
        }

    }
}