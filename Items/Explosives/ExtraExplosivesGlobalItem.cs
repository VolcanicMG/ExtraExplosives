using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
	public class ExtraExplosivesGlobalItem : GlobalItem
	{
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 3);
			recipe.AddIngredient(ItemID.Gel, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(ItemID.Dynamite);
			recipe.AddRecipe();
			base.AddRecipes();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 1);
			recipe2.AddIngredient(ItemID.Grenade, 1);
			recipe2.AddIngredient(ItemID.Gel, 5);
			recipe2.AddTile(TileID.WorkBenches);
			recipe2.SetResult(ItemID.Bomb);
			recipe2.AddRecipe();
			base.AddRecipes();
		}
	}
}