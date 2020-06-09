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
    public class HeavyBombProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HeavyBomb");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 13;
            projectile.height = 19;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1000;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item37, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(2f * 2f, projectile.Center, 30, 40, projectile.owner);

            //Create Bomb Explosion
            Vector2 position = projectile.Center;
            int radius = 2;

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

                        }
                        else //Breakable
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
                        }
                    }
                }
            }

            //Create Bomb Dust
            for (int i = 0; i < 10; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    Dust dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X - (30 / 2), position.Y - 0), 30, 30, 1, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                }
            }

            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);
            
            //Create Bomb Dust
            CreateDust(projectile.Center, 500);
            
            //Create Bomb Damage
            ExplosionDamage(20f * 1.5f, projectile.Center, 500, 40, projectile.owner);

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

                        }
                        else //Breakable
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
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
                    if (Main.rand.NextFloat() < 0.5f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 10f)];
                        dust.noGravity = true;
                        dust.fadeIn = 2.486842f;
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.5f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 203, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.5f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 31, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }
    }
}