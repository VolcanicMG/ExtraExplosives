using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class BombClockItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynamite Clock");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.GrandfatherClock);
            item.width = 32;
            item.height = 96;
            item.createTile = ModContent.TileType<BombClockTile>();
        }
    }
}