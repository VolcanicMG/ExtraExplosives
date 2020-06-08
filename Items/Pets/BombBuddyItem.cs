using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.Projectiles;
using ExtraExplosives.Pets;
using ExtraExplosives.Buffs;

namespace ExtraExplosives.Items.Pets
{
    public class BombBuddyItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cherry Bomb");
            Tooltip.SetDefault("A likly companian?");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Carrot);
            item.shoot = ModContent.ProjectileType<BombBuddy>();
            item.value = Item.buyPrice(1,0,0,0);
            item.rare = 9;
            item.buffType = ModContent.BuffType<BombBuddyBuff>();
        }


        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }

}