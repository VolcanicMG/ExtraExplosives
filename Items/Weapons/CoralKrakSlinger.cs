using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class CoralKrakSlinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Coral Krak-Slinger");
            // Tooltip.SetDefault("'Improvised, but deadly'");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.autoReuse = true;
            Item.useAnimation = 21;
            Item.useTime = 21;
            Item.useAmmo = AmmoID.Rocket;
            Item.width = 26;
            Item.height = 38;
            Item.shoot = ProjectileID.Grenade;
            Item.UseSound = SoundID.Item11;
            Item.damage = 12;
            Item.shootSpeed = 5;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 15, 0, 50);
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.Mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.Text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.Text = damageValue + " explosive " + damageWord;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float speedX = velocity.X;
            float speedY = velocity.Y;
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Projectile.NewProjectile(source, position, new Vector2(speedX, speedY), ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}