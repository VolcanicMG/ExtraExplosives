using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories
{
    [AutoloadEquip(EquipType.Head)]
    public class DynamiteHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynamite Hat");
        }

        public override void SetDefaults()
        {
            //item.social = true;
            //item.accessory = true;
            item.value = 5000;
            item.rare = ItemRarityID.Orange;
            item.consumable = false;
            item.expert = true;
        }

        public override bool DrawHead() => false;
    }
}