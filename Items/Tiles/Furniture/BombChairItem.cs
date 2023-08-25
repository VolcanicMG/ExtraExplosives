using ExtraExplosives.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombChairItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Chair");
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
            Item.createTile = ModContent.TileType<BombChairTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Dynamite, 10);
            recipe.AddIngredient(ItemID.WoodenChair);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}