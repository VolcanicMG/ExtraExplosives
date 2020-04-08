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

namespace ExtraExplosives.Items
{
    public class NPCSpawnerItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos...");
            Tooltip.SetDefault("A small price to pay for salvation \n" +
                "Spawns in the man, the myth, the legend! \n" +
                "[c/AB40FF:WARNING!!! Use way from important structures]");
        }

        public override void SetDefaults()
        {

            item.damage = 0;     //The damage stat for the Weapon.                
            item.width = 20;    //sprite width
            item.height = 20;   //sprite height
            item.maxStack = 1;   //This defines the items max stack
            item.consumable = true;  //Tells the game that this should be used up once fired
            item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            item.rare = 10;     //The color the title of your item when hovering over it ingame
            item.UseSound = SoundID.Item1; //The sound played when using this item
            item.useAnimation = 20;  //How long the item is used for.
            // item.useTime = 20;     //How fast the item is used.
            item.value = Item.buyPrice(0, 0, 0, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            item.noUseGraphic = true;
            item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.shoot = mod.ProjectileType("NPCSpawnerProjectile"); //This defines what type of projectile this item will shoot
            item.shootSpeed = 8f; //This defines the projectile speed when shot
            //item.createTile = mod.TileType("ExplosiveTile");

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BasicExplosiveItem"), 1);
            recipe.AddIngredient(mod.ItemType("SmallExplosiveItem"), 1);
            recipe.AddIngredient(mod.ItemType("MediumExplosiveItem"), 1);
            recipe.AddIngredient(mod.ItemType("LargeExplosiveItem"), 1);
            recipe.AddIngredient(mod.ItemType("MegaExplosiveItem"), 1);
            recipe.AddIngredient(mod.ItemType("GiganticExplosiveItem"), 1);
            recipe.AddIngredient(mod.ItemType("BigBouncyDynamite"), 1);
            recipe.AddIngredient(mod.ItemType("DynaglowmiteItem"), 1);
            recipe.AddIngredient(ItemID.Grenade, 50);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("BasicExplosiveItem"), 1);
            recipe2.AddIngredient(mod.ItemType("SmallExplosiveItem"), 1);
            recipe2.AddIngredient(mod.ItemType("MediumExplosiveItem"), 1);
            recipe2.AddIngredient(mod.ItemType("LargeExplosiveItem"), 1);
            recipe2.AddIngredient(mod.ItemType("MegaExplosiveItem"), 1);
            recipe2.AddIngredient(mod.ItemType("GiganticExplosiveItem"), 1);
            recipe2.AddIngredient(mod.ItemType("BigBouncyDynamite"), 1);
            recipe2.AddIngredient(mod.ItemType("DynaglowmiteItem"), 1);
            recipe2.AddIngredient(ItemID.Grenade, 50);
            recipe2.AddIngredient(ItemID.ShadowScale, 10);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();

        }
    }

}