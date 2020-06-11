using ExtraExplosives.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using ExtraExplosives;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.Projectiles
{
    public class NukeProjectile : ModProjectile
    {
        //Variables
        bool firstTick;
        SoundEffectInstance sound;
        private const int PickPower = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NukeExplosive");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 66;
            projectile.height = 112;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 10000;
            projectile.netImportant = true;
            //projectile.scale = 1.5f;
        }

        public override void AI()
        {
            //send the projectiles postion to the player's camera and set NukeActive to true
            ExtraExplosives.NukePos = projectile.Center;
            ExtraExplosives.NukeActive = true; //since the projectile is active set it active in the player class

            if (!firstTick)
            {
                if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
                {
                    sound = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/wizz"));
                }

                firstTick = true;
            }

            if ((projectile.position.Y / 16) > Main.maxTilesY - 100) //check abd see if the projectile is in the underworld if so destroy at maxtilesy - 100
            {
                projectile.Kill();
            }

            if ((projectile.position.Y / 16) > Main.worldSurface * 0.35)
            {
                //Main.NewText("Set");
                projectile.tileCollide = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];

            //Stop the sound
            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {
                sound.Stop();
            }

            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was killed by a nuclear explosion"), 5000, 1);

            //Set the shader
            ExtraExplosives.NukeHit = true;
            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {
                Filters.Scene.Activate("BigBang", player.Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
                //float progress = 0f;
                //Filters.Scene["Bang"].GetShader().UseProgress(progress).UseOpacity(0);
            }

            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Explosion"));
            ExtraExplosives.NukeActive = false;
            ExtraExplosives.NukeActivated = false;
            Main.screenPosition = player.Center;

            //Create Bomb Damage
            ExplosionDamage(150f * 1.5f, projectile.Center, 3000, 1.0f, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 150);

            //for (int x = 0; x < 50; x++)
            //{
            //    SpawnProjectileSynced(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(Main.rand.Next(15) - 7, Main.rand.Next(15) - 7), ModContent.ProjectileType<InvisibleNukeProjectile>(), 0, 0, 0, 0, projectile.owner);
            //    //Projectile.NewProjectile(position.X, position.Y, Main.rand.Next(40) + 10, Main.rand.Next(40) + 10, ModContent.ProjectileType<InvisibleNukeProjectile>(), 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square 
            //}
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left 
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        ushort tile = Main.tile[xPosition, yPosition].type;
                        if (CheckForUnbreakableTiles(tile) || !CanBreakTile(tile, PickPower)) //Unbreakable
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                                || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                                Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DesertFossil)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, true);
                            }
                        }
                        else //Breakable
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, true);
                        }
                    }
                }
            }

            int depth = 10; //Sets the depth of the waste

            for (int x = depth + 1; x > 0; x--)
            {
                if (x == depth + 1) AddNuclearWaste(radius++, position, x, ModContent.TileType<NuclearWasteSurfaceTile>(), ModContent.TileType<NuclearWasteSurfaceTile>());
                else AddNuclearWaste(radius++, position, x, ModContent.TileType<NuclearWasteSurfaceTile>(), ModContent.TileType<NuclearWasteSubSurfaceTile>());
            }

        }

        private void AddNuclearWaste(int radius, Vector2 position, int spawnChance, int surfaceTile, int subSurfaceTile)
        {
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left 
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius - 1 + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);
                    }
                    else if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                                || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum
                                || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium || Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DesertFossil)
                            {
                                if (!WorldGen.TileEmpty(xPosition, yPosition)) //Runs when a tile is not equal empty
                                {
                                    if (Main.rand.Next(10) < spawnChance)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);
                                        if (WorldGen.TileEmpty(xPosition + 1, yPosition) || WorldGen.TileEmpty(xPosition - 1, yPosition) || WorldGen.TileEmpty(xPosition, yPosition + 1) || WorldGen.TileEmpty(xPosition, yPosition - 1))
                                        {
                                            WorldGen.PlaceTile(xPosition, yPosition, surfaceTile);
                                        }
                                        else
                                        {
                                            WorldGen.PlaceTile(xPosition, yPosition, subSurfaceTile);
                                        }
                                    }
                                }
                            }
                        }
                        else //Breakable
                        {
                            if (!WorldGen.TileEmpty(xPosition, yPosition)) //Runs when a tile is not equal empty
                            {
                                if (Main.rand.Next(10) < spawnChance)
                                {
                                    WorldGen.KillTile(xPosition, yPosition, false, false, true);
                                    if (WorldGen.TileEmpty(xPosition + 1, yPosition) || WorldGen.TileEmpty(xPosition - 1, yPosition) || WorldGen.TileEmpty(xPosition, yPosition + 1) || WorldGen.TileEmpty(xPosition, yPosition - 1))
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, surfaceTile);
                                    }
                                    else
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, subSurfaceTile);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}