﻿using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class GiganticExplosiveItem : ExplosiveItem
    {
        public override void SafeSetDefaults()
        {
            Item.damage = 1000;  //The damage stat for the Weapon.
            Item.knockBack = 100;
            Item.width = 20;    //sprite width
            Item.height = 20;   //sprite height
            Item.maxStack = 999;   //This defines the items max stack
            Item.consumable = true;  //Tells the game that this should be used up once fired
            Item.useStyle = ItemUseStyleID.Swing;   //The way your item will be used, 1 is the regular sword swing for example
            Item.rare = ItemRarityID.Pink;   //The color the title of your item when hovering over it ingame
            Item.UseSound = SoundID.Item1; //The sound played when using this item
            Item.useAnimation = 20;  //How long the item is used for.
                                     // item.useTime = 20;	 //How fast the item is used.
            Item.value = Item.buyPrice(0, 25, 0, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            Item.noUseGraphic = true;
            Item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            Item.shoot = ModContent.ProjectileType<GiganticExplosiveProjectile>(); //This defines what type of projectile this item will shoot
            Item.shootSpeed = 5f; //This defines the projectile speed when shot
                                  //item.createTile = mod.TileType("ExplosiveTile");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MegaExplosiveItem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<LargeExplosiveItem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MediumExplosiveItem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SmallExplosiveItem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}