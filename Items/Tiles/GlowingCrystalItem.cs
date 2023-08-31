using ExtraExplosives.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class GlowingCrystalItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<GlowingCrystal>());
            Item.width = 16;
            Item.height = 16;
            Item.value = 1000;
        }

        public override string Texture => "ExtraExplosives/Items/Accessories/AnarchistCookbook/GlowingCompound";
    }
}