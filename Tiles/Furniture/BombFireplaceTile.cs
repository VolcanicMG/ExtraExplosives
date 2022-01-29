using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombFireplaceTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            animationFrameHeight = 36;
            TileObjectData.addTile(Type);

            Lighting.AddLight(Vector2.Zero, 210, 140, 100);
            Lighting.brightness = 100;

            drop = mod.ItemType("BombFireplaceItem");
            AddMapEntry(new Color(255, 55, 55));
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameX < 66)
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
            if (++frameCounter >= 17)	// Time spent on each frame
            {
                frameCounter = 0;
                frame = ++frame % 5;	// How many framesa
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 64, 32, ModContent.ItemType<Items.Tiles.Furniture.BombFireplaceItem>());
        }
    }
}