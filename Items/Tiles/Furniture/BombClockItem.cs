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
            Item.CloneDefaults(ItemID.GrandfatherClock);
            Item.width = 32;
            Item.height = 96;
            Item.createTile = ModContent.TileType<BombClockTile>();
        }
    }
}