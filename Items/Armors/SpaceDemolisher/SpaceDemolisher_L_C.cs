using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.SpaceDemolisher
{
    [AutoloadEquip(EquipType.Legs)]
    public class SpaceDemolisher_L_C : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Space Demolisher Legs");
            Tooltip.SetDefault("\n" +
                "3% Bomb Damage\n" +
                "3% Blast Radius");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .03f;
            player.EE().DamageMulti += .03f;
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