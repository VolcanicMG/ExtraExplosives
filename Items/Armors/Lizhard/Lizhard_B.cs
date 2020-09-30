using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Lizhard
{
    [AutoloadEquip(EquipType.Body)]
    public class Lizhard_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lizhard Bombard Body");
            Tooltip.SetDefault("\n" +
                "8% Bomb Damage\n" +
                "8% Blast Radius");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.Lime;
            item.defense = 23;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .08f;
            player.EE().DamageMulti += .08f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 15);
            recipe.AddIngredient(ItemID.BlueBrick, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}