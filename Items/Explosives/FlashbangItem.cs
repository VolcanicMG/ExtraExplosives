using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
	public class FlashbangItem : ExplosiveItem
	{
		public static int Direction;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flashbang");
			Tooltip.SetDefault("[c/AC5023:Don't look at it!!]\n" +
				"[c/AB40FF:Stuns enemies and confuses them]");
		}

		public override void SafeSetDefaults()
		{
			item.damage = 0;	 //The damage stat for the Weapon.
			item.width = 12;	//sprite width
			item.height = 32;   //sprite height
			item.maxStack = 999;   //This defines the items max stack
			item.consumable = true;  //Tells the game that this should be used up once fired
			item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
			item.rare = 6;	 //The color the title of your item when hovering over it ingame
			item.UseSound = SoundID.Item1; //The sound played when using this item
			item.useAnimation = 20;  //How long the item is used for.
			//item.useTime = 20;	 //How fast the item is used.
			item.value = Item.buyPrice(0, 0, 25, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
			item.noUseGraphic = true;
			item.noMelee = true;	  //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
			item.shoot = ModContent.ProjectileType<FlashbangProjectile>(); //This defines what type of projectile this item will shoot
			item.shootSpeed = 10f; //This defines the projectile speed when shot
			//item.createTile = mod.TileType("ExplosiveTile");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Direction = player.direction;

			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ExplosivePowder, 1);
			recipe.AddIngredient(ItemID.Grenade, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}