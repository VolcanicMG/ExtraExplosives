<<<<<<< HEAD
=======
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class BombBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Bag");
<<<<<<< HEAD
            Tooltip.SetDefault("Filled with explosives");
=======
            Tooltip.SetDefault("50% chance to throw a second explosives\n" +
                               "Does not consume a second item");
>>>>>>> Charlie's-Uploads
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 34;
            item.value = 20000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Pink;
            item.accessory = true;
            item.social = false;
        }
<<<<<<< HEAD
=======
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BombBag = true;
        }
>>>>>>> Charlie's-Uploads
    }
}