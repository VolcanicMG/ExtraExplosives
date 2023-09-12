using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class BoomerangItem : ExplosiveItem
    {
        public override void SafeSetDefaults()
        {
            Item.CloneDefaults(ItemID.EnchantedBoomerang);
            Item.shoot = ModContent.ProjectileType<BoomerangProjectile>();
            Item.damage = 50;
            Item.knockBack = 20;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.crit = 15;
            Item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Flamarang, 1);
            recipe.AddIngredient(ItemID.Dynamite, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}