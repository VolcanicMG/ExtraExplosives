using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class WyrdBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wyrd Bomb");
            /* Tooltip.SetDefault("'Truly a strange specimen'\n" +
                               "Bombs inflict Venom & Shadowflame"); */
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
                                                                                    $"Explosions spawn damaging spores\n" +
                                                                                    $"Bombs home in on enemies");
                synergyTooltipLine.OverrideColor = Color.LightCyan;
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bombshroom>());
            recipe.AddIngredient(ModContent.ItemType<SupernaturalBomb>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}