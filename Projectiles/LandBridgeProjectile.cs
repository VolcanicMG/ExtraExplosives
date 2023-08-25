﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class LandBridgeProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/land-bridge_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("LandBridge");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            Projectile.tileCollide = true;
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Projectile.position.Y -= 2;

            Projectile.velocity = Vector2.Zero;

            Projectile.aiStyle = 0;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner); //No damage needed

            //Create Bomb Explosion
            Explosion();

            //Create Bomb Dust
            CreateDust(Projectile.Center, 500);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(0f, -4f);
            Vector2 gVel2 = new Vector2(4f, 4f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = Projectile.Center;

            int height = 10; //Height of arena

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            for (int x = 3; x <= Main.maxTilesX - 3; x++)
            {
                for (int y = height; y >= 0; y--)
                {
                    int xPosition = x;
                    int yPosition = (int)(-y + position.Y / 16.0f);

                    Tile tile = Framing.GetTileSafely(xPosition, yPosition);

                    //The following happens whether the block is breakable or not as the following methods cannot break or replace blocks that already exist.
                    if (!OutOfBounds(xPosition, yPosition) && !tile.HasTile)
                    {
                        //Breaks Liquid
                        Main.tile[xPosition, yPosition].LiquidAmount = LiquidID.Water;
                        WorldGen.SquareTileFrame(xPosition, yPosition, true);

                        //Place Outline
                        if ((y == 0) || y == height)
                        {
                            WorldGen.PlaceTile(xPosition, yPosition, TileID.Platforms);
                        }
                        if (y >= 1 && y < height) { WorldGen.PlaceWall(xPosition, yPosition, WallID.GrayBrick); }
                        if (y == height / 2 && x % 6 == 0) { WorldGen.PlaceTile(xPosition, yPosition, TileID.Torches); }
                        NetMessage.SendTileSquare(-1, xPosition, yPosition, 1);
                    }
                }
            }
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 2000, 186, 0f, 0f, 0, new Color(159, 0, 255), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 1000) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 2000, 186, 0f, 0f, 0, new Color(0, 17, 255), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 1000) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 2000 / 2, position.Y - 2000 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 2000, 2000, 186, 0f, 0f, 0, new Color(255, 0, 150), 5f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 1000) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }
                }
            }
        }

        //This returns true if the arena is going out of bounds
        private bool OutOfBounds(int posX, int posY)
        {
            //Tests If Tile Is OutOfBounds
            if (posX <= 2.5f || posY <= 2.5f || posX > Main.maxTilesX || posY > Main.maxTilesY)
            {
                return true;
            }
            return false;
        }
    }
}