using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
            Tooltip.SetDefault("Nothing in the temple is flammable...\n" +
                               "but this sure is.\n" +
                               "Bombs inflict the On Fire Debuff");
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.player[item.owner].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(mod, "synergyTooltipLine", $"Cookbook Synergy: Allows full control over fuse length");
                synergyTooltipLine.overrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
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
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().LihzahrdFuzeset = true;
        }
    }
}