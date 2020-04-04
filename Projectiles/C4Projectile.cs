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
using ExtraExplosives.Items;
using ExtraExplosives;


namespace ExtraExplosives.Projectiles
{
    public class C4Projectile : ModProjectile
    {
        internal static bool CanBreakWalls;
        //internal static bool detonate;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("C4Projectile");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 16;   //This defines the hitbox width
            projectile.height = 10;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 4000000; //The amsadount of time the projectile is alive for
            //projectile.extraUpdates = 1;
        }

        private bool freeze = false;
        private Vector2 positionToFreeze;
    
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
            if (ExtraExplosives.detonate == true)
            {
                projectile.timeLeft = 0;
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

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 20;     //this is the explosion radius, the highter is the value the bigger is the explosion

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
                    {
                       
                        if(Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.DesertFossil || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                            || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                            Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge)
                        {

                        }
                        else
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles  
                            Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                        }
                    }
                }
            }
        }


    }
}

