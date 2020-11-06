using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    [AutoloadEquip(EquipType.Head)]
    public class BlastShielding : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blast Shielding");
            Tooltip.SetDefault("Immunity to friendly explosives");
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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BlastShielding = true;
        }
    }
}