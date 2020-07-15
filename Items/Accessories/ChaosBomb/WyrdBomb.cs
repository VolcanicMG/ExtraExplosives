using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class WyrdBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wyrd Bomb");
            Tooltip.SetDefault("Truly a strange specimen\n" +
                               "Bombs inflict the Venom & Shadowflames Debuff");
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
                                                                                    $"Explosions spawn damaging spores\n" +
                                                                                    $"Bombs home in on enemies");
                synergyTooltipLine.overrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().Bombshroom = true;
            player.EE().SupernaturalBomb = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Bombshroom>());
            recipe.AddIngredient(ModContent.ItemType<SupernaturalBomb>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}