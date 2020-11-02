using System.Collections.Generic;
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
            DisplayName.SetDefault("Corrupted Anarchy Helm");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 1, 50);
            item.rare = ItemRarityID.Blue;
            item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CorruptedAnarchy_B>() && legs.type == ModContent.ItemType<CorruptedAnarchy_L>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "\n" +
                "5% Bomb Damage\n" +
                "10% Blast Radius\n" +
                "Spawn in deadly spikes whenever your bombs blow up";
            player.EE().RadiusMulti += .1f;
            player.EE().DamageMulti += .05f;
            player.EE().Anarchy = true;
        }

        public override void UpdateEquip(Player player)
        {
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}