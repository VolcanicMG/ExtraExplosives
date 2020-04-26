using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.NPCs;

namespace ExtraExplosives.Projectiles
{
    public class PhaseBombProjectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PhaseBomb");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
            Main.projFrames[projectile.type] = 10;

        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 22;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 1000; //The amount of time the projectile is alive for
        }

        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            if (player.releaseUseItem)
            {
                projectile.timeLeft = 0;
            }
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                //projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                if (++projectile.frame >= 10)
                {
                    projectile.frame = 0;
                }
            }

        }

        public override void Kill(int timeLeft)
        {

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 20;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 2f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 450, 40, projectile.owner, 0.0f, 0);

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
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Dust dust;
                                Vector2 position1 = new Vector2(position.X - 600 / 2, position.Y - 600 / 2);
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                dust = Main.dust[Terraria.Dust.NewDust(position1, 600, 600, 155, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                                dust.noGravity = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(105, Main.LocalPlayer);



                                Dust dust2;
                                Vector2 position2 = new Vector2(position.X - 650 / 2, position.Y - 650 / 2);
                                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                dust2 = Main.dust[Terraria.Dust.NewDust(position2, 600, 600, 49, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                                dust2.noGravity = true;
                                dust2.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                            }
                        }
                    }
                }
            }
        }
    }
}