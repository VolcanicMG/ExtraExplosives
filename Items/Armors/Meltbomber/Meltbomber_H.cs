using System.Collections.Generic;
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
            DisplayName.SetDefault("Meltbomber Helm");
            Tooltip.SetDefault("\n" +
                "2.5% Bomb Damage\n" +
                "2.5% Blast Radius"); ;
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 18;
            item.value = Item.buyPrice(0, 0, 0, 50);
            item.rare = ItemRarityID.Blue;
            item.defense = 7;
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
        }

    }
}