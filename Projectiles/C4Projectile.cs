using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace ExtraExplosives.Projectiles
{
    public class C4Projectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;
        internal static bool CanBreakTiles;
        //internal static bool detonate;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("C4");

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

            if (projectile.owner == Main.myPlayer)
            {
                var player = Main.player[projectile.owner].GetModPlayer<ExtraExplosivesPlayer>();

                if (player.detonate == true)
                {
                    projectile.Kill();
                }
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
            Dust dust1;
            Dust dust2;
            Dust dust3;
            Dust dust4;

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 20;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 1000, 40, projectile.owner, 0.0f, 0);

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

                            if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.BlueDungeonBrick || Main.tile[xPosition, yPosition].type == TileID.GreenDungeonBrick
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
                                if (Main.rand.NextFloat() < 0.3f)
                                {
                                    Vector2 position3 = new Vector2(position.X - 360 / 2, position.Y - 360 / 2);
                                    dust3 = Main.dust[Terraria.Dust.NewDust(position3, 360, 360, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                                    dust3.noGravity = true;
                                    dust3.noLight = true;
                                    dust3.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);

                                    Vector2 position1 = new Vector2(position.X - 642 / 2, position.Y - 642 / 2);
                                    dust1 = Main.dust[Terraria.Dust.NewDust(position1, 642, 642, 56, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
                                    dust1.noGravity = true;
                                    dust1.noLight = true;
                                    dust1.shader = GameShaders.Armor.GetSecondaryShader(91, Main.LocalPlayer);

                                    Vector2 position2 = new Vector2(position.X - 560 / 2, position.Y - 560 / 2);
                                    dust2 = Main.dust[Terraria.Dust.NewDust(position2, 560, 560, 6, 0f, 0.5263162f, 0, new Color(255, 150, 0), 5f)];
                                    dust2.noGravity = true;
                                    dust2.noLight = true;
                                    dust2.fadeIn = 3f;

                                    Vector2 position4 = new Vector2(position.X - 157 / 2, position.Y - 157 / 2);
                                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                                    dust4 = Main.dust[Terraria.Dust.NewDust(position4, 157, 157, 55, 0f, 0f, 0, new Color(255, 100, 0), 3.552631f)];
                                    dust4.noGravity = true;
                                    dust4.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                                }
                            }

                        }
                    }
                }
            }
        }


    }
}

