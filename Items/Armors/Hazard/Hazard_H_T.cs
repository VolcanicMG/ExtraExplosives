using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Hazard
{
    [AutoloadEquip(EquipType.Head)]
    public class Hazard_H_T : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Hazard Demolisher Helm");
            Tooltip.SetDefault("\n" +
                "7% Increased Bomb Damage and Blast Radius\n");
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 80, 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 14;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Hazard_B_T>() && legs.type == ModContent.ItemType<Hazard_L_T>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "6% Increased Bomb Damage\n" +
                "6% Increased Blast Radius\n" +
                "7% Increased Damage\n" +
                "6% Increased Critical Strike Chance\n" +
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
            player.EE().RadiusMulti += .07f;
            player.EE().DamageMulti += .07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}