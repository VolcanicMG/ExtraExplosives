using System.Collections.Generic;
using System.Linq;
using ExtraExplosives.Projectiles;
using ExtraExplosives.Projectiles.Weapons.NovaBuster;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class NovaBuster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Buster");
            Tooltip.SetDefault("20% Chance to double blast radius (InDev)");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useAmmo = AmmoID.Rocket;
            item.width = 94;
            item.height = 34;
            item.shoot = 134;
            item.UseSound = SoundID.Item11;
            item.damage = 350;
            item.shootSpeed = 16f;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 15, 0, 50);
            item.knockBack = 8;
            item.rare = ItemRarityID.Red;
            item.ranged = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine ColoredTooltip = new TooltipLine(mod, "ExtraTooltip", $"The death of a star\n" +
                                                                              $"Compressed into a single weapon");
            ColoredTooltip.overrideColor = Color.Chartreuse;
            tooltips.Add(ColoredTooltip);
            
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.text = damageValue + " explosive " + damageWord;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY), ModContent.ProjectileType<NovaBusterProjectile>(), (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockBack, player.whoAmI);

            return false;
        }

        public override void AddRecipes()
        {
            /*
             * TODO Might be good to add a post LC or ML recipe for power cells to avoid having to farm lihzahrds 
             */
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}