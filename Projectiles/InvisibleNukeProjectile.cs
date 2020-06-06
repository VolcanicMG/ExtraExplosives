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
using ExtraExplosives.Tiles;

namespace ExtraExplosives.Projectiles
{
    public class InvisibleNukeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InvisibleNukeExplosive");
        }

        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile";

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 44;
            projectile.height = 44;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
            projectile.scale = 1.5f;
            projectile.netImportant = true;
            projectile.extraUpdates = 3;
        }

        public override void AI()
        {

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(8f * 1.5f, projectile.Center, 3000, 100, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, Main.rand.Next(5) + 5);

            //Create Bomb Dust
            //CreateDust(projectile.Center, 10);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            for (int y = -radius / 2; y <= 0; y++)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {

                        }
                        else //Breakable
                        {
                            if (Main.rand.Next(3) < 2)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSurfaceTile>());
                            }
                            else
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                            }
                        }
                    }
                }
            }

            for (int y = 0; y < radius / 2; y++)
            {
                for (int x = -radius; x < radius; x++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {

                        }
                        else //Breakable
                        {
                            if (WorldGen.TileEmpty(xPosition, yPosition))
                            {
                                WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                            }
                            else
                            {
                                if (y == 1)
                                {
                                    if (Main.rand.Next(10) < 8)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                                    }
                                }
                                else if (y == 2)
                                {
                                    if (Main.rand.Next(10) < 7)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                                    }
                                }
                                else if (y == 3)
                                {
                                    if (Main.rand.Next(10) < 6)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                                    }
                                }
                                else if (y == 4)
                                {
                                    if (Main.rand.Next(10) < 5)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                                    }
                                }
                                else if (y == 5)
                                {
                                    if (Main.rand.Next(10) < 4)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                                    }
                                }
                                else if (y == 6)
                                {
                                    if (Main.rand.Next(10) < 3)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                                    }
                                }
                                else if (y == 7)
                                {
                                    if (Main.rand.Next(10) < 2)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
                                    }
                                }
                                else if (y <= 8)
                                {
                                    if (Main.rand.Next(10) < 1)
                                    {
                                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSubSurfaceTile>());
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
                        updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 6, 0f, 0.5f, 0, new Color(255, 0, 0), 4f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0f;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
    }
}