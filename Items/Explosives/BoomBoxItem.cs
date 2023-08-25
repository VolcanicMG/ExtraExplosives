using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class BoomBoxItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boom Box");
            // Tooltip.SetDefault("Plays booming music");
        }

        public override void SafeSetDefaults()
        {
            Item.damage = 0;     //The damage stat for the Weapon.
            Item.knockBack = 0;
            Item.width = 40;    //sprite width
            Item.height = 28;   //sprite height
            Item.maxStack = 999;   //This defines the items max stack
            Item.consumable = true;  //Tells the game that this should be used up once fired
            Item.useStyle = ItemUseStyleID.Swing;   //The way your item will be used, 1 is the regular sword swing for example
            Item.rare = ItemRarityID.Yellow;     //The color the title of your item when hovering over it ingame
            Item.UseSound = SoundID.Item1; //The sound played when using this item
            Item.useAnimation = 20;  //How long the item is used for.
                                     //item.useTime = 20;	 //How fast the item is used.
            Item.value = Item.buyPrice(0, 0, 2, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            Item.noUseGraphic = true;
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            Item.shoot = ModContent.ProjectileType<BoomBoxProjectile>(); //This defines what type of projectile this item will shoot
            Item.shootSpeed = 6f; //This defines the projectile speed when shot
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
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.Bomb, 5);
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}