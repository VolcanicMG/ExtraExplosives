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
            // DisplayName.SetDefault("Fuse Eel Surströmming");
            // Tooltip.SetDefault("Is it edible?");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item3;
            Item.useTurn = true;
            Item.consumable = true;
            Item.maxStack = 30;
            Item.value = Item.buyPrice(silver: 3);
            Item.rare = ItemRarityID.Blue;
            Item.buffType = ModContent.BuffType<ExtraExplosivesSurstrommingBuff>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 5200; //The amount of time the buff declared in item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FuseEelItem>(), 5);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }
    }
}