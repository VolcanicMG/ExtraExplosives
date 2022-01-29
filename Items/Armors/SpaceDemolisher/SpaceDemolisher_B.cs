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
            DisplayName.SetDefault("Palladium Space Demolisher Body");
            Tooltip.SetDefault("\n" +
                "4% Increased Bomb Damage and Blast Radius");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 60, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .04f;
            player.EE().DamageMulti += .04f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalladiumBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}