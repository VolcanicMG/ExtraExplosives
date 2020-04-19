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
    public class BigBouncyDynamiteProjectile : ModProjectile
    {
        internal static bool CanBreakWalls;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BigBouncyDynamite");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 13;   //This defines the hitbox width
            projectile.height = 32;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 250; //The amount of time the projectile is alive for
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // This code makes the projectile very bouncy.
            if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                if (projectile.velocity.X >= 10 || projectile.velocity.X < -10) 
                { 
                    projectile.velocity.X = 10; 
                }
                else
                {
                    projectile.velocity.X = oldVelocity.X * -1.2f;
                }
            }
            if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                if(projectile.velocity.Y >= 10 || projectile.velocity.Y < -10) 
                {
                    projectile.velocity.Y = 10;
                }
                else
                {
                    projectile.velocity.Y = oldVelocity.Y * -1.2f;
                }
            }
            return false;
            //projectile.direction = projectile.direction * -1;

            //return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 5;     //this is the explosion radius, the highter is the value the bigger is the explosion


            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 2.0f);
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 300, 30, projectile.owner, 0.0f, 0);
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        int xPosition = (int)(x + position.X / 16.0f);
                        int yPosition = (int)(y + position.Y / 16.0f);

                        if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.DesertFossil || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                                || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                                Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge || Main.tile[xPosition, yPosition].type == TileID.DemonAltar)
                            {

                            }
                            else
                            {
                                Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.Next(100) - 50, Main.rand.Next(100) - 50, ProjectileID.BouncyDynamite, 0, 0, projectile.owner, 0.0f, 0);
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles  
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }


                        }
                    }
                }

            Dust dust;
            Vector2 glowPosition2 = new Vector2(position.X - 121/2, position.Y - 121/2);
            for (int i = 0; i < 100; i++)
            {
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                dust = Main.dust[Terraria.Dust.NewDust(glowPosition2, 121, 121, 216, 0f, 0f, 0, new Color(255, 105, 180), 3.092105f)];
                dust.noGravity = true;

            }

            
        }


    }
}