using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class StickyGunpowder : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sticky Gunpowder");
            /* Tooltip.SetDefault("Thrown explosives stick to walls\n" +
                               "Functions identically to sticky bombs"); */
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 18;
            Item.value = 1000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.social = false;
        }

        public override void AddRecipes()
        {
            Recipe modRecipe = CreateRecipe();
            modRecipe.AddIngredient(ItemID.Gel, 10);
            modRecipe.AddIngredient(ItemID.ExplosivePowder, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().StickyGunpowder = true;
        }
    }
}