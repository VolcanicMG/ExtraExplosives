using ExtraExplosives.Items.Accessories;
using ExtraExplosives.Items.Accessories.BombardierClassAccessories;
using ExtraExplosives.Items.Armors.Vanity.Bombforged;
using ExtraExplosives.Items.Armors.Vanity.Explonin;
using ExtraExplosives.Items.Armors.Vanity.TNTSUIT;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Items.Pets;
using ExtraExplosives.NPCs;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class CaptainExplosiveTreasureBag : ModItem
    {

        private int[] items = new int[3];

        private int[][] vanity = new int[3][];

        private int[] bombs = new int[27];

        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true; // Dev armor stuff
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Purple; // Might be wrong rarity
            Item.expert = true;
        }


        public override bool CanRightClick() => true;    // always able to right click so hijack the code to return true

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            items = new int[]    // Item array, add boss specific drops here
            {
                ModContent.ItemType<BombBuddyItem>(),    // Cherry Bomb
                ModContent.ItemType<BombHat>(),
                ModContent.ItemType<BombCloak>()
            };
            
            vanity = new int[][]    // Vanity set array, add vanity drops here
            {
                new int[] { ModContent.ItemType<Bombforged_B>(), ModContent.ItemType<Bombforged_H>(), ModContent.ItemType<Bombforged_L>() },
                new int[] { ModContent.ItemType<TNTSUIT_B>(), ModContent.ItemType<TNTSUIT_H>(), ModContent.ItemType<TNTSUIT_L>() },
                new int[] { ModContent.ItemType<Explonin_B>(), ModContent.ItemType<Explonin_H>(), ModContent.ItemType<Explonin_L>() },
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
                //ModContent.ItemType<HotPotatoItem>()
            };
            
            // TODO take a look at drop chance and fine tune if needed
            foreach ( int item in items )
            {
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(item, 3));
            }
            
            // These currently will drop individually, if they should drop as a set a different rule is needed TODO 
            foreach (int[] set in vanity)
            {
                foreach (int setItem in set)
                {
                    itemLoot.Add(ItemDropRule.BossBag(setItem));
                }
            }

            for ( int i = 0; i < bombs.Length; i++ )
            {
                itemLoot.Add(ItemDropRule.Common(i, 1, 2, 7));
            }
        }
    }
}