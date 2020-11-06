using ExtraExplosives.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Misc
{
    public class FuseEelSurstrommingItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fuse Eel Surströmming");
            Tooltip.SetDefault("Is it edible?");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.UseSound = SoundID.Item3;
            item.useTurn = true;
            item.consumable = true;
            item.maxStack = 30;
            item.value = Item.buyPrice(silver: 3);
            item.rare = ItemRarityID.Blue;
            item.buffType = ModContent.BuffType<ExtraExplosivesSurstrommingBuff>(); //Specify an existing buff to be applied when used.
            item.buffTime = 5200; //The amount of time the buff declared in item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<FuseEelItem>(), 5);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
            
        }
    }
}