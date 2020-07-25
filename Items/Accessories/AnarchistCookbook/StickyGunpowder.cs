using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories.AnarchistCookbook
{
    public class StickyGunpowder : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sticky Gunpowder");
            Tooltip.SetDefault("Thrown explosives stick to walls\n" +
                               "Functions identically to sticky bombs");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 18;
            item.value = 1000;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
            item.social = false;
        }

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ItemID.Gel, 10);
            modRecipe.AddIngredient(ItemID.ExplosivePowder, 10);
            modRecipe.AddTile(TileID.Anvils);
            modRecipe.SetResult(this);
            modRecipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.EE().StickyGunpowder = true;
        }
    }
}