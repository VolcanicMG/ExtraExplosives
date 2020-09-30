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
            DisplayName.SetDefault("Titanium  Hazard Demolisher Legs");
            Tooltip.SetDefault("\n" +
                "6% Bomb Damage\n" +
                "6% Blast Radius\n" +
                "5% movement speed");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .06f;
            player.moveSpeed += .05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddIngredient(ItemID.BlueBrick, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}