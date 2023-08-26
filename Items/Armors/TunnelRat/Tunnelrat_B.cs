using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.TunnelRat
{
    [AutoloadEquip(EquipType.Body)]
    public class Tunnelrat_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tunnel-rat Body");
            // Tooltip.SetDefault("1% Increased Bomb Damage");

        }

        public override void SetDefaults()
        {
            Item.height = 40;
            Item.width = 40;
            Item.value = Item.buyPrice(0, 0, 0, 55); ;
            Item.rare = ItemRarityID.Blue;
            Item.defense = 4;
        }


        public override void UpdateEquip(Player player)
        {
            player.EE().DamageMulti += .01f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 35);
            recipe.AddIngredient(ItemID.Silk, 5);
            //recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}