using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class ChaosBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Bomb");
            Tooltip.SetDefault("This is either a grave mistake or the best idea ever.\n" +
                               "Bombs inflict the On Fire, Confused, Venon, & Shadowflames Debuff");    //The spirits of light and dark have been compressed and refined
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
                                                                                    $"Bombs ignore gravity\n" +
                                                                                    $"Explosions spawn damaging spores\n" +
                                                                                    $"Bombs home in on enemies");
                synergyTooltipLine.overrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().LihzahrdFuzeset = true;
            player.EE().AlienExplosive = true;
            player.EE().SupernaturalBomb = true;
            player.EE().Bombshroom = true;
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<EclecticBomb>());
            recipe.AddIngredient(ModContent.ItemType<WyrdBomb>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}