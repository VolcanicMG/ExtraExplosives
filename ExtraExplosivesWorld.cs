using System;
using System.Collections.Generic;
using ExtraExplosives.Tiles;
using IL.Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.World.Generation;
using ItemID = Terraria.ID.ItemID;
using Tile = IL.Terraria.Tile;
using TileID = Terraria.ID.TileID;

namespace ExtraExplosives
{
    public class ExtraExplosivesWorld : ModWorld
    {
        public override void ModifyHardmodeTasks(List<GenPass> list)
        {
            int goodIndex = list.FindIndex(genpass => genpass.Name.Equals("Hardmode Good"));
            if (goodIndex != -1)
            {
                list.Add(new PassLegacy("Irradiating the Hallow",
                    delegate
                    {
                        Main.NewText("The world is leaking radiation", Color.Chartreuse);
                    }));
            }
        }

        public override void PreUpdate()
        {
            GenCrystals();    // once a tick, try to generate a crystal
        }
        

        private void GenCrystals()
        {
            if (Main.hardMode && Main.rand.Next(400) == 0) // 1 in 400 chance, TODO balance    
            {    // Tries the location and if its air and touches pearlstone (type == 117) then it will spawn
                int x = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
                int y = WorldGen.genRand.Next((int) WorldGen.worldSurfaceLow, Main.maxTilesY - 10);
                if (Main.tile[x, y].type == TileID.DemonAltar) return;    // Avoid breaking demon alters since this blesses the world with hm ores
                if ((WorldGen.SolidTile(x - 1, y) && Main.tile[x - 1, y].type == 117) ||        
                    (WorldGen.SolidTile(x + 1, y) && Main.tile[x + 1, y].type == 117) ||
                    (WorldGen.SolidTile(x, y - 1) && Main.tile[x, y - 1].type == 117) ||
                    (WorldGen.SolidTile(x, y + 1) && Main.tile[x, y + 1].type == 117))
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<GlowingCrystal>(), false, false, -1,   
                        Main.rand.Next(18));    // Random style between 0-17, rotation is done automatically
                }
            }
        }
    }
}