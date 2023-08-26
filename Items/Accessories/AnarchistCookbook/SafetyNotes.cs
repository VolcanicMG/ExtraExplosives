using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class SafetyNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Safety Notes");
            /* Tooltip.SetDefault("Prevents self damage from friendly explosives\n" +
                               "Increased damage output by 10%"); */
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
            modRecipe.AddIngredient(ModContent.ItemType<BlastShielding>());
            modRecipe.AddIngredient(ModContent.ItemType<ReactivePlating>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BlastShielding = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().ReactivePlating = true;
        }
    }
}