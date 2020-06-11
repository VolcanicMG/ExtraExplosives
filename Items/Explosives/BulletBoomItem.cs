using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.UI;
using Terraria.ModLoader.IO;

namespace ExtraExplosives.Items.Explosives
{
    public class BulletBoomItem : ModItem
    {

        // Variables
        private string displayName;
        private BulletBoomProjectile proj;
        
        public override bool CloneNewInstances => true;    // DONT CHANGE
        public override string Texture => "ExtraExplosives/Items/Explosives/BulletBoomItem";    // texture

        // Constructor, important
        public BulletBoomItem(string displayName, BulletBoomProjectile proj)
        {
            this.displayName = displayName;
            this.proj = proj;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(this.displayName + " Bullet Boom");    // Name
            Tooltip.SetDefault("Its filled with bullets");            // tooltip, change if needed
        }

        
        

        public override void SetDefaults()
        {
            item.damage = 75;     //The damage stat for the Weapon.   
            item.width = 20;    //sprite width
            item.height = 20;   //sprite height
            item.maxStack = 999;   //This defines the items max stack
            item.consumable = true;  //Tells the game that this should be used up once fired
            item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            item.rare = 8;     //The color the title of your item when hovering over it ingame
            item.UseSound = SoundID.Item1; //The sound played when using this item
            item.useAnimation = 35;  //How long the item is used for.
            item.useTime = 35;     //How fast the item is used.
            item.value = Item.buyPrice(0, 3, 0, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            item.noUseGraphic = true;
            item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.shoot = this.proj.projectile.type; // Leave this alone
            item.shootSpeed = 15f; //This defines the projectile speed when shot
            //item.createTile = mod.TileType("ExplosiveTile");

        }

        public override bool AllowPrefix(int pre)  
        {
            return true;
        }

        
        // This is here because i dont know if its needed, will leave just in case
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(this.itemID, 20);
            recipe.AddIngredient(ItemID.Gel, 10);
            recipe.AddIngredient(mod.ItemType("SmallExplosiveItem"), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }*/
    }

}