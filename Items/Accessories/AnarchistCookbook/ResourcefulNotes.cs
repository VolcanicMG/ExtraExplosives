using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class ResourcefulNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resourceful Notes");
            Tooltip.SetDefault("Chicken scratch");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
            item.accessory = true;
            item.social = false;
        }

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ModContent.ItemType<BombBag>());
            modRecipe.AddIngredient(ModContent.ItemType<MysteryBomb>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
    }
}