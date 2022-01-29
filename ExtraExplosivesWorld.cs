using ExtraExplosives.Items.Rockets;
using ExtraExplosives.Items.Weapons;
using ExtraExplosives.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;

namespace ExtraExplosives
{
    public class ExtraExplosivesWorld : ModWorld
    {
        public static bool BossCheckDead;

        public override void Initialize()
        {
            BossCheckDead = false;
            base.Initialize();
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            //tasks.Add(new PassLegacy("archivingValues", delegate(GenerationProgress progress)
            //{
            //    progress.Message = "Archiving Tile Locations";
            //    originalWorldState = new int[Main.maxTilesX, Main.maxTilesY];
            //    for(int i = 0; i < Main.maxTilesX; i++)
            //    {
            //        for (int j = 0; j < Main.maxTilesY; j++)
            //        {
            //            if (WorldGen.TileEmpty(i, j)) originalWorldState[i, j] = -1;
            //            else originalWorldState[i, j] = Main.tile[i, j].type;
            //            progress.Message = $"{i},{j} @ {i * j}";
            //        }

            //        progress.Set(i / Main.maxTilesX);
            //    }
            //}));
        }
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
                int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY - 10);
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

        public override void PostWorldGen()
        {
            int[] itemsToPlaceInWaterChests = { ModContent.ItemType<CoralKrakSlinger>() };
            int itemToPlaceInChestChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers &&
                    Main.tile[chest.x, chest.y].frameX == 17 * 36 && Main.rand.NextFloat() < 0.2f)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInWaterChests[itemToPlaceInChestChoice]);
                            chest.item[inventoryIndex + 1].SetDefaults(ModContent.ItemType<Rocket0>());
                            chest.item[inventoryIndex + 1].stack = 30;
                            itemToPlaceInChestChoice = (itemToPlaceInChestChoice + 1) % itemsToPlaceInWaterChests.Length;
                            break;
                        }
                    }
                }
            }
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                //Boss
                [nameof(BossCheckDead)] = BossCheckDead
            };
        }

        public override void Load(TagCompound tag)
        {
            //Boss tag loading
            BossCheckDead = tag.GetBool(nameof(BossCheckDead));
            base.Load(tag);
        }
    }
}