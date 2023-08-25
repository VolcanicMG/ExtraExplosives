using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class LihzahrdFuzeset : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lihzahrd Fuzeset");
            Tooltip.SetDefault("'Nothing in the temple is flammable...\n" +
                               "but this sure is.'\n" +
                               "Bombs light enemies on fire");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.player[Item.playerIndexTheItemIsReservedFor].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(Mod, "synergyTooltipLine", $"Cookbook Synergy: Allows full control over fuse length");
                synergyTooltipLine.OverrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().LihzahrdFuzeset = true;
        }
    }
}