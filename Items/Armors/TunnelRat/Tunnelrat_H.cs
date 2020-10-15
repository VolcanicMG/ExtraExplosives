using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.TunnelRat
{
    [AutoloadEquip(EquipType.Head)]
    public class Tunnelrat_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tunnel-rat Head");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50); ;
            item.rare = ItemRarityID.Blue;
            item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Tunnelrat_B>() && legs.type == ModContent.ItemType<Tunnelrat_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "5% Bomb Damage\n" +
                "5% Blast Radius\n" +
                "10% chance to drop ores twice on bomb explosion (EE bombs only)";
            player.EE().RadiusMulti += .05f;
            player.EE().DamageMulti += .05f;
            player.EE().DropOresTwice = true;
            player.EE().dropChanceOre = .1f;
        }

        public override void UpdateEquip(Player player)
        {
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 15);
            recipe.AddIngredient(ItemID.Silk, 3);
            recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}