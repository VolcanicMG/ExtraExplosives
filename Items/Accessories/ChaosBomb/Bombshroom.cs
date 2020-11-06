using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class Bombshroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bombshroom");
            Tooltip.SetDefault("'It's an explosive mushroom, what more could you want?'\n" +
                               "Bombs inflict Venom");
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.player[item.owner].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(mod, "synergyTooltipLine", $"Cookbook Synergy: Explosions spawn damaging spores");
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
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().Bombshroom = true;
        }
    }
}