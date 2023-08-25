using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class FleshyBlastingCaps : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fleshy Blasting Caps");
            /* Tooltip.SetDefault("'Why is it sticky'\n" +
                               "Bombs gain a small amount of lifesteal"); */
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
            player.EE().FleshBlastingCaps = true;
        }
    }
}