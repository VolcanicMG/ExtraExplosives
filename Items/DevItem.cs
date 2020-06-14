using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
	public class DevItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Projectile Tester");
			Tooltip.SetDefault("For testing purposes");
		}

		public override void SetDefaults()
		{
			item.damage = 0;	 //The damage stat for the Weapon.
			item.width = 20;	//sprite width
			item.height = 20;   //sprite height
			item.maxStack = 1;   //This defines the items max stack
			item.consumable = false;  //Tells the game that this should be used up once fired
			item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
			item.rare = 11;	 //The color the title of your item when hovering over it ingame
			item.UseSound = SoundID.Item1; //The sound played when using this item
			item.useAnimation = 20;  //How long the item is used for.
			item.useTime = 20;
			item.noUseGraphic = true;
			item.noMelee = true;	  //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
			item.shoot = 386; //This defines what type of projectile this item will shoot
			item.shootSpeed = 15f; //This defines the projectile speed when shot
			//item.createTile = mod.TileType("ExplosiveTile");
		}
	}
}