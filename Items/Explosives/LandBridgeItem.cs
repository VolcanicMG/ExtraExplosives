using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
	public class LandBridgeItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Land Bridge");
			Tooltip.SetDefault("Explodes into a long bridge across the world");
		}

		public override void SetDefaults()
		{
			item.damage = 0;     //The damage stat for the Weapon.
			item.width = 20;    //sprite width
			item.height = 20;   //sprite height
			item.maxStack = 1;   //This defines the items max stack
			item.consumable = true;  //Tells the game that this should be used up once fired
			item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
			item.rare = 10;  //The color the title of your item when hovering over it ingame
			item.UseSound = SoundID.Item1; //The sound played when using this item
			item.useAnimation = 20;  //How long the item is used for.
									 //item.useTime = 20;	 //How fast the item is used.
			item.value = Item.buyPrice(3,0,0,0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
			item.noUseGraphic = true;
			item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
			item.shoot = ModContent.ProjectileType<LandBridgeProjectile>(); //This defines what type of projectile this item will shoot
			item.shootSpeed = 14f; //This defines the projectile speed when shot
								  //item.createTile = mod.TileType("ExplosiveTile");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 700);
			recipe.AddIngredient(ItemID.StoneBlock, 700);
			recipe.AddIngredient(ItemID.Torch, 70);
			recipe.AddIngredient(ItemID.BuilderPotion, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}