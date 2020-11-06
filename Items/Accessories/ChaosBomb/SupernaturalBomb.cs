using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.ChaosBomb
{
    public class SupernaturalBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supernatural Bomb");
            Tooltip.SetDefault("'It whispers to me.'\n" +
                               "Bombs inflict Shadowflame");    //Infused with the essence of the ethereal plane
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.player[item.owner].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(mod, "synergyTooltipLine", $"Cookbook Synergy: Bombs home in on enemies");
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
            player.EE().SupernaturalBomb = true;
        }
    }
}