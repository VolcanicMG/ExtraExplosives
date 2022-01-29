using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class UtilityNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Utility Notes");
            Tooltip.SetDefault("10% Increase to Damage and Crit Chance\n" +
                               "Explosives leave behind a glowing aura");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
            item.accessory = true;
            item.social = false;
        }

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ModContent.ItemType<CrossedWires>());
            modRecipe.AddIngredient(ModContent.ItemType<GlowingCompound>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().CrossedWires = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().GlowingCompound = true;
        }
    }
}