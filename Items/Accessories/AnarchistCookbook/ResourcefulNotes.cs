using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class ResourcefulNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Resourceful Notes");
            /* Tooltip.SetDefault("50% chance to throw a second explosives\n" +
                               "Does not consume a second item\n" +
                               "20% chance to not consume explosives"); */
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
            modRecipe.AddIngredient(ModContent.ItemType<BombBag>());
            modRecipe.AddIngredient(ModContent.ItemType<MysteryBomb>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BombBag = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().MysteryBomb = true;
        }
    }
}