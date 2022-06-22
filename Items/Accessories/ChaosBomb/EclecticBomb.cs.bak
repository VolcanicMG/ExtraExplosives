using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class EclecticBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eclectic Bomb");
            Tooltip.SetDefault("'Antiquity and modernity combined'\n" +
                               "Bombs light enemies on fire & confuse them");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
            item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.player[item.owner].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(mod, "synergyTooltipLine", $"Cookbook Synergies:\n" +
                                                                                    $"Allows full control over fuse length\n" +
                                                                                    $"Bombs ignore gravity");
                synergyTooltipLine.overrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().LihzahrdFuzeset = true;
            player.EE().AlienExplosive = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<LihzahrdFuzeset>());
            recipe.AddIngredient(ModContent.ItemType<AlienExplosive>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}