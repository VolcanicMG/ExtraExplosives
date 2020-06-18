using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Furniture
{
    public class BombStatueItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive Statue");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ArmorStatue);
            item.createTile = ModContent.TileType<BombStatueTile>();
            item.placeStyle = 0;
        }
    }
}