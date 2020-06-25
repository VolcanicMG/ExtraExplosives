using ExtraExplosives.Items.Accessories;
using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Pets;
using ExtraExplosives.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class CaptainExplosiveTreasureBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            int[] items = new int[]    // Item array, add all items which should drop here
            {
                ModContent.ItemType<BombBuddyItem>(),    // Cherry Bomb
                ModContent.ItemType<BombHat>(),
                ModContent.ItemType<BombCloak>()
            };

            foreach (int item in items)
            {
                if (Main.rand.Next(4) == 0)
                {
                    player.QuickSpawnItem(item, 1);    // add hooks for special items here
                }
            }
            
        }

        public override int BossBagNPC => ModContent.NPCType<CaptainExplosive>();
    }
}