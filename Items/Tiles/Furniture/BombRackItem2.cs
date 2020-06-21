using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombRackItem2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynamite Rack");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenTable);
            item.createTile = ModContent.TileType<BombRackTile2>();
            item.placeStyle = 0;
        }
    }
}