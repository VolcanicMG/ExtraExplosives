using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Hazard
{
    [AutoloadEquip(EquipType.Body)]
    public class Hazard_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Hazard Demolisher Body");
            Tooltip.SetDefault("\n" +
                "6% Bomb Damage and Blast Radius\n");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 80, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .06f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}