using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombStatueItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive Statue");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ArmorStatue);
            Item.createTile = ModContent.TileType<BombStatueTile>();
            Item.placeStyle = 0;
        }
    }
}