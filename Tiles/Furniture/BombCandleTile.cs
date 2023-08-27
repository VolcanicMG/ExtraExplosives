using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombCandleTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 20 };
            TileObjectData.newTile.CoordinateWidth = 12;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.DrawYOffset = -2;
            TileObjectData.addTile(Type);
            
            TileID.Sets.DisableSmartCursor[Type] = true;

            AnimationFrameHeight = 22;
            DustType = DustID.RedTorch;
            

            // TODO Check
            Lighting.AddLight(Vector2.Zero, 210, 140, 100);
            Lighting.Brightness(100, 100);
            
            AddMapEntry(new Color(255, 55, 55), this.GetLocalization("MapEntry"));
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            /*Tile tile = Main.tile[i, j];
            if (tile.TileFrameX < 66)
            {
                r = 0.9f;
                g = 0.9f;
                b = 0.9f;
            }*/
            r = 0.9f;
            g = 0.9f;
            b = 0.9f;
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 9) {
                frameCounter = 0;
                if (++frame >= 3) {
                    frame = 0;
                }
            }
        }


    }
}