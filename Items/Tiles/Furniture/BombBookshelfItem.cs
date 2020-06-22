using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombBookshelfItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Bookcase");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Bookcase);
            item.width = 12;
            item.height = 20;
            item.createTile = ModContent.TileType<BombBookshelfTile>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bomb, 10);
            recipe.AddIngredient(ItemID.Bookcase);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}