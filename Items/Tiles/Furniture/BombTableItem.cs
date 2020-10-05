using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombTableItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive Statue");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenTable);
            item.createTile = ModContent.TileType<BombTableTile>();
            item.placeStyle = 0;
        }
    }
}