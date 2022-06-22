using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class AnarchistCookbook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anarchist Cookbook");
            Tooltip.SetDefault("Just owning it could get you arrested\n" +
                               "Allows for modification of most accessories effects");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ExtraExplosivesPlayer mp = Main.player[Item.playerIndexTheItemIsReservedFor].EE();
            if (!mp.AlienExplosive &&
                !mp.LihzahrdFuzeset &&
                !mp.Bombshroom &&
                !mp.SupernaturalBomb)
            {
                var hintTooltipLine = new TooltipLine(Mod, "", $"There still seem to be pages missing");
                hintTooltipLine.OverrideColor = Color.LightSkyBlue;
                //tooltips.Add(hintTooltipLine);
                tooltips.Insert(tooltips.Count - 1, hintTooltipLine);

            }

            var extraTooltip = new TooltipLine(Mod, "", $"Its more than just the sum of its parts");
            extraTooltip.OverrideColor = Color.MediumVioletRed;
            //tooltips.Add(extraTooltip);
            tooltips.Insert(tooltips.Count - 1, extraTooltip);
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = 100000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
            Item.social = false;
        }

        public override void AddRecipes()
        {
            Recipe modRecipe = CreateRecipe();
            modRecipe.AddIngredient(ModContent.ItemType<HandyNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<RandomNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<ResourcefulNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<SafetyNotes>());
            modRecipe.AddIngredient(ModContent.ItemType<UtilityNotes>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ExtraExplosivesPlayer mp = player.EE();
            if (mp.BlastShieldingActive) mp.BlastShielding = true;
            if (mp.BombBagActive) mp.BombBag = true;
            if (mp.CrossedWiresActive) mp.CrossedWires = true;
            if (mp.GlowingCompoundActive) mp.GlowingCompound = true;
            if (mp.LightweightBombshellsActive) mp.LightweightBombshells = true;
            if (mp.MysteryBombActive) mp.MysteryBomb = true;
            if (mp.RandomFuelActive) mp.RandomFuel = true;
            if (mp.ReactivePlatingActive) mp.ReactivePlating = true;
            if (mp.ShortFuseActive) mp.ShortFuse = true;
            if (mp.StickyGunpowderActive) mp.StickyGunpowder = true;
            player.EE().AnarchistCookbook = true;
        }
    }
}