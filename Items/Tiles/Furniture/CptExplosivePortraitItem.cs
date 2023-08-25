using ExtraExplosives.Tiles.Furniture;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Tiles.Furniture
{
    public class CptExplosivePortraitItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive Portrait");
        }

        public override void SetDefaults()
        {
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.value = 1000;
            Item.width = 48;
            Item.height = 50;
            Item.createTile = ModContent.TileType<CptExplosivePortraitTile>();
        }
    }
}