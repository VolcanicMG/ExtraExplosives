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
            DisplayName.SetDefault("Crossed Wires");
            Tooltip.SetDefault("Increases explosive damage by X\n" +
                               "Increases Explosive Critchance by Y");
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
            modRecipe.AddIngredient(ItemID.Gel, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
            modRecipe = new ModRecipe(mod);
            //modRecipe.AddIngredient(ItemID.Oil);
            modRecipe.AddIngredient(ItemID.TinBar, 10);
            modRecipe.AddIngredient(ItemID.Gel, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ExtraExplosivesPlayer>().CrossedWires = true;
        }
    }
}