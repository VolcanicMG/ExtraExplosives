using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombTableItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bomb Table");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodenTable);
            Item.createTile = ModContent.TileType<BombTableTile>();
            Item.placeStyle = 0;
        }
    }
}