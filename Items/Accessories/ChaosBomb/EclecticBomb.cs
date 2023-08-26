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
            // DisplayName.SetDefault("Eclectic Bomb");
            /* Tooltip.SetDefault("'Antiquity and modernity combined'\n" +
                               "Bombs light enemies on fire & confuse them"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = 10000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.player[Item.playerIndexTheItemIsReservedFor].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(Mod, "synergyTooltipLine", $"Cookbook Synergies:\n" +
                                                                                    $"Allows full control over fuse length\n" +
                                                                                    $"Bombs ignore gravity");
                synergyTooltipLine.OverrideColor = Color.LightCyan;
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LihzahrdFuzeset>());
            recipe.AddIngredient(ModContent.ItemType<AlienExplosive>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}