using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Nova
{
    [AutoloadEquip(EquipType.Body)]
    public class Nova_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Bombard Body");
            Tooltip.SetDefault("\n" +
                "10% Bomb Damage and Blast Radius");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.Red;
            item.defense = 36;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .1f;
            player.EE().DamageMulti += .1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 15);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 10);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}