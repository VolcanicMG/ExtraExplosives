using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Explosives
{
    public class HotPotatoItem : ModItem
    {
        private int _pickPower = 0;
        
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
            item.useAnimation = 20;
            item.autoReuse = false;
            //item.useTime = 20;	 
            item.value = Item.buyPrice(0, 1, 17, 0);  
            item.noUseGraphic = true;
            item.noMelee = true;	  
            item.shoot = mod.ProjectileType("HotPotatoProjectile"); 
            item.shootSpeed = 0f; 
        }

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