using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class CertificateOfDemolition : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Certificate of Demolition");
            Tooltip.SetDefault("'Certification that you are trained in the art of Demolition.'\n" +
                "50% Increased area of effect for explosives");
        }

        public override void SetDefaults()
        {
            item.accessory = true;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.consumable = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().CertificateOfDemolition = true;
        }
    }
}