using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class BombardsLaurels : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bombard's Laurels");
            Tooltip.SetDefault("Increases Area of Effect of all Explosives by 80%");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.value = 5000;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().BombersHat = true;
            player.EE().CertificateOfDemolition = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BombersCap>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CertificateOfDemolition>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}