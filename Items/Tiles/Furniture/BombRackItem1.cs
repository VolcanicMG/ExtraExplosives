using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombRackItem1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Rack");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenTable);
            item.createTile = ModContent.TileType<BombRackTile1>();
            item.placeStyle = 0;
        }
    }
}