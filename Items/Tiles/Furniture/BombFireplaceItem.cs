using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombFireplaceItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bomb Fireplace");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Fireplace);
            Item.createTile = ModContent.TileType<BombFireplaceTile>();
            Item.placeStyle = 0;
        }
    }
}