using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Nova
{
    [AutoloadEquip(EquipType.Legs)]
    public class Nova_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Bombard Legs");
            Tooltip.SetDefault("\n" +
                "10% Increased Bomb Damage and Blast Radius\n" +
                "15% Increased Movement Speed");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.Red;
            item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .1f;
            player.EE().DamageMulti += .1f;
            player.moveSpeed += .15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 2);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}