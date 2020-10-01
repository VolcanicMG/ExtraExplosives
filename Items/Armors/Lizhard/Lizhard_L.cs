using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Lizhard
{
    [AutoloadEquip(EquipType.Legs)]
    public class Lizhard_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lizhard Bombard Legs");
            Tooltip.SetDefault("\n" +
                "8% Bomb Damage and Blast Radius\n" +
                "5% movement speed");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 1, 0, 50);
            item.rare = ItemRarityID.Lime;
            item.defense = 16;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .08f;
            player.EE().DamageMulti += .08f;
            player.moveSpeed += .05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}