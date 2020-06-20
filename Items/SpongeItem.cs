using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
	public class SpongeItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sponge");
			Tooltip.SetDefault("Not very absorbent on its own...");
		}

		public override void SetDefaults()
		{
			item.width = 22;	//sprite width
			item.height = 22;   //sprite height
			item.maxStack = 999;   //This defines the items max stack
			item.consumable = false;  //Tells the game that this should be used up once fired
			item.rare = 1;	 //The color the title of your item when hovering over it ingame
			//item.useTime = 20;	 //How fast the item is used.
			item.value = Item.buyPrice(0, 0, 0, 10);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cobweb, 15);
			recipe.AddIngredient(ItemID.Wood, 1);
			recipe.anyWood = true;
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}