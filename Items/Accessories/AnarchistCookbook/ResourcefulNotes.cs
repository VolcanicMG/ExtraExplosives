<<<<<<< HEAD
=======
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class ResourcefulNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resourceful Notes");
<<<<<<< HEAD
            Tooltip.SetDefault("Chicken scratch");
=======
            Tooltip.SetDefault("50% chance to throw a second explosives\n" +
                               "Does not consume a second item\n" +
                               "20% chance to not consume explosives");
>>>>>>> Charlie's-Uploads
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
            modRecipe.AddIngredient(ModContent.ItemType<BombBag>());
            modRecipe.AddIngredient(ModContent.ItemType<MysteryBomb>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
<<<<<<< HEAD
=======
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().BombBag = true;
            player.GetModPlayer<ExtraExplosivesPlayer>().MysteryBomb = true;
        }
>>>>>>> Charlie's-Uploads
    }
}