using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives;

namespace ExtraExplosives.Projectiles
{
    public class ArenaBuilderProjectile : ModProjectile
    {
        internal static bool CanBreakWalls;

        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ArenaBuilder");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 5;   //This defines the hitbox width
            projectile.height = 5;    //This defines the hitbox height
            projectile.aiStyle = 16; //16  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 100; //The amount of time the projectile is alive for
        }

        public override bool OnTileCollide(Vector2 old)
        {
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 5;
            projectile.height = 5;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;
            projectile.aiStyle = 0;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            int x = 0;
            int y = 0;

            int width = 240;
            int height = 120;

            Dust dust1;
            Dust dust2;
            Dust dust3;

            int platformCntr = height - 15; //Space between platform layers

            for (x = -(width / 2); x <= width / 2; x++)
            {
                for (y = height - 1; y >= 0; y--)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.DesertFossil || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                            || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                            Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge)
                    {

                    }
                    else if (CalamityMod != null && (Main.tile[xPosition, yPosition].type == CalamityMod.TileType("SeaPrism") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AerialiteOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CryonicOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CharredOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PerennialOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ScoriaOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AstralOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("UelibloomOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AuricOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AbyssGravel") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Voidstone") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PlantyMush")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Tenebris") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ArenaBlock") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Cinderplate") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumClusterOre")))
                    {
                        if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                        }
                    }
                    else if (ThoriumMod != null && (Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("Aquaite") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("LodeStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("ValadiumChunk") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("IllumiteChunk")
                        || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("PearlStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("DepthChestPlatform")))
                    {
                        if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                        }
                    }
                    else
                    {
                        WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles   
                        //Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                        if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);


                        Vector2 position1 = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);
                        dust1 = Main.dust[Terraria.Dust.NewDust(position1, 2000, 2000, 186, 0f, 0f, 0, new Color(159, 0, 255), 5f)];
                        dust1.noGravity = true;
                        dust1.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                        dust1.fadeIn = 3f;

                        Vector2 position2 = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        dust2 = Main.dust[Terraria.Dust.NewDust(position2, 2000, 2000, 186, 0f, 0f, 0, new Color(0, 17, 255), 5f)];
                        dust2.noGravity = true;
                        dust2.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                        dust2.fadeIn = 3f;

                        Vector2 position3 = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        dust3 = Main.dust[Terraria.Dust.NewDust(position3, 2000, 2000, 186, 0f, 0f, 0, new Color(255, 0, 150), 5f)];
                        dust3.noGravity = true;
                        dust3.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                        dust3.fadeIn = 3f;
                    }
                    
                    //Breaks Liquid
                    Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
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
                platformCntr = height - 15; //Reset the platformCntr
            }

            platformCntr = 15; //Reset the platformCntr

            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

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
                platformCntr = 15; //Reset the platformCntr 
            }

            platformCntr = 15; //Reset the platformCntr

            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

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
                platformCntr = 15;
            }

            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

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
                            if(x > 100 && x < 120)
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
                            if(y == 5)
                            {
                                if(x == 101 || x == 108 || x == 110 || x == 119)
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition, TileID.Torches);
                                }
                            }

                            //Furniture
                            if(y == 1)
                            {
                                if(x == 105)
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition, TileID.Beds);
                                }
                                else if(x == 114)
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition, TileID.Tables);
                                }
                                else if(x == 116)
                                {
                                    WorldGen.PlaceTile(xPosition, yPosition, TileID.Chairs);
                                }
                            }
                        }
                    }
                }
            }

            for (x = -(width / 2); x < width / 2; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    //Spawn & Nurse
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
}