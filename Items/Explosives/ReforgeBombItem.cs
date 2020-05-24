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
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.Projectiles;

namespace ExtraExplosives.Items.Explosives
{
    public class ReforgeBombItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reforge Bomb");
            Tooltip.SetDefault("Reforge up to 3 items! \n" +
                "[c/32CD32:Press 'P' to toggle reforge UI]");
        }

        public override void SetDefaults()
        {

            item.damage = 0;     //The damage stat for the Weapon.                
            item.width = 20;    //sprite width
            item.height = 20;   //sprite height
            item.maxStack = 999;   //This defines the items max stack
            item.consumable = true;  //Tells the game that this should be used up once fired
            item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            item.rare = 2;     //The color the title of your item when hovering over it ingame
            item.UseSound = SoundID.Item1; //The sound played when using this item
            item.useAnimation = 20;  //How long the item is used for.
            // item.useTime = 20;     //How fast the item is used.
            item.value = Item.buyPrice(0, 1, 8, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            item.noUseGraphic = true;
            item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.shoot = ModContent.ProjectileType<ReforgeBombProjectile>(); //This defines what type of projectile this item will shoot
            item.shootSpeed = 5f; //This defines the projectile speed when shot
            //item.createTile = mod.TileType("ExplosiveTile");

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<HeavyBombItem>(), 1);
            recipe.AddIngredient(ItemID.GoldHammer, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ModContent.ItemType<HeavyBombItem>(), 1);
            recipe2.AddIngredient(ItemID.PlatinumHammer, 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }

}