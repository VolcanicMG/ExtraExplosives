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
            DisplayName.SetDefault("Tunnel-rat Body");
            Tooltip.SetDefault("1% Increased Bomb Damage");

        }

        public override void SetDefaults()
        {
            item.height = 40;
            item.width = 40;
            item.value = Item.buyPrice(0, 0, 0, 55); ;
            item.rare = ItemRarityID.Blue;
            item.defense = 4;
        }


        public override void UpdateEquip(Player player)
        {
            player.EE().DamageMulti += .01f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 35);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}