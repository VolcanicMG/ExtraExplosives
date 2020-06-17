using System;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class HotPotatoItem : ModItem
    {
        private int _pickPower = 0;
        private int firespeed = 345;
        private bool beingUsed = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hot Potato");
            Tooltip.SetDefault("We don't get it either");
        }

        public override void SetDefaults()
        {
            item.useTurn = true;
            item.damage = 0;	 
            item.width = 40;	
            item.height = 40;   
            item.maxStack = 99;  
            item.consumable = true;  
            item.useStyle = 4;
            item.rare = ItemRarityID.Orange;	 
            item.UseSound = SoundID.Item1; 
            item.useAnimation = 5;
            item.autoReuse = true;
            item.useTime = 5;	 
            item.value = Item.buyPrice(0, 1, 17, 0);
            item.noUseGraphic = false;
            item.noMelee = true;	  
            item.shoot = mod.ProjectileType("HotPotatoProjectile"); 
            item.shootSpeed = 0f;
            item.channel = true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.netMode != NetmodeID.Server && !Filters.Scene["BurningScreen"].IsActive())
            {
                item.shoot = mod.ProjectileType("HotPotatoProjectile");
                return true;
            }
            item.shoot = 0;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Dynamite);
            recipe.AddIngredient(ItemID.Carrot);
            recipe.AddIngredient(ItemID.LivingFireBlock, 10);
            recipe.AddTile(TileID.Campfire);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}