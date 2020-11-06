using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class AlienExplosive : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alien Explosive");
            Tooltip.SetDefault("Bombs confuse enemies");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.player[item.owner].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(mod, "synergyTooltipLine", $"Cookbook Synergy: Bombs ignore gravity");
                synergyTooltipLine.overrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
        }
        
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.value = 100000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
            item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().AlienExplosive = true;
        }
    }
}