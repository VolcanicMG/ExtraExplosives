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
            // DisplayName.SetDefault("Tunnel-rat Head");
            // Tooltip.SetDefault("1% Increased Bomb Damage");
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 0, 50); ;
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Tunnelrat_B>() && legs.type == ModContent.ItemType<Tunnelrat_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "2% Increased Bomb Damage\n" +
                "5% Increased Blast Radius\n" +
                "10% chance to drop ores twice on bomb explosion (EE bombs only)";
            player.EE().RadiusMulti += .05f;
            player.EE().DamageMulti += .02f;
            player.EE().DropOresTwice = true;
            player.EE().dropChanceOre = .1f;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().DamageMulti += .01f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 15);
            recipe.AddIngredient(ItemID.Silk, 3);
            //recipe.anyIronBar = true;
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}