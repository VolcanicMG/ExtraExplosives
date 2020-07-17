using Terraria.ID;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class CrossedWires : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crossed Wires");
            Tooltip.SetDefault("The only time you should cross the streams");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 10000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            //modRecipe.AddIngredient(ItemID.Oil);
            modRecipe.AddIngredient(ItemID.CopperBar, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
            modRecipe = new ModRecipe(mod);
            //modRecipe.AddIngredient(ItemID.Oil);
            modRecipe.AddIngredient(ItemID.TinBar, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }
    }
}