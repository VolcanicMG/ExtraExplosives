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
    public class ReactivePlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reactive Plating");
<<<<<<< HEAD
            Tooltip.SetDefault("Made of decommissioned Doomsday bunkers");
=======
            Tooltip.SetDefault("Increases Explosive damage by X\n" +
                               "Reduces damage taken by 10%");
>>>>>>> Charlie's-Uploads
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }
<<<<<<< HEAD
=======
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().ReactivePlating = true;
        }
>>>>>>> Charlie's-Uploads
    }
}