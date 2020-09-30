using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Armors.Meltbomber
{
    [AutoloadEquip(EquipType.Legs)]
    public class Meltbomber_L : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meltbomber Legs");
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