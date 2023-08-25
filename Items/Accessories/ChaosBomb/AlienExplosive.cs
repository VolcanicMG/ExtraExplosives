using Microsoft.Xna.Framework;
using System.Collections.Generic;
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
            if (Main.player[Item.playerIndexTheItemIsReservedFor].EE().AnarchistCookbook)
            {
                var synergyTooltipLine = new TooltipLine(Mod, "synergyTooltipLine", $"Cookbook Synergy: Bombs ignore gravity");
                synergyTooltipLine.OverrideColor = Color.LightCyan;
                tooltips.Add(synergyTooltipLine);
            }
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = 100000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().AlienExplosive = true;
        }
    }
}