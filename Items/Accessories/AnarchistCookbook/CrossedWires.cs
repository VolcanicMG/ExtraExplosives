using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ItemID = Terraria.ID.ItemID;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class CrossedWires : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crossed Wires");
            /* Tooltip.SetDefault("Increases explosive damage by 15%\n" +
                               "Increases Explosive Critical chance by 10%"); */
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = 10000;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.social = false;
        }

        public override void AddRecipes()
        {
            Recipe modRecipe = CreateRecipe();
            //modRecipe.AddIngredient(ItemID.Oil);
            modRecipe.AddIngredient(ItemID.CopperBar, 10);
            modRecipe.AddIngredient(ItemID.Gel, 10);
            modRecipe.AddIngredient(ItemID.Wire, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.Register();
            modRecipe = CreateRecipe();
            //modRecipe.AddIngredient(ItemID.Oil);
            modRecipe.AddIngredient(ItemID.TinBar, 10);
            modRecipe.AddIngredient(ItemID.Gel, 10);
            modRecipe.AddIngredient(ItemID.Wire, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().CrossedWires = true;
        }
    }
}