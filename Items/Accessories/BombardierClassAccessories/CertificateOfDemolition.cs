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
            Tooltip.SetDefault("Certification that you are\n" +
                               "trained in the art of Demolition.\n" +
                               "Also increases area of effect of\n" +
                               "all explosives by 50%");
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
            player.EE().CertificateOfDemolition = true;
        }
    }
}