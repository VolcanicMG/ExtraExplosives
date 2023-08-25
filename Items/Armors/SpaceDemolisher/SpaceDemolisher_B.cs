using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.SpaceDemolisher
{
    [AutoloadEquip(EquipType.Body)]
    public class SpaceDemolisher_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Palladium Space Demolisher Body");
            /* Tooltip.SetDefault("\n" +
                "4% Increased Bomb Damage and Blast Radius"); */
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 60, 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .04f;
            player.EE().DamageMulti += .04f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}