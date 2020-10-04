using System.Collections.Generic;
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
            DisplayName.SetDefault("Lizhard Bombard Helm");
            Tooltip.SetDefault("\n" +
                "10% Bomb Damage and 8% Blast Radius\n");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 1, 0, 50);
            item.rare = ItemRarityID.Lime;
            item.defense = 16;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Lizhard_B>() && legs.type == ModContent.ItemType<Lizhard_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "8% Bomb Damage\n" +
                "6% Blast Radius\n" +
                "7% damage\n" +
                "8% critical strike chance\n" +
                "Press "+ ExtraExplosives.TriggerLizhard.GetAssignedKeys(InputMode.Keyboard)[0] + " to fire a spread of 6 sun rockets \n 10s Cooldown";
            player.EE().RadiusMulti += .06f;
            player.EE().DamageMulti += .08f;
            player.allDamage += .07f;
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}