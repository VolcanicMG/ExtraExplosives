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
            modRecipe.AddIngredient(ModContent.ItemType<RandomFuel>());
            modRecipe.AddIngredient(ModContent.ItemType<ShortFuse>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().RandomFuel = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().ShortFuse = true;
        }
    }
}