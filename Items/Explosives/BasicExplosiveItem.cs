using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class BasicExplosiveItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Explosive");
            Tooltip.SetDefault("The most basic explosive");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 70;    //The damage stat for the Weapon.
            item.knockBack = 20;
            item.width = 10;    //sprite width
            item.height = 32;   //sprite height
            item.maxStack = 999;   //This defines the items max stack
            item.consumable = true;  //Tells the game that this should be used up once fired
            item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            item.rare = 0;   //The color the title of your item when hovering over it ingame
            item.UseSound = SoundID.Item1; //The sound played when using this item
            item.useAnimation = 20;  //How long the item is used for.
                                     //item.useTime = 20;	 //How fast the item is used.
            item.value = Item.buyPrice(0, 0, 2, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            item.noUseGraphic = true;
            item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.shoot = mod.ProjectileType("BasicExplosiveProjectile"); //This defines what type of projectile this item will shoot
            item.shootSpeed = 5f; //This defines the projectile speed when shot
                                  //item.createTile = mod.TileType("ExplosiveTile");
        }

        //public override bool UseItem(Player player)
        //{
        //	NPC.NewNPC((int)player.Center.X, (int)player.Center.Y + 20, mod.NPCType("Bow_Turret_AI"), 0, 0f, 0f, 0f, 0f, 255);
        //	Main.NewText("Turret placed!", (byte)30, (byte)255, (byte)10, false);
        //	Main.PlaySound(0, (int)player.position.X, (int)player.position.Y, 0);
        //	return true;

        //}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Grenade, 1);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddIngredient(ItemID.StoneBlock, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.CopperBar, 2);
            recipe2.AddIngredient(ItemID.Gel, 7);
            recipe2.AddIngredient(ItemID.StoneBlock, 2);
            recipe2.AddIngredient(ItemID.Torch, 1);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this, 2);
            recipe2.AddRecipe();

            recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TinBar, 2);
            recipe2.AddIngredient(ItemID.Gel, 7);
            recipe2.AddIngredient(ItemID.StoneBlock, 2);
            recipe2.AddIngredient(ItemID.Torch, 1);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this, 2);
            recipe2.AddRecipe();
        }
    }
}