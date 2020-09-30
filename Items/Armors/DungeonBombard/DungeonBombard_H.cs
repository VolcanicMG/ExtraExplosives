using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.DungeonBombard
{
    [AutoloadEquip(EquipType.Head)]
    public class DungeonBombard_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Bombard Helm");
            Tooltip.SetDefault("\n" +
                "2.5% Bomb Damage\n" +
                "2.5% Blast Radius"); ;
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.Blue;
            item.defense = 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DungeonBombard_B>() && legs.type == ModContent.ItemType<DungeonBombard_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "2.5% Bomb Damage\n" +
                "7.5% Blast Radius\n" +
                "10% chance to dodge attack";
            player.EE().RadiusMulti += .075f;
            player.EE().DamageMulti += .025f;
            player.EE().DungeonBombard = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .025f;
            player.EE().DamageMulti += .025f;
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