using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.BombardierClassAccessories
{
    public class RavenousBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ravenous Bomb");
            Tooltip.SetDefault("It looks hungry\n" +
                               "Gives explosions lifesteal");
        }

        public override void SetDefaults()
        {
            item.accessory = true;
            item.value = 5000;
            item.rare = ItemRarityID.Orange;
            item.consumable = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().RavenousBomb = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<FleshyBlastingCaps>(), 1);
            recipe.AddIngredient(ItemID.PlanteraMask, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}