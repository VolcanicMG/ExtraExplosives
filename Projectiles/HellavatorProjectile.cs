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
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class HellavatorProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellavator Projectile");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.damage = 0;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 0);

            //Create Bomb Dust
            CreateDust(projectile.Center, 100);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            int width = 5; //Explosion Width
            int height = 1000; //Explosion Height

            for (int y = 0; y <= height; y++)
            {
                for (int x = -width; x < width; x++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type, xPosition, yPosition)) //Unbreakable
                    {
<<<<<<< Updated upstream
                        Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                                    || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                                    Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge || Main.tile[xPosition, yPosition].type == TileID.DemonAltar)
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

                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            if (CanBreakWalls) //break the last bit
                            {
                                WorldGen.KillWall(xPosition + 1, yPosition + 1, false);
                            }

                            if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                            {
                                if (Main.rand.NextFloat() < 0.3f)
                                {
                                    Dust dust1;
                                    Dust dust2;

                                    Vector2 position1 = new Vector2(position.X, position.Y);
                                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 10, 10, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                                    dust1.noGravity = true;
                                    dust1.noLight = true;
                                    dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                                    Vector2 position2 = new Vector2(position.X, position.Y);
                                    dust2 = Main.dust[Terraria.Dust.NewDust(position2, 10, 10, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
                                    dust2.noGravity = true;
                                    dust2.noLight = true;
                                    dust2.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
                                    dust2.fadeIn = 3f;
                                }
                            }

                        }
=======

>>>>>>> Stashed changes
                    }
                    else //Breakable
                    {
                        WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                        if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
                        if (CanBreakWalls) WorldGen.KillWall(xPosition + 1, yPosition + 1, false); //Break the last bit of wall
                    }

                    Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water; //This destroys liquids
                    WorldGen.SquareTileFrame(xPosition, yPosition, true); //Updates Area
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
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
                        dust.fadeIn = 3f;
                    }
                    //------------
                }
            }
        }
    }
}