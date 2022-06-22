using Microsoft.Xna.Framework;
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
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 20 };
            TileObjectData.newTile.CoordinateWidth = 12;
            TileObjectData.newTile.DrawYOffset = -4;

            TileObjectData.addTile(Type);

            AnimationFrameHeight = 20;

            DustType = DustID.FlameBurst;
            ItemDrop = Mod.Find<ModItem>("BombCandleItem").Type;
            AddMapEntry(new Color(255, 55, 55));

            Lighting.AddLight(Vector2.Zero, 210, 140, 100);
            Lighting.brightness = 100;

            torch = true;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX < 66)
            {
                r = 0.9f;
                g = 0.9f;
                b = 0.9f;
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            // Spend 9 ticks on each of 6 frames, looping
            frameCounter++;
            if (++frameCounter >= 18)   // Time spent on each frame
            {
                frameCounter = 0;
                frame = ++frame % 3;    // How many framesa
            }
        }


    }
}