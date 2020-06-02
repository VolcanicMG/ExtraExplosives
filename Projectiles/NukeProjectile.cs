using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives;

namespace ExtraExplosives.Projectiles
{
    public class NukeProjectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NukeExplosive");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false; //checks to see if the projectile can go through tiles
            projectile.width = 66;   //This defines the hitbox width
            projectile.height = 112;    //This defines the hitbox height
            projectile.aiStyle = 1;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 10000; //The amount of time the projectile is alive for
            projectile.netImportant = true;
            //projectile.scale = 1.5f;
        }

        public override void AI()
        {
            //send the projectiles postion to the player's camera and set NukeActive to true
            ExtraExplosives.NukePos = projectile.Center;
            ExtraExplosives.NukeActive = true; //since the projectile is active set it active in the player class


            //Main.NewText(projectile.timeLeft);

            if ((projectile.position.Y / 16) > Main.maxTilesY - 100) //check abd see if the projectile is in the underworld if so destroy at maxtilesy - 100
            {
                projectile.Kill();
            }

            if ((projectile.position.Y / 16) > Main.worldSurface * 0.35)
            {
                //Main.NewText("Set");
                projectile.tileCollide = true;
            }

            //Main.NewText("World: " + Main.worldSurface * 0.35);
            //Main.NewText("Projectile: " + projectile.position.Y / 16);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];

            Vector2 position = projectile.Center;

            //set the shader
            ExtraExplosives.NukeHit = true;
            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {

                Filters.Scene.Activate("BigBang", player.Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
                //float progress = 0f;
                //Filters.Scene["Bang"].GetShader().UseProgress(progress).UseOpacity(0);

            }

            //Create Bomb Damage
            ExplosionDamage(150f * 1.5f, projectile.Center, 3000, 1.0f, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, 150);

            for (int x = 0; x < 1; x++)
            {
                Projectile.NewProjectile(position.X, position.Y, Main.rand.Next(40) + 10, Main.rand.Next(40) + 10, ModContent.ProjectileType<InvisibleNukeProjectile>(), 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square            
            }

            //Ending part that sets effects back to normal and sets the nuke active back to false.
            //player.ResetEffects();
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Explosion"));
            ExtraExplosives.NukeActive = false;
            ExtraExplosives.NukeActivated = false;
            Main.screenPosition = player.Center;

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
                        Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                                || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                                Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DesertFossil)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                            }
                        }
                        else //Breakable
                        {
                            //-----===THIS IS WHERE THE BOMBS UNIQUE CODE GOES===-----\\
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                            //-----===########################################===-----\\
                        }
                    }
                }
            }
        }

    }
}