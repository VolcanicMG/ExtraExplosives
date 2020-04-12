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
using ExtraExplosives.Buffs;
using ExtraExplosives.Items;

namespace ExtraExplosives.Projectiles
{
    public class DaBombProjectile : ModProjectile
    {
        internal static bool CanBreakWalls;
        internal static bool CanBreakTiles;

        public bool buffActive;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DaBomb");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 42;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = 20; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 400; //The amount of time the projectile is alive for
            
            buffActive = true;
        }

        public override void PostAI()
        {
            Player player = Main.player[Main.myPlayer];

            if (buffActive == true && Terraria.ID.NetmodeID.Server == NetmodeID.Server)
            {
                player.AddBuff(mod.BuffType("ExtraExplosivesDaBombBuff"), 50, false);
            }

            base.PostAI();
        }



        public override void Kill(int timeLeft)
        {

            Player player = Main.player[Main.myPlayer];
            Vector2 position = player.Center;//projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 20;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 400, 200, Main.myPlayer, 0.0f, 0);

                if (CanBreakTiles == true)
                {
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
                                    WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles  
                                    if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                                }

                            }
                        }
                    }
                }
            }

            for (int i = 0; i <= 50; i++)
            {
                int Hw = 550;
                float scale = 10f;

                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 vev = new Vector2(position.X - (Hw / 2), position.Y - (Hw / 2));
                dust = Main.dust[Terraria.Dust.NewDust(vev, Hw, Hw, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), scale)];
                dust.noGravity = true;
                dust.fadeIn = 2.486842f;


                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                dust = Main.dust[Terraria.Dust.NewDust(vev, Hw, Hw, 203, 0f, 0f, 0, new Color(255, 255, 255), scale)];
                dust.noGravity = true;
                dust.noLight = true;


                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                dust = Main.dust[Terraria.Dust.NewDust(vev, Hw, Hw, 31, 0f, 0f, 0, new Color(255, 255, 255), scale)];
                dust.noGravity = true;
                dust.noLight = true;
            }

            buffActive = false;
        }


    }
}