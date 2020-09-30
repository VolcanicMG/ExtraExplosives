using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.CrimsonAnarchy
{
    [AutoloadEquip(EquipType.Body)]
    public class CrimsonAnarchy_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Anarchy Body");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.Blue;
            item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 19);
            recipe.AddIngredient(ItemID.TissueSample, 15);
            recipe.anyIronBar = true;
        }

    }
}