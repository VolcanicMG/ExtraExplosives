using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Furniture
{
    public class BombChestItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Chest");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 28;
            item.maxStack = 99;
            item.value = 500;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<BombChestTile>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Dynamite, 10);
            recipe.AddIngredient(ItemID.Chest);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}