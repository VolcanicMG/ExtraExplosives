using ExtraExplosives.Projectiles.Weapons.Snipesploder;
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
            item.shoot = ModContent.ProjectileType<SnipesploderProjectile>(); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 15;
            item.useAmmo = AmmoID.Rocket;
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<SnipesploderProjectile>()] <= 0;

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