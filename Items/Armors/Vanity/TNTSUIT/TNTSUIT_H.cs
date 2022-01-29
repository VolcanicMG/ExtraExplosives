using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Vanity.TNTSUIT
{
    [AutoloadEquip(EquipType.Head)]
    public class TNTSUIT_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TNT Suit Head");
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