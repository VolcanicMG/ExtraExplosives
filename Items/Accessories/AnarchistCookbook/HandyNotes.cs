using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class HandyNotes : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Handy Notes");
            /* Tooltip.SetDefault("Double the initial velocity of thrown explosives\n" +
                               "Thrown explosives stick to walls\n" +
                               "Functions identically to sticky bombs"); */
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
            modRecipe.AddIngredient(ModContent.ItemType<StickyGunpowder>());
            modRecipe.AddIngredient(ModContent.ItemType<LightweightBombshells>());
            modRecipe.AddTile(TileID.TinkerersWorkbench);
            modRecipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().StickyGunpowder = true;
            if (player.EE().LightweightBombshellsActive) player.EE().LightweightBombshells = true;
        }
    }
}