using ExtraExplosives.Buffs;
using ExtraExplosives.Pets;
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
            item.CloneDefaults(ItemID.Carrot);
            item.shoot = ModContent.ProjectileType<BombBuddy>();
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 9;
            item.buffType = ModContent.BuffType<BombBuddyBuff>();
            item.maxStack = 1;
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