using ExtraExplosives.Tiles.Furniture;
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
            item.width = 12;
            item.height = 20;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = true;
            item.maxStack = 99;
            item.consumable = true;
            item.value = 1000;
            item.createTile = ModContent.TileType<BombChairTile>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Dynamite, 10);
            recipe.AddIngredient(ItemID.WoodenChair);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}