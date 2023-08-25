using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.SpaceDemolisher
{
    [AutoloadEquip(EquipType.Head)]
    public class SpaceDemolisher_H_C : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cobalt Space Demolisher Helm");
            /* Tooltip.SetDefault("\n" +
                "3% Increased Bomb Damage and Blast Radius"); */
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 60, 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SpaceDemolisher_B_C>() && legs.type == ModContent.ItemType<SpaceDemolisher_L_C>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "6% Increased Bomb Damage\n" +
                "6% Increased Blast Radius\n" +
                "10% Increased Movement Speed\n" +
                "3% Increased Critical Strike Chance\n" +
                "15% Increased chance to drop ores twice on bomb explosion (EE bombs only)";
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}