using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
	public class EndlessRocketLoader : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Rocket Loader");
			Tooltip.SetDefault("Material for the hellfire battery\n" +
				"All rockets combined together!");
		}

		public override void SetDefaults()
		{
			item.width = 40;	//sprite width
			item.height = 40;   //sprite height
			item.maxStack = 1;   //This defines the items max stack
			item.consumable = false;  //Tells the game that this should be used up once fired
			item.rare = 11;	 //The color the title of your item when hovering over it ingame
			//item.useTime = 20;	 //How fast the item is used.
			item.value = Item.buyPrice(5, 0, 0, 10);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RocketI, 3996);
			recipe.AddIngredient(ItemID.RocketII, 3996);
			recipe.AddIngredient(ItemID.RocketIII, 3996);
			recipe.AddIngredient(ItemID.RocketIV, 3996);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}