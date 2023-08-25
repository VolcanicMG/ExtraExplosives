using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class ArenaBuilderProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Arena_Bomb_";
        protected override string goreFileLoc => "n/a";
        private const int PickPower = 70;
        // private LegacySoundStyle[] explodeSounds;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ArenaBuilder");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            pickPower = 70;
            radius = 0;
            Projectile.tileCollide = true;
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            /*explodeSounds = new LegacySoundStyle[2]; // TODO
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }*/
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);

            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;
            Projectile.aiStyle = 0;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            ////SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)Projectile.Center.X, (int)Projectile.Center.Y);
            //SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

            /* ===== ABOUT THE BOMB SOUND =====
			 * 
			 * Because the KillTile() and KillWall() methods used in CreateExplosion()
			 * produce a lot of sounds, the bomb's own explosion sound is difficult to
			 * hear. The solution to eliminate those unnecessary sounds is to alter
			 * the fields of each Tile that the explosion affects, but this creates
			 * additional problems (no dropped Tile items, adjacent Tiles not updating
			 * their sprites, etc). I've decided to ignore doing the changes because
			 * it would entail making the same changes to multiple projectiles and the
			 * projectile template.
			 * 
			 * -- V8_Ninja
			 */

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner); //No damage needed

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, -1);

            Explosion();

            //Create Bomb Dust dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 2000, 186, 0f, 0f, 0, new Color(159, 0, 255), 5f)];
            CreateDust(Projectile.Center, 500);
        }

        public override void ExplosionDamage()
        {
            return;
        }

        public override void Explosion()    // This is a special explosive, ignored
        {
            //if (Main.player[projectile.owner].EE().BombardEmblem) return;
            Vector2 position = Projectile.position;
            int width = 240; //Width of arena
            int height = 120; //Height of arena

            int x = 0;
            int y = 0;

            int platformCntr = height - 15; //Space between platform layers

            for (x = -(width / 2); x <= width / 2; x++)
            {
                for (y = height - 1; y >= 0; y--)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (!WorldGen.InWorld(xPosition, yPosition)) continue;

                    Tile tile = Framing.GetTileSafely(xPosition, yPosition);

                    if (WorldGen.InWorld(xPosition, yPosition) && tile.HasTile)
                    {
                        if (!CanBreakTile(tile.TileType, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            // TODO no clue but needs fixing probably
                            /*if (!TileID.Sets.BasicChest[Main.tile[xPosition, yPosition - 1].TileType] && !TileLoader.IsDresser(Main.tile[xPosition, yPosition - 1].TileType) && Main.tile[xPosition, yPosition - 1].TileType != 26)
                            {
                                tile.ClearTile();
                                tile.HasTile = false;

                            }*/

                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                WorldGen.SquareTileFrame(xPosition, yPosition, true); //Updates Area
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 2, (float)xPosition, (float)yPosition, 0f, 0, 0, 0);
                            }

                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
                        }
                    }

                    //The following happens whether the block is breakable or not as the following methods cannot break or replace blocks that already exist.
                    if (!OutOfBounds(xPosition, yPosition))
                    {
                        //Breaks Liquid
                        Main.tile[xPosition, yPosition].LiquidAmount = LiquidID.Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        //Place Arena Outline
                        if (y == 0 || y == height - 1)
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.CrystalBlock);
                        if ((x == -(width / 2) || x == (width / 2)) && (y != 0 || y != height - 1))
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.CrystalBlock);

                        //Places Arena Platforms
                        if (y == platformCntr && (x != -(width / 2) || x != (width / 2)))
                        {
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.Platforms);
                            platformCntr -= 15;
                        }
                    }
                }
                platformCntr = height - 15; //Decrease the platformCntr
            }
            platformCntr = 15; //Reset the platformCntr

            //Now that the space has been cleared and an outline has been established, the arena can be filled.
            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (!OutOfBounds(xPosition, yPosition))
                    {
                        //Campfires
                        if (y == platformCntr && (x != -(width / 2) || x != (width / 2)))
                        {
                            if (x == -64 || x == 0 || x == 64)
                            {
                                WorldGen.PlaceTile(xPosition, yPosition - 1, TileID.Campfire);
                            }

                            platformCntr += 30;
                        }
                    }
                }
                platformCntr = 15; //Reset the platformCntr
            }
            platformCntr = 15; //Reset the platformCntr

            //More arena filling
            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (!OutOfBounds(xPosition, yPosition))
                    {
                        //Torches
                        if (y == platformCntr && (x != -(width / 2) || x != (width / 2)))
                        {
                            if (x == -112 || x == -100 || x == -88 || x == -76 || x == -52 || x == -40 || x == -28 || x == -16 || x == -4 || x == 4 || x == 16 || x == 28 || x == 40 || x == 52 || x == 76 || x == 88 || x == 100 || x == 112)
                            {
                                WorldGen.PlaceTile(xPosition, yPosition - 1, TileID.Torches);
                            }

                            platformCntr += 15;
                        }
                    }
                }
                platformCntr = 15;
            }

            //More arena filling
            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (!OutOfBounds(xPosition, yPosition))
                    {
                        //Spawn & Nurse
                        if (x > 99) //X confines
                        {
                            if (y < 7) //Y confines
                            {
                                //Walls
                                if (x == 100 && (y == 4 || y == 5))
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition, TileID.CrystalBlock);
                                }
                                if (x == 109 && (y == 4 || y == 5))
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition, TileID.CrystalBlock);
                                }

                                //Roof
                                if (y == 6 && (x != 100 || x != 120))
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition, TileID.CrystalBlock);
                                }

                                //Background
                                if (x > 100 && x < 120)
                                {
                                    //Break Walls
                                    WorldGen.KillWall(xPosition, yPosition);

                                    //Places Walls
                                    if (y == 5 || y == 2 || y == 1)
                                        WorldGen.PlaceWall(xPosition, yPosition, WallID.Wood);
                                    if (y == 3 || y == 4)
                                    {
                                        if (x == 113 || x == 114 || x == 115)
                                        {
                                            WorldGen.PlaceWall(xPosition, yPosition, WallID.Glass);
                                        }
                                        else
                                        {
                                            WorldGen.PlaceWall(xPosition, yPosition, WallID.Wood);
                                        }
                                    }
                                }

                                //Lights
                                if (y == 5)
                                {
                                    if (x == 101 || x == 108 || x == 110 || x == 119)
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, TileID.Torches);
                                    }
                                }

                                //Furniture
                                if (y == 1)
                                {
                                    if (x == 105)
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, TileID.Beds);
                                    }
                                    else if (x == 114)
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, TileID.Tables);
                                    }
                                    else if (x == 116)
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, TileID.Chairs);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //More arena filling
            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (!OutOfBounds(xPosition, yPosition))
                    {
                        //Spawn & Nurse additional
                        if (x > 99) //X confines
                        {
                            if (y < 7) //Y confines
                            {
                                //Doors
                                if (y == 1)
                                {
                                    if (x == 100)
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, TileID.ClosedDoor);
                                    }
                                    else if (x == 109)
                                    {
                                        WorldGen.PlaceTile(xPosition, yPosition, TileID.ClosedDoor);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 2000, 186, 0f, 0f, 0, new Color(159, 0, 255), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 2000, 186, 0f, 0f, 0, new Color(0, 17, 255), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 2000, 186, 0f, 0f, 0, new Color(255, 0, 150), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > radius * 16) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }
                }
            }
        }

        //This returns true if the arena is going out of bounds
        private bool OutOfBounds(int posX, int posY)
        {
            //Tests If Tile Is OutOfBounds
            if (posX < 0 || posY < 0 || posX > Main.maxTilesX || posY > Main.maxTilesY)
                return true;
            return false;
        }
    }
}