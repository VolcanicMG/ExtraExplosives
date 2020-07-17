<<<<<<< HEAD
=======
using Terraria;
>>>>>>> Charlie's-Uploads
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class HandyNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Handy Notes");
<<<<<<< HEAD
            Tooltip.SetDefault("Chicken scratch");
=======
            Tooltip.SetDefault("Double the initial velocity of thrown explosives\n" +
                               "Thrown explosives stick to walls\n" +
                               "Functions identically to sticky bombs");
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
            modRecipe.AddIngredient(ModContent.ItemType<StickyGunpowder>());
            modRecipe.AddIngredient(ModContent.ItemType<LightweightBombshells>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
<<<<<<< HEAD
=======
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().StickyGunpowder = true;
            if(player.EE().LightweightBombshellsActive)player.EE().LightweightBombshells = true;
        }
>>>>>>> Charlie's-Uploads
    }
}