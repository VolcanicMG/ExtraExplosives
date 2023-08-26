using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class ChaosBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Bomb");
            /* Tooltip.SetDefault("'This is either a grave mistake or the best idea ever.'\n" +
                               "Bombs inflict Venom and Shadowflame, ignite enemies, and confuse enemies"); */    //The spirits of light and dark have been compressed and refined
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
                                                                                    $"Bombs ignore gravity\n" +
                                                                                    $"Explosions spawn damaging spores\n" +
                                                                                    $"Bombs home in on enemies");
                synergyTooltipLine.OverrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().LihzahrdFuzeset = true;
            player.EE().AlienExplosive = true;
            player.EE().SupernaturalBomb = true;
            player.EE().Bombshroom = true;
            player.EE().AntiGravity = true;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EclecticBomb>());
            recipe.AddIngredient(ModContent.ItemType<WyrdBomb>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}