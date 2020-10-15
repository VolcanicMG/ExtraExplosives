using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class RavenousBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ravenous Bomb");
            Tooltip.SetDefault("It looks hungry\n" +
                               "Gives explosions lifesteal");
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
            player.EE().RavenousBomb = true;
        }
    }
}