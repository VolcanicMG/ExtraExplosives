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
            Item.height = 40;
            Item.width = 40;
            Item.value = Item.buyPrice(0, 0, 0, 55); ;
            Item.rare = ItemRarityID.Blue;
            Item.expert = true;
        }

    }
}