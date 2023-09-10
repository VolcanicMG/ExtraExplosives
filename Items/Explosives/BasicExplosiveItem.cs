using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class BasicExplosiveItem : ExplosiveItem
    {
        public override void SafeSetDefaults()
        {
            Item.damage = 70;    //The damage stat for the Weapon.
            Item.knockBack = 20;
            Item.width = 10;    //sprite width
            Item.height = 32;   //sprite height
            Item.maxStack = 999;   //This defines the items max stack
            Item.consumable = true;  //Tells the game that this should be used up once fired
            Item.useStyle = ItemUseStyleID.Swing;   //The way your item will be used, 1 is the regular sword swing for example
            Item.rare = ItemRarityID.White;   //The color the title of your item when hovering over it ingame
            Item.UseSound = SoundID.Item1; //The sound played when using this item
            Item.useAnimation = 20;  //How long the item is used for.
                                     //item.useTime = 20;	 //How fast the item is used.
            Item.value = Item.buyPrice(0, 0, 2, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            Item.noUseGraphic = true;
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            Item.shoot = ModContent.ProjectileType<BasicExplosiveProjectile>(); //This defines what type of projectile this item will shoot
            Item.shootSpeed = 5f; //This defines the projectile speed when shot
                                  //item.createTile = mod.TileType("ExplosiveTile");
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Grenade, 1);
            recipe.AddIngredient(ItemID.Gel, 1);
            recipe.AddIngredient(ItemID.StoneBlock, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            Recipe recipe2 = CreateRecipe(2);
            recipe2.AddIngredient(ItemID.CopperBar, 1);
            recipe2.AddIngredient(ItemID.Gel, 3);
            recipe2.AddIngredient(ItemID.StoneBlock, 2);
            recipe2.AddIngredient(ItemID.Torch, 1);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();

            recipe2 = CreateRecipe(2);
            recipe2.AddIngredient(ItemID.TinBar, 1);
            recipe2.AddIngredient(ItemID.Gel, 3);
            recipe2.AddIngredient(ItemID.StoneBlock, 2);
            recipe2.AddIngredient(ItemID.Torch, 1);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();
        }
    }
}