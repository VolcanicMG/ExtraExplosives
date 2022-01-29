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
        public override bool CloneNewInstances => true;

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
            item.useStyle = 1;
            item.shootSpeed = 4f;
            item.width = 8;
            item.height = 28;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 40;
            item.useTime = 40;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.damage = 250;
            item.knockBack = 2;
            item.consumable = false;
            item.shoot = ModContent.ProjectileType<InfinityBombProjectile>();
            item.autoReuse = false;
            item.value = 200000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Green;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            damage = (int)(damage * multiplier);
            knockBack = (int)(knockBack * multiplier);
            //Main.NewText(multiplier);
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var multiplerTooltip = new TooltipLine(mod, "Multiplier", $"It has become {multiplier} times more powerful\n" +
                                                                      $"It is {growing}currently growing");
            multiplerTooltip.overrideColor = Color.Crimson;
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
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(Terraria.ID.ItemID.Dynamite, 3996);
            modRecipe.AddTile(TileID.CrystalBall);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
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

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(multiplier)] = multiplier,
                [nameof(growing)] = growing,
            };
        }

        public override void Load(TagCompound tag)
        {
            multiplier = tag.GetDouble(nameof(multiplier));
            enableGrowth = tag.GetBool(nameof(enableGrowth));
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(multiplier);
            writer.Write(enableGrowth);
        }

        public override void NetRecieve(BinaryReader reader)
        {
            multiplier = reader.ReadDouble();
            enableGrowth = reader.ReadBoolean();
        }
    }
}