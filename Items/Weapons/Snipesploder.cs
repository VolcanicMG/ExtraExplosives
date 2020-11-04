using System.Collections.Generic;
using System.Linq;
using ExtraExplosives.Projectiles.Weapons.Snipesploder;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class Snipesploder : ExplosiveWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snipesploder(WIP)");
            Tooltip.SetDefault("Arrows are for chumps");
        }

        protected override string SoundLocation { get; } = "Sounds/Item/Weapons/Snipesploder/Snipesploder";

        public override void SafeSetDefaults()
        {
            item.damage = 38;
            item.width = 62;
            item.height = 24;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; 
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<SnipesploderProjectile>();
            item.shootSpeed = 15;
            item.useAmmo = AmmoID.Rocket;

            PrimarySounds = new LegacySoundStyle[4];
            SecondarySounds = null;

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] =
                    mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + n);
            }
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.text = damageValue + " explosive " + damageWord;
            }
        }

        // public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
                    //     ref float knockBack)
                    // {
                    //     Main.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                    //         (int) player.position.X, (int) player.position.Y);
                    //     return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
                    // }

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