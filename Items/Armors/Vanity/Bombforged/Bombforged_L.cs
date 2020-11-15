using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Vanity.Bombforged
{
    [AutoloadEquip(EquipType.Legs)]
    public class Bombforged_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb-forged Legs");
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