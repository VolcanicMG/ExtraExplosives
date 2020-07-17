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
<<<<<<< HEAD
=======
    [AutoloadEquip(EquipType.Head)]
>>>>>>> Charlie's-Uploads
    public class BlastShielding : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blast Shielding");
<<<<<<< HEAD
            Tooltip.SetDefault("Made of decommissioned Doomsday bunkers");
=======
            Tooltip.SetDefault("Immunity to friendly Explosives");
>>>>>>> Charlie's-Uploads
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
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
            player.GetModPlayer<ExtraExplosivesPlayer>().BlastShielding = true;
        }
>>>>>>> Charlie's-Uploads
    }
}