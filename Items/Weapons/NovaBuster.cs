using ExtraExplosives.Projectiles.Weapons.NovaBuster;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class NovaBuster : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useAmmo = AmmoID.Rocket;
            Item.width = 94;
            Item.height = 34;
            Item.shoot = 134;
            Item.UseSound = SoundID.Item11;
            Item.damage = 350;
            Item.shootSpeed = 16f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 15, 0, 50);
            Item.knockBack = 8;
            Item.rare = ItemRarityID.Red;
            //Item.ranged = true;
        }

        // check, isnt this done in the hjson?
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine ColoredTooltip = new TooltipLine(Mod, "ExtraTooltip", $"'The death of a star compressed into a single weapon'");
            ColoredTooltip.OverrideColor = Color.Chartreuse;
            tooltips.Add(ColoredTooltip);

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
            return new Vector2(-16, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float speedX = velocity.X;
            float speedY = velocity.Y;

            //Spread
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));

            //Muzzle offset
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            if (Main.rand.NextFloat() < .2f)
            {
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), perturbedSpeed, ModContent.ProjectileType<NovaBusterProjectile>(), (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti) * 2, knockback, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), perturbedSpeed, ModContent.ProjectileType<NovaBusterProjectile>(), (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            /*
             * TODO Might be good to add a post LC or ML recipe for power cells to avoid having to farm lihzahrds 
             */
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}