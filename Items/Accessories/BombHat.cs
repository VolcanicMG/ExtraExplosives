<<<<<<< HEAD
=======
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories
{
<<<<<<< HEAD
=======
    [AutoloadEquip(EquipType.Head)]
>>>>>>> Charlie's-Uploads
    public class BombHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Hat");
        }

        public override void SetDefaults()
        {
            item.social = true;
            item.accessory = true;
            item.value = 5000;
            item.rare = ItemRarityID.Orange;
            item.consumable = false;
            item.expert = true;
        }
<<<<<<< HEAD
=======

        public override bool DrawHead() => false;
>>>>>>> Charlie's-Uploads
    }
}