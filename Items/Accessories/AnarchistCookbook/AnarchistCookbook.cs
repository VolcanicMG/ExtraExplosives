using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class AnarchistCookbook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anarchist Cookbook");
            Tooltip.SetDefault("Just owning it could get you arrested");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.value = 100000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;
            item.social = false;
        }

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ModContent.ItemType<HandyNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<RandomNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<ResourcefulNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<SafetyNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<UtilityNotes>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
    }
}