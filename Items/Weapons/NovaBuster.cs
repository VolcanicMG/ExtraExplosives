using System.Collections.Generic;
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
            item.useStyle = 5;
            item.autoReuse = true;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useAmmo = AmmoID.Rocket;
            item.width = 94;
            item.height = 34;
            item.shoot = 134;
            item.UseSound = SoundID.Item11;
            item.damage = 45;
            item.shootSpeed = 5f;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 15, 0, 50);
            item.knockBack = 8;
            item.rare = ItemRarityID.Yellow;
            item.ranged = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var ColoredTooltip = new TooltipLine(mod, "ExtraTooltip", $"The death of a star\n" +
                                                                      $"Compressed into a single weapon");
            ColoredTooltip.overrideColor = Color.Chartreuse;
            tooltips.Add(ColoredTooltip);
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
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 5);
        }
    }
}