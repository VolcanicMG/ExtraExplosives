using System.Collections.Generic;
using System.Linq;
using ExtraExplosives.Projectiles.Weapons.Snipesploder;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class Snipesploder : ExplosiveWeapon
    {
        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/Snipesploder/Snipesploder";

        public override void SafeSetDefaults()
        {
            Item.damage = 38;
            Item.width = 62;
            Item.height = 24;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; 
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SnipesploderProjectile>();
            Item.shootSpeed = 15;
            Item.useAmmo = AmmoID.Rocket;

            PrimarySounds = new SoundStyle[4];
            SecondarySounds = null;

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] =
                    new SoundStyle(SoundLocation + n);
            }
        }
        
        /*public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.text = damageValue + " explosive " + damageWord;
            }
        }*/

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],position);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<SnipesploderProjectile>()] <= 0;

        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.IronBow, 1);
        //    recipe.AddIngredient(ItemID.Wood, 20);
        //    recipe.AddIngredient(ItemID.Dynamite, 1);
        //    recipe.anyWood = true;
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
            
        //    recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.LeadBow, 1);
        //    recipe.AddIngredient(ItemID.Wood, 20);
        //    recipe.AddIngredient(ItemID.Dynamite, 1);
        //    recipe.anyWood = true;
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
        //}
    }
}