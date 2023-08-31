using ExtraExplosives.Items.Rockets;
using ExtraExplosives.Items.Weapons;
using ExtraExplosives.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using ExtraExplosives.Items.Accessories;
using ExtraExplosives.Items.Accessories.BombardierClassAccessories;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using ItemID = Terraria.ID.ItemID;
using TileID = Terraria.ID.TileID;
using Terraria.WorldBuilding;

namespace ExtraExplosives
{
    public class ExtraExplosivesSystem : ModSystem // Here it is
    {
        public static bool BossCheckDead;

        //UI
        internal UserInterface ExtraExplosivesUserInterface;
        internal UserInterface ExtraExplosivesReforgeBombInterface;
        internal UserInterface CEBossInterface;
        internal UserInterface CEBossInterfaceNonOwner;

        public override void OnWorldLoad()
        {
            BossCheckDead = false;
            base.OnWorldLoad();
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
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
            int goodIndex = list.FindIndex(genPass => genPass.Name.Equals("Hardmode Good"));
            if (goodIndex != -1)
            {
                list.Add(new PassLegacy("Irradiating the Hallow",
                    delegate { Main.NewText("The world is leaking radiation", Color.Chartreuse); }));
            }
        }

        public override void PreUpdateWorld()
        {
            GenCrystals(); // once a tick, try to generate a crystal
        }


        private void GenCrystals()
        {
            if (Main.hardMode && Main.rand.NextBool(400)) // 1 in 400 chance, TODO balance    
            {
                // Tries the location and if its air and touches pearlstone (type == 117) then it will spawn
                int x = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY - 10);
                if (Main.tile[x, y].TileType == TileID.DemonAltar)
                    return; // Avoid breaking demon alters since this blesses the world with hm ores
                if ((WorldGen.SolidTile(x - 1, y) && Main.tile[x - 1, y].TileType == 117) ||
                    (WorldGen.SolidTile(x + 1, y) && Main.tile[x + 1, y].TileType == 117) ||
                    (WorldGen.SolidTile(x, y - 1) && Main.tile[x, y - 1].TileType == 117) ||
                    (WorldGen.SolidTile(x, y + 1) && Main.tile[x, y + 1].TileType == 117))
                {
                    WorldGen.PlaceTile(x, y, ModContent.TileType<GlowingCrystal>(), false, false, -1,
                        Main.rand.Next(18)); // Random style between 0-17, rotation is done automatically
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
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers &&
                    Main.tile[chest.x, chest.y].TileFrameX == 17 * 36 && Main.rand.NextFloat() < 0.2f)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInWaterChests[itemToPlaceInChestChoice]);
                            chest.item[inventoryIndex + 1].SetDefaults(ModContent.ItemType<Rocket0>());
                            chest.item[inventoryIndex + 1].stack = 30;
                            itemToPlaceInChestChoice =
                                (itemToPlaceInChestChoice + 1) % itemsToPlaceInWaterChests.Length;
                            break;
                        }
                    }
                }
            }
        }

        public override void SaveWorldData(TagCompound tag)
        {
            //Boss
            tag.Add(nameof(BossCheckDead), BossCheckDead);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            //Boss tag loading
            BossCheckDead = tag.GetBool(nameof(BossCheckDead));
            base.LoadWorldData(tag);
        }


        public override void Load()
        {
            base.Load();
            //UI stuff
            ExtraExplosivesUserInterface = new UserInterface();
            ExtraExplosivesReforgeBombInterface = new UserInterface();
            CEBossInterface = new UserInterface();
            CEBossInterfaceNonOwner = new UserInterface();
        }

        public override void Unload()
        {
            base.Unload();
            ExtraExplosivesUserInterface = null;
        }

        public override void PostUpdateEverything() /* t-ModPorter Note: Removed. Use ModSystem.PostUpdateEverything */
        {
            /*if (boomBoxMusic)
            {
                boomBoxTimer++;
                if (boomBoxTimer > (1200 + Main.rand.Next(600)))
                {
                    boomBoxMusic = false;
                    boomBoxTimer = 0;
                }
            }*/

            //Still needs work and most likely reworked(MP issues)
            /*if (Main.LocalPlayer.dead)
            {
                if (NovaBooster.EngineSoundInstance != null &&
                    NovaBooster.EngineSoundInstance.State == SoundState.Playing)
                    NovaBooster.EngineSoundInstance.Stop();
                if (NovaBooster.EndSoundInstance != null && NovaBooster.EndSoundInstance.State == SoundState.Playing)
                    NovaBooster.EndSoundInstance.Stop();
            }*/
        }

        public override void UpdateUI(GameTime gameTime)
        {
            ExtraExplosivesUserInterface?.Update(gameTime);
            CEBossInterface?.Update(gameTime);
            CEBossInterfaceNonOwner?.Update(gameTime);
            //ExtraExplosivesReforgeBombInterface?.Update(gameTime);

            //TODO buttonInterface?.Update(gameTime);
            //cookbookInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
            // TODO
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExtraExplosives: UI",
                    delegate
                    {
                        // If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
                        ExtraExplosivesUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExtraExplosives: ReforgeBombUI",
                    delegate
                    {
                        // If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
                        ExtraExplosivesReforgeBombInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExtraExplosives: CEBossUI",
                    delegate
                    {
                        // If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
                        CEBossInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExtraExplosives: CEBossUINonOwner",
                    delegate
                    {
                        // If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
                        CEBossInterfaceNonOwner.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExtraExplosives: CookbookButton",
                    delegate
                    {
                        /*if (Main.playerInventory)
                        {
                            buttonInterface.Draw(Main.spriteBatch, new GameTime());
                        }*/
                        return true;
                    },
                    InterfaceScaleType.UI));
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExtraExplosives: CookbookUI",
                    delegate
                    {

                        //cookbookInterface.Draw(Main.spriteBatch, new GameTime());

                        return true;

                    },
                    InterfaceScaleType.UI));
            }

            if (mouseTextIndex != -1)
            {

            }


        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.AvengerEmblem);
            recipe.AddIngredient(ModContent.ItemType<BombardierEmblem>(), 1);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            base.AddRecipes();
        }
    }
}