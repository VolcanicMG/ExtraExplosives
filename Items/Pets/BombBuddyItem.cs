using ExtraExplosives.Buffs;
using ExtraExplosives.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Pets
{
    public class BombBuddyItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cherry Bomb");
            Tooltip.SetDefault("Summons a pet bomb");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Carrot);
            Item.shoot = ModContent.ProjectileType<BombBuddy>();
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 9;
            Item.buffType = ModContent.BuffType<BombBuddyBuff>();
            Item.maxStack = 1;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }
}