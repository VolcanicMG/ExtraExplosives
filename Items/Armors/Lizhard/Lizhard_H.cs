using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Lizhard
{
    [AutoloadEquip(EquipType.Head)]
    public class Lizhard_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lizhard Bombard Helm");
            /* Tooltip.SetDefault("\n" +
                "10% Increased Bomb Damage and 8% Blast Radius\n"); */
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 1, 0, 50);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 16;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Lizhard_B>() && legs.type == ModContent.ItemType<Lizhard_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "8% Increased Bomb Damage\n" +
                "6% Increased Blast Radius\n" +
                "8% Increased Critical Strike Chance\n" +
                "Press " + ExtraExplosives.TriggerLizhard.GetAssignedKeys(InputMode.Keyboard)[0] + " to fire a spread of 7 sun rockets \n" +
                "10s Cooldown";
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .08f;
            player.EE().ExplosiveCrit += 8;
            player.EE().Lizhard = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .08f;
            player.EE().DamageMulti += .1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}