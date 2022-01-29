using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Hazard
{
    [AutoloadEquip(EquipType.Body)]
    public class Hazard_B_T : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Hazard Demolisher Body");
            Tooltip.SetDefault("\n" +
                "7% Increased Bomb Damage and Blast Radius\n");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 80, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .07f;
            player.EE().DamageMulti += .07f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}