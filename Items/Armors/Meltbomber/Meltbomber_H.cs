using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Meltbomber
{
    [AutoloadEquip(EquipType.Head)]
    public class Meltbomber_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Meltbomber Helm");
            /* Tooltip.SetDefault("\n" +
                "2.5% Increased Bomb Damage and Blast Radius"); */
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 10, 50);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<Meltbomber_B>() && legs.type == ModContent.ItemType<Meltbomber_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.EE().MeltbomberFire = true;
            player.setBonus = "\n" +
                "2.5% Bomb Damage\n" +
                "2.5% Blast Radius\n" +
                "Bombs Ignite Enemies";
            player.EE().RadiusMulti += .025f;
            player.EE().DamageMulti += .025f;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().RadiusMulti += .025f;
            player.EE().DamageMulti += .025f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}