using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class NukePosterItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nuke the Corruption Poster");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.TreasureMap);
            Item.createTile = ModContent.TileType<NukePosterTile>();
            Item.placeStyle = 0;
        }
    }
}