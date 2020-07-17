using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories
{
    public class BombardEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bombard Emblem");
        }

        public override void SetDefaults()
        {
            item.social = true;
            item.accessory = true;
            item.value = 5000;
            item.rare = ItemRarityID.Orange;
            item.consumable = false;
        }
    }
}