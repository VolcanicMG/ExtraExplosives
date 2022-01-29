﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class CritterBombItem : ExplosiveItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Critter Bomb");
            Tooltip.SetDefault("Spawns in golden critters");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 70;    //The damage stat for the Weapon.
            item.knockBack = 20;
            item.width = 20;    //sprite width
            item.height = 20;   //sprite height
            item.maxStack = 999;   //This defines the items max stack
            item.consumable = true;  //Tells the game that this should be used up once fired
            item.useStyle = 1;   //The way your item will be used, 1 is the regular sword swing for example
            item.rare = 8;   //The color the title of your item when hovering over it ingame
            item.UseSound = SoundID.Item1; //The sound played when using this item
            item.useAnimation = 20;  //How long the item is used for.
                                     //item.useTime = 20;	 //How fast the item is used.
            item.value = Item.buyPrice(1, 3, 3, 0);   //How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3 silver)
            item.noUseGraphic = true;
            item.noMelee = true;      //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.shoot = mod.ProjectileType("CritterBombProjectile"); //This defines what type of projectile this item will shoot
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
            recipe.AddIngredient(ItemID.Bunny, 1);
            recipe.AddIngredient(ItemID.Bird, 1);
            recipe.AddIngredient(ItemID.Frog, 1);
            recipe.AddIngredient(ItemID.Grasshopper, 1);
            recipe.AddIngredient(ItemID.Mouse, 1);
            recipe.AddIngredient(ItemID.Squirrel, 1);
            recipe.AddIngredient(ItemID.Worm, 1);
            recipe.AddIngredient(ItemID.TreeNymphButterfly, 1);
            recipe.AddIngredient(ItemID.GoldBar, 7);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}