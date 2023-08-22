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
            Item.width = 30;
            Item.height = 30;
            Item.value = 10000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.social = false;
        }

        public override void AddRecipes()
        {
            Recipe modRecipe = CreateRecipe();
            modRecipe.AddIngredient(ModContent.ItemType<CrossedWires>());
            modRecipe.AddIngredient(ModContent.ItemType<GlowingCompound>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().CrossedWires = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().GlowingCompound = true;
        }
    }
}