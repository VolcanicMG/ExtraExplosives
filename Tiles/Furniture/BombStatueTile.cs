using ExtraExplosives.Items.Tiles.Furniture;
using Microsoft.Xna.Framework;
//using On.Terraria.ID;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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
            
            AddMapEntry(new Color(144, 148, 144), this.GetLocalization("MapEntry"));
            TileID.Sets.DisableSmartCursor[Type] = true;
        }
    }
}