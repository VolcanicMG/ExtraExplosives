using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Hazard
{
    [AutoloadEquip(EquipType.Head)]
    public class Hazard_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Hazard Demolisher Helm");
            Tooltip.SetDefault("\n" +
                "6% Increased Bomb Damage and Blast Radius\n");
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 80, 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 12;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Hazard_B>() && legs.type == ModContent.ItemType<Hazard_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "6% Increased Bomb Damage\n" +
                "6% Increased Blast Radius\n" +
                "7% Increased Damage\n" +
                "3% Increased Critical Strike Chance\n" +
                "25% chance to drop ores twice on bomb explosion";
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .06f;
            //player.allDamage += .07f;
            player.EE().ExplosiveCrit += 6;
            player.EE().DropOresTwice = true;
            player.EE().dropChanceOre = .25f;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .06f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}