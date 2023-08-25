using ExtraExplosives.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombCandleItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Candle");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 20;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.value = 1000;
            Item.createTile = ModContent.TileType<BombCandleTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.Dynamite, 10);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddIngredient(ItemID.Gel, 10);
            //recipe.anyWood = true;
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}