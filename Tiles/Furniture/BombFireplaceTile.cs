using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombFireplaceTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            
            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            
            TileID.Sets.DisableSmartCursor[Type] = true;
            
            AnimationFrameHeight = 38;

            Lighting.AddLight(Vector2.Zero, 210, 140, 100);
            Lighting.Brightness(100, 100);

            AddMapEntry(new Color(255, 55, 55));
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.9f;
            g = 0.9f;
            b = 0.9f;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            // Spend 17 ticks on each of 5 frames, looping
            frameCounter++;
            if (++frameCounter >= 17)	// Time spent on each frame
            {
                frameCounter = 0;
                frame = ++frame % 5;	// How many frames
            }
        }
    }
}