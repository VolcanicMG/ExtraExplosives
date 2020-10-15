using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.SpaceDemolisher
{
    [AutoloadEquip(EquipType.Head)]
    public class SpaceDemolisher_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Space Demolisher Helm");
            Tooltip.SetDefault("\n" +
                "3% Bomb Damage and Blast Radius");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 60, 50);
            item.rare = ItemRarityID.LightRed;
            item.defense = 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SpaceDemolisher_B>() && legs.type == ModContent.ItemType<SpaceDemolisher_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "6% Bomb Damage\n" +
                "6% Blast Radius\n" +
                "10% movement speed\n" +
                "3% critical strike chance\n" +
                "15% chance to drop ores twice on bomb explosion (EE bombs only)";
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .06f;
            player.moveSpeed += .1f;
            player.EE().ExplosiveCrit += 3;
            player.EE().DropOresTwice = true;
            player.EE().dropChanceOre = .15f;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .03f;
            player.EE().DamageMulti += .03f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalladiumBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}