using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Vanity.Explonin
{
    [AutoloadEquip(EquipType.Legs)]
    public class Explonin_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explonin Legs");
            Tooltip.SetDefault("");

        }

        public override void SetDefaults()
        {
            item.height = 40;
            item.width = 40;
            item.value = Item.buyPrice(0, 0, 0, 55); ;
            item.rare = ItemRarityID.Blue;
            item.expert = true;
        }

    }
}