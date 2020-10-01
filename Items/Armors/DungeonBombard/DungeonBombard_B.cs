using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.DungeonBombard
{
    [AutoloadEquip(EquipType.Body)]
    public class DungeonBombard_B : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Bombard Body");
            Tooltip.SetDefault("\n" +
                "2.5% Bomb Damage and Blast Radius");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 50, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .025f;
            player.EE().DamageMulti += .025f;
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