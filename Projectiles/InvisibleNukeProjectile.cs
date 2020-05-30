using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraExplosives.Tiles;

namespace ExtraExplosives.Projectiles
{
    public class InvisibleNukeProjectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InvisibleNukeExplosive");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile";

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 44;   //This defines the hitbox width
            projectile.height = 44;    //This defines the hitbox height
            projectile.aiStyle = 1;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            //projectile.timeLeft = 100000; //The amount of time the projectile is alive for
            projectile.scale = 1.5f;
            projectile.Opacity = 0;
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

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 8;     //this is the explosion radius, the highter is the value the bigger is the explosion

            bool placeTile = false;
            int placeTileAmount = Main.rand.Next(5);

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 3000, 100, projectile.owner, 0.0f, 0);

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

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
                    else //WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                    {
                        if (Main.tile[xPosition, yPosition].type != null)
                        {

                        }
                        else
                        {
                            placeTile = true;

                            if(placeTileAmount-- > 0)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                                WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSurfaceTile>());
                            }
                        }
                    }
                    
                }
                placeTile = false;
                placeTileAmount = Main.rand.Next(5);
            }

        }


    }
}