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
    public class C4Projectile : ModProjectile
    {
        //Variables:
        private bool freeze = false;
        private Vector2 positionToFreeze;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("C4");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; 
            projectile.width = 16;
            projectile.height = 10;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 4000000;
            //projectile.extraUpdates = 1;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            if (!freeze)
            {
                freeze = true;
                positionToFreeze = new Vector2(projectile.position.X, projectile.position.Y);
                projectile.width = 64;
                projectile.height = 40;
                projectile.position.X = positionToFreeze.X;
                projectile.position.Y = positionToFreeze.Y;
                projectile.velocity.X = 0;
                projectile.velocity.Y = 0;
            }

            return true;
        }

        public override void PostAI()
        {
            if (projectile.owner == Main.myPlayer)
            {
                var player = Main.player[projectile.owner].GetModPlayer<ExtraExplosivesPlayer>();

                if (player.detonate == true)
                {
                    projectile.Kill();
                }
            }

            if (freeze == true)
            {
                projectile.position.X = positionToFreeze.X;
                projectile.position.Y = positionToFreeze.Y;
                projectile.velocity.X = 0;
                projectile.velocity.Y = 0;
            }

            base.PostAI();
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Dust
            CreateDust(projectile.Center, 550);

            //Create Bomb Damage
            ExplosionDamage(20f * 1.5f, projectile.Center, 1000, 40, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 20);
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
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.DesertFossil)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                            }
                        }
                        else //Breakable
                        {
                            if (CanBreakTiles) //User preferences dictates if this bomb can break tiles
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
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
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 360 / 2, position.Y - 360 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 360, 360, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 642 / 2, position.Y - 642 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 642, 642, 56, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(91, Main.LocalPlayer);
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 560 / 2, position.Y - 560 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 560, 560, 6, 0f, 0.5263162f, 0, new Color(255, 150, 0), 5f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                        dust.fadeIn = 3f;
                    }
                    //------------

                    //---Dust 4---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 157 / 2, position.Y - 157 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 157, 157, 55, 0f, 0f, 0, new Color(255, 100, 0), 3.552631f)];
                        dust.noGravity = true;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                    }
                    //------------
                }
            }
        }
    }
}

