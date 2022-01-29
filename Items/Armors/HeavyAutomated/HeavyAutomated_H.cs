using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.HeavyAutomated
{
    [AutoloadEquip(EquipType.Head)]
    public class HeavyAutomated_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy Automated Bombard Helm");
            Tooltip.SetDefault("\n" +
                "7% Increased Bomb Damage and 8% Blast Radius\n");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 90, 50);
            item.rare = ItemRarityID.Pink;
            item.defense = 14;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<HeavyAutomated_B>() && legs.type == ModContent.ItemType<HeavyAutomated_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "6% Increased Bomb Damage\n" +
                "6% Increased Blast Radius\n" +
                "7% Increased Damage\n" +
                "8% Increased Critical Strike Chance\n" +
                "Fire out bolts of fire after every explosion";
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .06f;
            player.allDamage += .07f;
            player.EE().ExplosiveCrit += 8;
            player.EE().HeavyBombard = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .08f;
            player.EE().DamageMulti += .07f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}