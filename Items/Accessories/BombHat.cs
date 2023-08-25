using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories
{
    [AutoloadEquip(EquipType.Head)]
    public class BombHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bomb Hat");
        }

        public override void SetDefaults()
        {
            //item.social = true;
            //item.accessory = true;
            Item.value = 5000;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
            Item.expert = true;
        }

        //public override bool DrawHead() => false;
    }
}