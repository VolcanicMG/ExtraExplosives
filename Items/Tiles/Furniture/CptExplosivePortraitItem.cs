using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class CptExplosivePortraitItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive Portrait");
        }

        public override void SetDefaults()
        {
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = true;
            item.maxStack = 99;
            item.consumable = true;
            item.value = 1000;
            item.width = 48;
            item.height = 50;
            item.createTile = ModContent.TileType<CptExplosivePortraitTile>();
        }
    }
}