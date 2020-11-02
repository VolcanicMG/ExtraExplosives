using ExtraExplosives.Tiles;
using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class GlowingCrystalItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Crystal");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = true;
            item.maxStack = 99;
            item.consumable = true;
            item.value = 1000;
            item.createTile = ModContent.TileType<GlowingCrystal>();
            item.UseSound = null;
        }

        public override string Texture => "ExtraExplosives/Items/Accessories/AnarchistCookbook/GlowingCompound";
    }
}