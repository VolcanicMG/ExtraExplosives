using ExtraExplosives.Items.Tiles.Furniture;
using Microsoft.Xna.Framework;
//using On.Terraria.ID;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombStatueTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Bomb Statue");
            AddMapEntry(new Color(144, 148, 144), name);
            DustType = 11;
            TileID.Sets.DisableSmartCursor = new[] { true };
            //disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            // TODO Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<BombStatueItem>());
        }
    }
}