using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class HeavyBombProjectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HeavyBomb");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 13;   //This defines the hitbox width
            projectile.height = 19;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 1000; //The amount of time the projectile is alive for
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item37, (int)position.X, (int)position.Y);
            int radius = 2;     //this is the explosion radius, the highter is the value the bigger is the explosion

            ExplosionDamageProjectile.DamageRadius = (float)(radius * 2f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 30, 40, projectile.owner, 0.0f, 0);

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (xPosition > 0 && yPosition > 0 && xPosition < Main.maxTilesX && yPosition < Main.maxTilesY))    //this make so the explosion radius is a circle
                    {
                        if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.DesertFossil || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
                            || Main.tile[xPosition, yPosition].type == TileID.PinkDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
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
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }
                        else if (ThoriumMod != null && (Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("Aquaite") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("LodeStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("ValadiumChunk") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("IllumiteChunk")
                            || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("PearlStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("DepthChestPlatform")))
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }
                        else
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles  
                                                                                           //Dust.NewDust(position, 22, 22, DustID.Smoke, 0.0f, 0.0f, 120, new Color(), 1f);  //this is the dust that will spawn after the explosion
                        }
                    }
                }
            }


            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                Vector2 vev = new Vector2(position.X - (30 / 2), position.Y - 0);
                dust = Main.dust[Terraria.Dust.NewDust(vev, 30, 30, 1, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            }
            return true;
        }

        public override void Kill(int timeLeft)
        {

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 20;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 500, 40, projectile.owner, 0.0f, 0);

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
                        else if (CalamityMod != null && (Main.tile[xPosition, yPosition].type == CalamityMod.TileType("SeaPrism") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AerialiteOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CryonicOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CharredOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PerennialOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ScoriaOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AstralOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("UelibloomOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AuricOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AbyssGravel") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Voidstone") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PlantyMush")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Tenebris") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ArenaBlock") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Cinderplate") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumClusterOre")))
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }
                        else if (ThoriumMod != null && (Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("Aquaite") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("LodeStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("ValadiumChunk") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("IllumiteChunk")
                            || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("PearlStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("DepthChestPlatform")))
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }
                        else
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this make the explosion destroy tiles  
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                        }

                        if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                        {
                            if (Main.rand.NextFloat() < 0.5f)
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


                                Dust dust3;
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                dust = Main.dust[Terraria.Dust.NewDust(vev, Hw, Hw, 31, 0f, 0f, 0, new Color(255, 255, 255), scale)];
                                dust.noGravity = true;
                                dust.noLight = true;
                            }
                        }
                    }
                }
            }


        }
    }
}