<<<<<<< HEAD
=======
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class AnarchistCookbook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anarchist Cookbook");
<<<<<<< HEAD
            Tooltip.SetDefault("Just owning it could get you arrested");
=======
            Tooltip.SetDefault("Just owning it could get you arrested\n" +
                               "Allows for modification of\n" +
                               "most accessories effects");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ExtraExplosivesPlayer mp = Main.player[item.owner].EE();
            if (!mp.AlienExplosive &&
                !mp.LihzahrdFuzeset &&
                !mp.Bombshroom &&
                !mp.SupernaturalBomb)
            {
                var hintTooltipLine = new TooltipLine(mod, "hintTooltipLine", $"There still seem to be pages missing");
                hintTooltipLine.overrideColor = Color.LightSkyBlue;
                tooltips.Add(hintTooltipLine);
            }
            var extraTooltip = new TooltipLine(mod, "extraTooltip", $"Its more than just the sum of its parts");
            extraTooltip.overrideColor = Color.MediumVioletRed;
            tooltips.Add(extraTooltip);
>>>>>>> Charlie's-Uploads
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.value = 100000;
            item.maxStack = 1;
<<<<<<< HEAD
            item.rare = ItemRarityID.Cyan;
=======
            item.rare = ItemRarityID.Lime;
>>>>>>> Charlie's-Uploads
            item.accessory = true;
            item.social = false;
        }

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ModContent.ItemType<HandyNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<RandomNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<ResourcefulNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<SafetyNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<UtilityNotes>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
<<<<<<< HEAD
=======
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ExtraExplosivesPlayer mp = player.EE();
            if(mp.BlastShieldingActive)mp.BlastShielding = true;
            if(mp.BombBagActive)mp.BombBag = true;
            if(mp.CrossedWiresActive)mp.CrossedWires = true;
            if(mp.GlowingCompoundActive)mp.GlowingCompound = true;
            if(mp.LightweightBombshellsActive)mp.LightweightBombshells = true;
            if(mp.MysteryBombActive)mp.MysteryBomb = true;
            if(mp.RandomFuelActive)mp.RandomFuel = true;
            if(mp.ReactivePlatingActive)mp.ReactivePlating = true;
            if(mp.ShortFuseActive)mp.ShortFuse = true;
            if(mp.StickyGunpowderActive)mp.StickyGunpowder = true;
            player.EE().AnarchistCookbook = true;
        }
>>>>>>> Charlie's-Uploads
    }
}