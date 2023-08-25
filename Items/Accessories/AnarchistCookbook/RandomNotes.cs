using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class RandomNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Random Notes");
            Tooltip.SetDefault("Makes explosives detonate twice\n" +
                               "Halves fuse length of explosives");
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
            modRecipe.AddIngredient(ModContent.ItemType<RandomFuel>());
            modRecipe.AddIngredient(ModContent.ItemType<ShortFuse>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().RandomFuel = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().ShortFuse = true;
        }
    }
}