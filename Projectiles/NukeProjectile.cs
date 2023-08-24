using ExtraExplosives.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class NukeProjectile : ModProjectile
    {
        //Variables
        private bool firstTick;

        private const int pickPower = 250;
        private ReLogic.Utilities.SlotId sound;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NukeExplosive");
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.width = 66;
            Projectile.height = 112;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10000;
            Projectile.netImportant = true;
            //projectile.scale = 1.5f;
        }

        public override void AI()
        {
            //send the projectiles position to the player's camera and set NukeActive to true
            ExtraExplosives.NukePos = Projectile.Center;

            if (!firstTick)
            {
                if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
                {
                    // TODO this works but is wrong
                    sound = SoundEngine.PlaySound(new SoundStyle("Sounds/Custom/wizz"));
                    
                }

                firstTick = true;
            }

            if ((Projectile.position.Y / 16) > Main.maxTilesY - 100) //check and see if the projectile is in the underworld if so destroy at maxtilesy - 100
            {
                Projectile.Kill();
            }

            if ((Projectile.position.Y / 16) > Main.worldSurface * 0.35) //Once the nuke drops bellow the amount turn tile collide on
            {
                //Main.NewText("Set");
                Projectile.tileCollide = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            Player playerScreen = Main.player[Main.myPlayer];

            //Stop the sound
            if (Main.netMode != NetmodeID.Server &&  // This all needs to happen client-side!
                sound != null)                       // If the sound is null (the game is muted) just skip this step to avoid a crash
            {
                ; // TODO make the sound stop here, no idea how tho
            }

            //Deal 10k damage to everything in the game

            playerScreen.KillMe(PlayerDeathReason.ByCustomReason(playerScreen.name + " was killed by a nuclear explosion"), 10000, 1); //kill all players


            foreach (NPC npc in Main.npc)
            {
                if (npc.active)
                {
                    npc.StrikeNPC(10000, Projectile.knockBack, 1, false);
                }
            }

            //nuke hit
            ExtraExplosives.NukeHit = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket myPacket = Mod.GetPacket();
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.nukeHit);
                myPacket.Send();
            }

            //Set the shader
            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {
                foreach (Player player in Main.player)
                {
                    if (player.active && player.whoAmI != 255)
                    {
                        Filters.Scene.Activate("BigBang", player.Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
                    }
                }

            }

            SoundEngine.PlaySound(new SoundStyle("Sounds/Custom/Explosion"));

            //deactivate shaders and stuff
            ExtraExplosives.NukeActive = false;
            ExtraExplosives.NukeActivated = false;

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket myPacket = Mod.GetPacket();
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.nukeNotActive);
                myPacket.Send();

                ModPacket myPacket2 = Mod.GetPacket();
                myPacket2.Write((byte)ExtraExplosives.EEMessageTypes.nukeDeactivate);
                myPacket2.Send();
            }

            Main.screenPosition = playerScreen.Center;


            //Create Bomb Explosion
            CreateExplosion(Projectile.Center, 160);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {

            //position = new Vector2(projectile.Center.X / 16f, projectile.Center.Y / 16f);    // Converts to tile cords for convenience

            for (int x = -radius;
                x <= radius;
                x++)
            {
                //int x = (int)(i + position.X);
                for (int y = -radius;
                    y <= radius;
                    y++)
                {
                    //int y = (int)(j + position.Y);
                    int i = (int)(x + position.X / 16);
                    int j = (int)(y + position.Y / 16);
                    if (!WorldGen.InWorld(i, j)) continue;
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        Tile tile = Framing.GetTileSafely(i, j);

                        if (!WorldGen.TileEmpty(i, j) && tile.HasTile)
                        {
                            if (!CanBreakTile(tile.TileType, pickPower)) continue;
                            if (!CanBreakTiles) continue;
                            // Using KillTile is laggy, use ClearTile when working with larger tile sets    (also stops sound spam)
                            // But it must be done on outside tiles to ensure propper updates so use it only on outermost tiles
                            if (Math.Abs(x) >= radius - 1 || Math.Abs(y) >= radius - 1)
                            {
                                WorldGen.KillTile((int)(i), (int)(j), false, false, false);

                            }
                            else
                            {
                                /* TODO if (!TileID.Sets.BasicChest[Main.tile[i, j - 1].TileType] && !TileLoader.IsDresser(Main.tile[i, j - 1].TileType))
                                {
                                    tile.ClearTile();
                                    tile.HasTile = false;
                                }*/

                                if (tile.LiquidAmount == LiquidID.Water || tile.LiquidAmount == LiquidID.Lava || tile.LiquidAmount == LiquidID.Honey)
                                {
                                    Main.tile[i, j].LiquidAmount = LiquidID.Water;
                                    WorldGen.SquareTileFrame(i, j, true);
                                }
                            }
                        }
                    }
                }
            }

            int depth = 8; //Sets the depth of the waste

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
                        Main.tile[xPosition, yPosition].LiquidAmount = LiquidID.Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);
                    }
                    else if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        Main.tile[xPosition, yPosition].LiquidAmount = LiquidID.Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        ushort tile = Main.tile[xPosition, yPosition].TileType;
                        if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                            if (!WorldGen.TileEmpty(xPosition, yPosition)) //Runs when a tile is not equal empty
                            {
                                if (Main.rand.Next(10) < spawnChance)
                                {
                                    /* TODO if (!TileID.Sets.BasicChest[Main.tile[xPosition, yPosition - 1].TileType] && !TileLoader.IsDresser(Main.tile[xPosition, yPosition - 1].TileType))
                                    {
                                        Main.tile[xPosition, yPosition].ClearTile();
                                        Main.tile[xPosition, yPosition].HasTile = false;
                                    }*/
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
                        else //Breakable
                        {

                            if (!WorldGen.TileEmpty(xPosition, yPosition)) //Runs when a tile is not equal empty
                            {
                                if (Main.rand.Next(10) < spawnChance)
                                {
                                    /* TODO if (!TileID.Sets.BasicChest[Main.tile[xPosition, yPosition - 1].TileType] && !TileLoader.IsDresser(Main.tile[xPosition, yPosition - 1].TileType))
                                    {
                                        Main.tile[xPosition, yPosition].ClearTile();
                                        Main.tile[xPosition, yPosition].HasTile = false;
                                    }*/
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