using ExtraExplosives.Projectiles.Testing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class DevItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dev Item");
            Tooltip.SetDefault("'For testing purposes'");
        }

        public override void SetDefaults()
        {
            Item.damage = 0;     //The damage stat for the Weapon.
            Item.width = 20;    //sprite width
            Item.height = 20;   //sprite height
            Item.maxStack = 1;   //This defines the items max stack
            Item.consumable = false;  //Tells the game that this should be used up once fired
            Item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            Item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example	
            Item.rare = 11;  //The color the title of your item when hovering over it ingame
            Item.UseSound = SoundID.Item1; //The sound played when using this item
            Item.useAnimation = 20;  //How long the item is used for.
            Item.useTime = 20;
            Item.noUseGraphic = true;
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damage
            Item.shoot = ModContent.ProjectileType<TestingExplosive>(); //This defines what type of projectile this item will shoot
            Item.shootSpeed = 5f; //This defines the projectile speed when shot
                                  //item.createTile = ModContent.TileType<ExplosiveTile>();
                                  //item.createTile = mod.TileType("ExplosiveTile");
        }


        private int cooldown = 0;
        public override void UpdateInventory(Player player)
        {
            cooldown--;
        }

        public override bool AltFunctionUse(Player player)
        {
            if (cooldown > 0) return false;
            cooldown = 30;
            //Projectile projectile = Projectile.NewProjectileDirect(Main.player[item.owner].position, Vector2.Zero, ModContent.ProjectileType<TestingExplosive>(), 0, 0, Main.myPlayer);
            //projectile.hostile = true;
            return false;
        }
    }
}