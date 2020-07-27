using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class Snipesploder : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snipesploder");
            Tooltip.SetDefault("Arrows are for chumps");
        }

        public override void SetDefaults()
        {
            item.damage = 38;
            item.ranged = true;
            item.width = 62;
            item.height = 24;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 15;
            item.useAmmo = AmmoID.Rocket;
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-9, 2);
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            type = ProjectileID.Dynamite;
            return true; // return false because we don't want tmodloader to shoot projectile
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBow, 1);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(ItemID.Dynamite, 1);
            recipe.anyWood = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
            
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadBow, 1);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(ItemID.Dynamite, 1);
            recipe.anyWood = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}