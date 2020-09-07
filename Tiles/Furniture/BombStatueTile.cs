using ExtraExplosives.Items.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombStatueTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Bomb Statue");
            AddMapEntry(new Color(144, 148, 144), name);
            dustType = 11;
            disableSmartCursor = true;
        }
        
        public override void KillMultiTile(int i, int j, int frameX, int frameY) 
        {
            Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<BombStatueItem>());
        }
    }
}