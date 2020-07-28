using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class BombardsLaurels : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bombard's Laurels");
            Tooltip.SetDefault("Increases Area of Effect of\n" +
                               "all Explosives by 80%");
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
            player.EE().BombersHat = true;
            player.EE().CertificateOfDemolition = true;
        }
    }
}