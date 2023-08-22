using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Asteroid
{
    [AutoloadEquip(EquipType.Body)]
    public class AsteroidMiner_B_O : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Asteroid Miner Body");
            Tooltip.SetDefault("\n" +
                "5% Increased Bomb Damage and " +
                "6% Blast Radius");
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 70, 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.OrichalcumBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}