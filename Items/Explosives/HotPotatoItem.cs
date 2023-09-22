using System;
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class HotPotatoItem : ExplosiveItem
    {
        /*public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hot Potato (WIP)");
            Tooltip.SetDefault("We don't get it either\n" 
                + "Hold the left mouse button to charge");
        }*/

        public override void SafeSetDefaults()
        {
            Item.useTurn = true;
            Item.damage = 0;	 
            Item.width = 40;	
            Item.height = 40;   
            Item.maxStack = 99;  
            Item.consumable = true;  
            Item.useStyle = 4;
            Item.rare = ItemRarityID.Orange;	 
            Item.UseSound = null; 
            Item.useAnimation = 5;
            Item.autoReuse = true;
            Item.useTime = 5;	 
            Item.value = Item.buyPrice(0, 1, 17, 0);
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<HotPotatoProjectile>();
            Item.shootSpeed = 0f;
            Item.channel = true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.netMode != NetmodeID.Server && !Filters.Scene["BurningScreen"].IsActive())
            {
                Item.shoot = ModContent.ProjectileType<HotPotatoProjectile>();
                return true;
            }
            Item.shoot = ProjectileID.None;
            return true;
        }

        /*public override bool UseItemFrame(Player player)
        {
            player.releaseUseItem = true;
            return base.UseItemFrame(player);
        }*/

        /*public override void HoldItem(Player player)
        {
            if (!Main.mouseLeftRelease)
            {
                //item.useAnimation = firespeed;
            }
            else if (Main.mouseLeftRelease && beingUsed)
            {
                beingUsed = false;
                player.itemAnimation = 0;
            }
        }*/

        public override string Texture => "ExtraExplosives/Projectiles/HotPotatoProjectile";

        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.Dynamite);
        //    recipe.AddIngredient(ItemID.Carrot);
        //    recipe.AddIngredient(ItemID.LivingFireBlock, 10);
        //    recipe.AddTile(TileID.Campfire);
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
        //}
    }
}