using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.HeavyAutomated
{
    [AutoloadEquip(EquipType.Legs)]
    public class HeavyAutomated_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy Automated Bombard Legs");
            Tooltip.SetDefault("\n" +
                "6% Bomb Damage and 8% Blast Radius\n" +
                "10% movement speed");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 90, 50);
            item.rare = ItemRarityID.Pink;
            item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .08f;
            player.EE().DamageMulti += .06f;
            player.moveSpeed += .1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}