using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.DungeonBombard
{
    [AutoloadEquip(EquipType.Legs)]
    public class DungeonBombard_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Bombard Legs");
            Tooltip.SetDefault("\n" +
                "3% Increased Bomb Damage and Blast Radius");
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 50, 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .03f;
            player.EE().DamageMulti += .03f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddIngredient(ItemID.BlueBrick, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}