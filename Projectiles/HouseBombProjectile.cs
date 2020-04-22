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
    public class HouseBombProjectile : ModProjectile
    {
        internal static bool CanBreakWalls;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HouseBomb");
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
            projectile.timeLeft = 1000; //The amount of time the projectile is alive for
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

            int width = 11;
            int height = 7;

            for (x = -5; x < 6; x++)
            {
                for (y = height - 1; y >= 0; y--)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.DesertFossil || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                            || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                            Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge || Main.tile[xPosition, yPosition].type == TileID.DemonAltar)
                    {

                    }
                    else
                    {
                        WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles
                        Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                                                                                                         //Break Walls
                        WorldGen.KillWall(xPosition, yPosition);
                    }

                    //destroy water
                    Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
                    WorldGen.SquareTileFrame(xPosition, yPosition, true);


                    //Partical Effects
                    Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion

                    //Place House Outline
                    if (y == 0 || y == 6)
                        WorldGen.PlaceTile(xPosition, yPosition, TileID.WoodBlock);
                    if ((x == -5 || x == 5) && (y == 4 || y == 5))
                        WorldGen.PlaceTile(xPosition, yPosition, TileID.WoodBlock);

                    //Place House Walls
                    if ((y == 5 || y == 2 || y ==1) && x != -5 && x != 5)
                        WorldGen.PlaceWall(xPosition, yPosition, WallID.Wood);
                    if (y == 3 || y == 4)
                    {
                        if (x == -4 || x == -3 || x == -2 || x == 2 || x == 3 || x == 4)
                            WorldGen.PlaceWall(xPosition, yPosition, WallID.Wood);
                        if (x == -1 || x == 0 || x == 1)
                            WorldGen.PlaceWall(xPosition, yPosition, WallID.Glass);
                    }

                    //Places House Lights
                    if (y == 5)
                        if (x == -4 || x == 4)
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.Torches);

                }
            }

            for (x = -5; x < 6; x++)
            {
                for (y = 0; y < height; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    //Places House Furniture
                    if (y == 1)
                    {
                        if (x == -5 || x == 5) //Door
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.ClosedDoor);
                        if (x == 0) //Table
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.Tables);
                        if (x == 2) //Chair
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.Chairs);
                    }
                }
            }

            Dust dust1;
            Dust dust2;
            Dust dust3;

            for (int i = 0; i < 100; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Vector2 position3 = new Vector2(position.X - 250 / 2, position.Y - 190 + 20);
                    dust3 = Main.dust[Terraria.Dust.NewDust(position3, 250, 190, 263, 0f, 0f, 0, new Color(255, 255, 255), 4.5f)];
                    dust3.noGravity = true;
                    dust3.noLight = true;
                    dust3.fadeIn = 1.618421f;
                }
            }

            for (int i = 0; i < 100; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Vector2 position1 = new Vector2(position.X - 221 / 2, position.Y - 170 + 10);
                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 221, 170, 232, 0f, 0f, 214, new Color(255, 150, 0), 4.407895f)];
                    dust1.noGravity = true;
                    dust1.noLight = true;
                }

            }

            for (int i = 0; i < 100; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Vector2 position2 = new Vector2(position.X - 221 / 2, position.Y - 170 + 10);
                    dust2 = Main.dust[Terraria.Dust.NewDust(position2, 221, 170, 1, 0f, 0f, 140, new Color(255, 255, 255), 2.5f)];
                    dust2.noGravity = true;
                    dust2.noLight = true;
                }
            }
        }
    }
}