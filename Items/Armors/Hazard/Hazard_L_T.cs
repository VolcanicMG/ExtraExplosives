using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Hazard
{
    [AutoloadEquip(EquipType.Legs)]
    public class Hazard_L_T : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Hazard Demolisher Legs");
            Tooltip.SetDefault("\n" +
                "7% Increased Bomb Damage and Blast Radius\n" +
                "6% Increased Movement Speed");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 80, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .07f;
            player.EE().DamageMulti += .07f;
            player.moveSpeed += .06f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}