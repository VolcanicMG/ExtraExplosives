using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.CorruptedAnarchy
{
    [AutoloadEquip(EquipType.Body)]
    public class CorruptedAnarchy_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Corrupted Anarchy Body");
            // Tooltip.SetDefault("2% Increased Bomb Damage");
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 2, 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().DamageMulti += .02f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 19);
            recipe.AddIngredient(ItemID.ShadowScale, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}