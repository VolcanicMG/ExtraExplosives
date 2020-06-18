using ExtraExplosives.Items.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExtraExplosives.Tiles.Furniture
{
    public class BombCandleTile : ModTile
    {
	    public override void SetDefaults()
	    {
		    Main.tileLighted[Type] = true;
		    Main.tileLavaDeath[Type] = true;
		    Main.tileWaterDeath[Type] = true;
		    Main.tileNoAttach[Type] = true;
		    Main.tileFrameImportant[Type] = true;
		    AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		    TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		    TileObjectData.newTile.CoordinateHeights = new int[] {20};
		    TileObjectData.newTile.CoordinateWidth = 12;
		    TileObjectData.newTile.DrawYOffset = -4;
		    
		    TileObjectData.addTile(Type);

		    animationFrameHeight = 20;
		    
		    dustType = DustID.FlameBurst;
		    drop = mod.ItemType("BombCandleItem");
		    AddMapEntry(new Color(255, 55, 55));
	    }
	    
	    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
		    r = 0.93f;
		    g = 0.11f;
		    b = 0.12f;
	    }
	    
	    public override void AnimateTile(ref int frame, ref int frameCounter) {
		    // Spend 9 ticks on each of 6 frames, looping
		    frameCounter++;
		    if (++frameCounter >= 9)	// Time spent on each frame
		    {
			    frameCounter = 0;
			    frame = ++frame % 3;	// How many framesa
		    }
	    }
	    
	    
    }
}