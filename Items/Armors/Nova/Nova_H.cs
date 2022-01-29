using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Nova
{
    [AutoloadEquip(EquipType.Head)]
    public class Nova_H : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Bombard Helm");
            Tooltip.SetDefault("\n" +
                "10% Increased Bomb Damage and Blast Radius\n");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.Red;
            item.defense = 26;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Nova_B>() && legs.type == ModContent.ItemType<Nova_L>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "\n" +
                "10% Increased Damage and Critical Strike Chance\n" +
                "Press " + ExtraExplosives.TriggerNovaBomb.GetAssignedKeys(InputMode.Keyboard)[0].ToString() + " to trigger a localized explosion on your character that \nwill knock enemies away from you and deal decent damage. 10s cooldown";

            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .08f;
            player.allDamage += .07f;
            player.EE().ExplosiveCrit += 8;
            player.EE().Nova = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .1f;
            player.EE().DamageMulti += .1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 2);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}