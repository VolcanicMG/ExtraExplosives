using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class DynaglowmiteItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynaglowmite");
            Tooltip.SetDefault("'Spawns in a bunch of glow sticks. Great for spelunking!'");
        }

        public override void SafeSetDefaults()
        {
            Item.damage = 0;     //The damage stat for the Weapon.
            Item.width = 20;    //sprite width
            Item.height = 20;   //sprite height
            Item.maxStack = 999;   //This defines the items max stack
            Item.consumable = true;  //Tells the game that this should be used up once fired
            Item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            Item.rare = 4;   //The color the title of your item when hovering over it ingame
            Item.UseSound = SoundID.Item1; //The sound played when using this item
            Item.useAnimation = 60;  //How long the item is used for.
            Item.useTime = 60;   //How fast the item is used.
            Item.value = Item.buyPrice(0, 0, 10, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            Item.noUseGraphic = true;
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            Item.shoot = Mod.Find<ModProjectile>("DynaglowmiteProjectile").Type; //This defines what type of projectile this item will shoot
            Item.shootSpeed = 10f; //This defines the projectile speed when shot
                                   //item.createTile = mod.TileType("ExplosiveTile");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bomb, 1);
            recipe.AddIngredient(ItemID.StickyGlowstick, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 1);
            recipe2.AddIngredient(ItemID.StickyGlowstick, 10);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();
        }
    }
}