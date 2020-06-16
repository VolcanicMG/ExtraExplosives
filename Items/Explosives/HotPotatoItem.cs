<<<<<<< HEAD
using System;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Graphics.Effects;
=======
using Terraria;
>>>>>>> master
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class HotPotatoItem : ModItem
    {
        private int _pickPower = 0;
<<<<<<< HEAD
        private int firespeed = 345;
=======
>>>>>>> master
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hot Potato");
            Tooltip.SetDefault("We don't get it either");
        }

        public override void SetDefaults()
        {
            item.damage = 0;	 
            item.width = 40;	
            item.height = 40;   
            item.maxStack = 99;  
            item.consumable = true;  
            item.useStyle = 4;  
            item.rare = ItemRarityID.Orange;	 
            item.UseSound = SoundID.Item1; 
<<<<<<< HEAD
            item.useAnimation = 345;
            item.autoReuse = false;
            item.useTime = 345;	 
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
                return true;
            }

            return false;
        }

        public override void HoldItem(Player player)
        {
            if (Main.mouseLeftRelease)
            {
                item.useAnimation = firespeed;
            }
            else if (!Main.mouseLeftRelease)
            {
                //item.useAnimation = 2;
                player.itemAnimation = 2;
            }
        }
        public override string Texture => "ExtraExplosives/Projectiles/HotPotatoProjectile";

=======
            item.useAnimation = 20;
            item.autoReuse = false;
            //item.useTime = 20;	 
            item.value = Item.buyPrice(0, 1, 17, 0);  
            item.noUseGraphic = true;
            item.noMelee = true;	  
            item.shoot = mod.ProjectileType("HotPotatoProjectile"); 
            item.shootSpeed = 0f; 
        }

>>>>>>> master
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