using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.TunnelRat
{
    [AutoloadEquip(EquipType.Legs)]
    public class Tunnelrat_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tunnel-rat Legs");
        }

        public override void SetDefaults()
        {
            item.height = 40;
            item.width = 40;
            item.value = Item.buyPrice(0, 0, 0, 55); ;
            item.rare = ItemRarityID.Blue;
            item.defense = 2;
        }


        public override void UpdateEquip(Player player)
        {
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 13);
            recipe.AddIngredient(ItemID.Silk, 2);
            recipe.anyIronBar = true;
        }

    }
}