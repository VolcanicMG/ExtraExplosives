using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class CertificateOfDemolition : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Certificate of Demolition");
            /* Tooltip.SetDefault("'Certification that you are trained in the art of Demolition.'\n" +
                "50% Increased area of effect for explosives"); */
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().CertificateOfDemolition = true;
        }
    }
}