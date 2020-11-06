using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.CrimsonAnarchy
{
    [AutoloadEquip(EquipType.Head)]
    public class CrimsonAnarchy_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Anarchy Helm");
            Tooltip.SetDefault("2% Increased Bomb Damage");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 1, 50);
            item.rare = ItemRarityID.Blue;
            item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CrimsonAnarchy_B>() && legs.type == ModContent.ItemType<CrimsonAnarchy_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "2% Increased Bomb Damage\n" +
                "10% Increased Blast Radius\n" +
                "Spawn in deadly spikes whenever your bombs blow up";
            player.EE().RadiusMulti += .1f;
            player.EE().DamageMulti += .05f;
            player.EE().Anarchy = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().DamageMulti += .02f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}