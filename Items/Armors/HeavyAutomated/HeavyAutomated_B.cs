using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.HeavyAutomated
{
    [AutoloadEquip(EquipType.Body)]
    public class HeavyAutomated_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heavy Automated Bombard Body");
            /* Tooltip.SetDefault("\n" +
                "7% Increased Bomb Damage and 8% Blast Radius\n"); */
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 90, 50);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 19;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .08f;
            player.EE().DamageMulti += .07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}