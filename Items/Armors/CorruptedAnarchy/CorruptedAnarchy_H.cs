using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.CorruptedAnarchy
{
    [AutoloadEquip(EquipType.Head)]
    public class CorruptedAnarchy_H : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Corrupted Anarchy Helm");
            // Tooltip.SetDefault("2% Increased Bomb Damage");
        }

        public override void SetDefaults()
        {
            Item.height = 18;
            Item.width = 18;
            Item.value = Item.buyPrice(0, 0, 1, 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CorruptedAnarchy_B>() && legs.type == ModContent.ItemType<CorruptedAnarchy_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "2% Increased Bomb Damage\n" +
                "10% Increased Blast Radius\n" +
                "Spawn in deadly spikes whenever your bombs blow up";
            player.EE().RadiusMulti += .1f;
            player.EE().DamageMulti += .02f;
            player.EE().Anarchy = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.EE().DamageMulti += .02f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}