using ExtraExplosives.Items.Accessories;
using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Items.Pets;
using ExtraExplosives.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class CaptainExplosiveTreasureBag : ModItem
    {

        private int[] items = new int[3];

        private int[] bombs = new int[27];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive Treasure Bag");    // Name, (change if you want idk)
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");    // Boss bag tooltip (dont change)
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
        }


        public override bool CanRightClick() => true;    // always able to right click so hijack the code to return true

        public override void OpenBossBag(Player player)
        {
            //player.QuickSpawnItem(items[0], 1);
            if (Main.hardMode)
            {
                player.TryGettingDevArmor(); // Will attempt to get dev armor if its hardmode
                player.TryGettingDevArmor(); // Dev armor only technically drops from hardmode bosses but fuck it
            }

            int drop = Main.rand.Next(3);    // get the item which will 100% drop
            player.QuickSpawnItem(items[drop], 1);    // spawn it
            items[drop] = bombs[Main.rand.Next(bombs.Length - 1)];    //Main.NewText(ModContent.GetModItem(.Name);    // to avoid double drops replace it with a pre-hardmode bomb
            foreach (int item in items)    // try to spawn each item left in the array
            {
                if (Main.rand.Next(3) == 0)
                {
                    player.QuickSpawnItem(item, 1);    // add hooks for special items here
                }
            }

        }

        public override void AddRecipes()
        {
            items = new int[]    // Item array, add boss specific drops here
            {
                ModContent.ItemType<BombBuddyItem>(),    // Cherry Bomb
                ModContent.ItemType<BombHat>(),
                ModContent.ItemType<BombCloak>()
            };

            bombs = new int[]        // List of bombs which should drop from CE boss bag,
            {
                ModContent.ItemType<BasicExplosiveItem>(),
                ModContent.ItemType<SmallExplosiveItem>(),
                ModContent.ItemType<MediumExplosiveItem>(),
                ModContent.ItemType<LargeExplosiveItem>(),
                ModContent.ItemType<MegaExplosiveItem>(),
                ModContent.ItemType<GiganticExplosiveItem>(),
                ModContent.ItemType<TorchBombItem>(),
                ModContent.ItemType<DynaglowmiteItem>(),
                ModContent.ItemType<BigBouncyDynamiteItem>(),
                ModContent.ItemType<HouseBombItem>(),
                ModContent.ItemType<ClusterBombItem>(),
                ModContent.ItemType<PhaseBombItem>(),
                ModContent.ItemType<TheLevelerItem>(),
                ModContent.ItemType<DeliquidifierItem>(),
                ModContent.ItemType<HydromiteItem>(),
                ModContent.ItemType<LavamiteItem>(),
                ModContent.ItemType<CritterBombItem>(),
                ModContent.ItemType<BunnyiteItem>(),
                ModContent.ItemType<BreakenTheBankenItem>(),
                ModContent.ItemType<HeavyBombItem>(),
                ModContent.ItemType<DaBombItem>(),
                ModContent.ItemType<ReforgeBombItem>(),
                ModContent.ItemType<MeteoriteBusterItem>(),
                ModContent.ItemType<TrollBombItem>(),
                ModContent.ItemType<FlashbangItem>(),
                ModContent.ItemType<RainboomItem>(),
                ModContent.ItemType<HotPotatoItem>()
            };
            base.AddRecipes();
        }

        public override int BossBagNPC => ModContent.NPCType<CaptainExplosive>();    // does some terraria stuff to make the game know this is a boss bag
    }
}