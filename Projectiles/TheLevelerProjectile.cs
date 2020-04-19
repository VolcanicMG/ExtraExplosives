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
    public class TheLevelerProjectile : ModProjectile
    {

        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Leveler");
            //Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 5;   //This defines the hitbox width
            projectile.height = 5;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 120; //The amount of time the projectile is alive for
            projectile.damage = 0;

        }

        public override bool OnTileCollide(Vector2 old)
        {

            projectile.timeLeft = 0;
            return true;
        }


        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            int x = 0;
            int y = 0;

            int width = 100; //Explosion Width
            int height = 20; //Explosion Height

            for (y = height-1; y >= 0; y--)
            {
                for(x = -width; x < width; x++)
                {
                    int xPosition = (int) (x + position.X / 16.0f); //converts to world space
                    int yPosition = (int) (-y + position.Y / 16.0f); //converts to world space

                    if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                                || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                                Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge || Main.tile[xPosition, yPosition].type == TileID.DemonAltar)
                    {

                    }
                    else
                    {
                        WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles  
                        //Dust.NewDust(position, width, height, DustID.Fire, 4.0f, 4.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                        if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                        if(CanBreakWalls)
                        {
                            WorldGen.KillWall(xPosition + 1, yPosition + 1, false);
                        }

                        if (Main.rand.NextFloat() < 0.4f)
                        {
                            Dust dust1;
                            Dust dust2;

                            Vector2 position1 = new Vector2(position.X - 2000 / 2, position.Y - 320);
                            dust1 = Main.dust[Terraria.Dust.NewDust(position1, 2000, 320, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                            dust1.noGravity = true;
                            dust1.noLight = true;
                            dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                            Vector2 position2 = new Vector2(position.X - 2000 / 2, position.Y - 320);
                            dust2 = Main.dust[Terraria.Dust.NewDust(position2, 2000, 320, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
                            dust2.noGravity = true;
                            dust2.noLight = true;
                            dust2.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
                            dust2.fadeIn = 3f;
                        }

                    }

                }
                width++; //Increments width to make stairs on each end
            }
        }

    }
}