using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class BombBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Bag");
            Tooltip.SetDefault("50% chance to throw a second explosive\n" +
                               "Does not consume a second item");
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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BombBag = true;
        }
    }
}