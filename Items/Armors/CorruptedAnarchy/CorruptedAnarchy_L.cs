using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.CorruptedAnarchy
{
    [AutoloadEquip(EquipType.Legs)]
    public class CorruptedAnarchy_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Anarchy Legs");
            Tooltip.SetDefault("2% Increased Bomb Damage");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 1, 50);
            item.rare = ItemRarityID.Blue;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().DamageMulti += .02f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}