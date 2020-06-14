using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
	public class TornadoBombItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornado Bomb");
<<<<<<< HEAD
			Tooltip.SetDefault("Here comes the twister!\n" +
				"Grab onto something, it's going to be a wild ride.");
=======
			Tooltip.SetDefault("Spawns in a tornado that sucks up players, enemies, and items");
>>>>>>> Volcanic's_Uploads

			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 5));
		}

		public override void SetDefaults()
		{
			item.damage = 0;	 //The damage stat for the Weapon.
			item.width = 20;	//sprite width
			item.height = 20;   //sprite height
			item.maxStack = 999;   //This defines the items max stack
			item.consumable = true;  //Tells the game that this should be used up once fired
			item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
			item.rare = 1;	 //The color the title of your item when hovering over it ingame
			item.UseSound = SoundID.Item1; //The sound played when using this item
			item.useAnimation = 70;  //How long the item is used for.
			item.useTime = 70;
			// item.useTime = 20;	 //How fast the item is used.
			item.value = Item.buyPrice(0, 5, 50, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
			item.noUseGraphic = true;
			item.noMelee = true;	  //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damage
			item.shoot = ModContent.ProjectileType<TornadoBombProjectile>(); //This defines what type of projectile this item will shoot
			item.shootSpeed = 5f; //This defines the projectile speed when shot
			//item.createTile = mod.TileType("ExplosiveTile");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bomb, 1);
			recipe.AddIngredient(ItemID.IronBar, 5);
			recipe.AddIngredient(ItemID.SoulofFlight, 10);
			recipe.anyIronBar = true;
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}