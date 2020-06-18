using ExtraExplosives.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.Items.Furniture
{
    public class BombBannerItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Banner");
        }

        public override void SetDefaults() 
        {
            item.width = 10;
            item.height = 24;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.createTile = TileType<BombBannerTile>();
            item.placeStyle = 0;
        }
    }
}