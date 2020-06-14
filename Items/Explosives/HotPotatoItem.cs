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
            Tooltip.SetDefault("Don't hold it too long");
        }

        public override void SetDefaults()
        {
            item.damage = 0;	 
            item.width = 40;	
            item.height = 40;   
            item.maxStack = 999;  
            item.consumable = true;  
            item.useStyle = 4;  
            item.rare = 3;	 
            item.UseSound = SoundID.Item1; 
            item.useAnimation = 20;  
            //item.useTime = 20;	 
            item.value = Item.buyPrice(0, 4, 17, 0);  
            item.noUseGraphic = true;
            item.noMelee = true;	  
            item.shoot = mod.ProjectileType("HotPotatoProjectile"); 
            item.shootSpeed = 0f; 
        }
    }
}