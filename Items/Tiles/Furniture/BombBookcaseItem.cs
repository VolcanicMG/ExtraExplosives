using ExtraExplosives.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombBookcaseItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bomb Bookcase");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Bookcase);
            Item.width = 12;
            Item.height = 20;
            Item.createTile = ModContent.TileType<BombBookcaseTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bomb, 10);
            recipe.AddIngredient(ItemID.Bookcase);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}