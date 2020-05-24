using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class NukeProjectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

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
            projectile.netUpdate = true;
            //projectile.scale = 1.5f;
        }

        public override void AI()
        {
            //send the projectiles postion to the player's camera and set NukeActive to true
            ExtraExplosivesPlayer.NukePos = projectile.Center;
            ExtraExplosivesPlayer.NukeActive = true; //since the projectile is active set it active in the player class


            //Main.NewText(projectile.timeLeft);

            if ((projectile.position.Y / 16) > Main.maxTilesY - 100) //check abd see if the projectile is in the underworld if so destroy at maxtilesy - 100
            {
                projectile.Kill();
            }

            if ((projectile.position.Y / 16) > Main.worldSurface * 0.25)
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

            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Explosion"));
            //set the shader
            ExtraExplosivesPlayer.NukeHit = true;
            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {

                Filters.Scene.Activate("BigBang", player.Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
                //float progress = 0f;
                //Filters.Scene["Bang"].GetShader().UseProgress(progress).UseOpacity(0);

            }


            int radius = 150;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 3000, 100, projectile.owner, 0.0f, 0);

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius - 3 + 0.5 && (xPosition > 0 && yPosition > 0 && xPosition < Main.maxTilesX && yPosition < Main.maxTilesY))   //this make so the explosion radius is a circle
                    {
                        Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
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
                                //if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }
                        else if (ThoriumMod != null && (Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("Aquaite") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("LodeStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("ValadiumChunk") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("IllumiteChunk")
                            || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("PearlStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("DepthChestPlatform")))
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                                //if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }

                        else
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles  
                            //if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                        }
                    }
                    //if (Math.Sqrt(x * x + y * y) > radius - 3 + 0.5 && Math.Sqrt(x * x + y * y) < radius + 0.5 && y > 0 && x > -radius+3 && x < radius - 3) //Places Tiles
                    //{
                    //    WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                    //    WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSurfaceTile>());
                    //}
                }
            }

            for (int x = 0; x < 1; x++)
            {
                Projectile.NewProjectile(position.X, position.Y, Main.rand.Next(40) + 10, Main.rand.Next(40) + 10, ModContent.ProjectileType<InvisibleNukeProjectile>(), 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square            
            }

            //player.ResetEffects();
            ExtraExplosivesPlayer.NukeActive = false;
            ExtraExplosives.NukeActivated = false;
            Main.screenPosition = player.Center;

        }
    }
}