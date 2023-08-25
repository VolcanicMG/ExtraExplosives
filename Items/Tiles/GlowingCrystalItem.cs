using ExtraExplosives.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items
{
    public class GlowingCrystalItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Crystal");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.value = 1000;
            Item.createTile = ModContent.TileType<GlowingCrystal>();
            Item.UseSound = null;
        }

        public override string Texture => "ExtraExplosives/Items/Accessories/AnarchistCookbook/GlowingCompound";
    }
}