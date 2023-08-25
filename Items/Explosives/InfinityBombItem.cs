using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExtraExplosives.Items.Explosives
{
    public class InfinityBombItem : ExplosiveItem
    {
        protected override bool CloneNewInstances => true;

        public double multiplier = 1;    // Starts at one, each instance will slowly grow

        public bool enableGrowth = true;

        public string growing = "";
        // This will either be based on uses or time alive idk yet
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Bomb");
            Tooltip.SetDefault("'No wait it gets better'\n" +
                               "Right Click to disable its growth");

        }

        public override void SafeSetDefaults()
        {
            //item.CloneDefaults(167);
            Item.useStyle = 1;
            Item.shootSpeed = 4f;
            Item.width = 8;
            Item.height = 28;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.damage = 250;
            Item.knockBack = 2;
            Item.consumable = false;
            Item.shoot = ModContent.ProjectileType<InfinityBombProjectile>();
            Item.autoReuse = false;
            Item.value = 200000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Green;
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            damage = (int)(damage * multiplier);
            knockBack = (int)(knockBack * multiplier);
            //Main.NewText(multiplier);
            return true;
        }
        */

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var multiplerTooltip = new TooltipLine(Mod, "Multiplier", $"It has become {multiplier} times more powerful\n" +
                                                                      $"It is {growing}currently growing");
            multiplerTooltip.OverrideColor = Color.Crimson;
            tooltips.Add(multiplerTooltip);
        }

        public override bool CanUseItem(Player player)
        {
            if (enableGrowth)
            {
                multiplier += 0.001f;
                multiplier = Math.Round(multiplier, 4);
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe modRecipe = CreateRecipe();
            modRecipe.AddIngredient(Terraria.ID.ItemID.Dynamite, 3996);
            modRecipe.AddTile(TileID.CrystalBall);
            modRecipe.Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            if (Main.mouseRight && Main.mouseRightRelease)
            {
                enableGrowth = (enableGrowth) ? false : true;
                growing = (enableGrowth) ? "" : "not ";
                Main.NewText($"[c/FF00000:{growing}growing]");
            }
            return false;
        }

        /*public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound #1#
        {
            return new TagCompound
            {
                [nameof(multiplier)] = multiplier,
                [nameof(growing)] = growing,
            };
        }*/

        public override void LoadData(TagCompound tag)
        {
            multiplier = tag.GetDouble(nameof(multiplier));
            enableGrowth = tag.GetBool(nameof(enableGrowth));
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(multiplier);
            writer.Write(enableGrowth);
        }

        public override void NetReceive(BinaryReader reader)
        {
            multiplier = reader.ReadDouble();
            enableGrowth = reader.ReadBoolean();
        }
    }
}