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
            Tooltip.SetDefault("Is it edible?\n" +
                               "Will increase explosion stats eventually\n" +
                               "does nothing currently");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.consumable = true;
            item.maxStack = 30;
            item.rare = ItemRarityID.Blue;
        }

        public override bool AltFunctionUse(Player player)
        {
            player.AddBuff(BuffID.WellFed, 6969, false);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<FuseEelItem>(), 5);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
            
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<FuseEelItem>(), 5);
            recipe.SetResult(this);
            recipe.needWater = true;
            recipe.AddRecipe();
        }
    }
}