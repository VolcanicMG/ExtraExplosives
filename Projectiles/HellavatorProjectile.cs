using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class HellavatorProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Hellavator_1";
        protected override string goreFileLoc => "Gores/Explosives/hellevator_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hellavator Projectile");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            radius = 0;
            pickPower = 40;
            Projectile.tileCollide = true;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.damage = 0;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;
            explodeSounds = new SoundStyle[] {
                new SoundStyle(explodeSoundsLoc)
            };
        }

        public override bool OnTileCollide(Vector2 old)
        {
            Projectile.Kill();

            return true;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(explodeSounds[0]);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 0);

            Explosion();
            ExplosionDamage();

            //Create Bomb Dust
            CreateDust(Projectile.Center, 400);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-2f, 2f);
            Vector2 gVel2 = new Vector2(2f, -2f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = Projectile.Center;
            int width = 3; //Explosion Width for both sides starting from the center
            int height = Main.maxTilesY - 10; //Explosion Height

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            for (int x = -width; x < width; x++)
            {
                for (int y = 0; y <= height; y++)
                {
                    int i = (int)(x + position.X / 16.0f);
                    int j = (int)(y + position.Y / 16.0f);
                    
                    if (WorldGen.InWorld(i, j))
                    {
                        Tile tile = Framing.GetTileSafely(i, j);
                        ushort tileP = tile.TileType;

                        if (!CanBreakTile(tileP, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            /*if TODO (!TileID.Sets.BasicChest[Main.tile[i, j - 1].TileType] && !TileLoader.IsDresser(Main.tile[i, j - 1].TileType) && Main.tile[i, j - 1].TileType != 26)
                            {
                                tile.ClearTile();
                                tile.HasTile = false;

                            }*/
                            if (CanBreakWalls) WorldGen.KillWall(i, j, false); //This destroys Walls
                            if (CanBreakWalls && y - 1 != height) WorldGen.KillWall(i + 1, j + 1, false); //Break the last bit of wall
                            NetMessage.SendTileSquare(-1, i, j, 1);
                        }

                        Main.tile[i, j].LiquidAmount = LiquidID.Water; //This destroys liquids
                        WorldGen.SquareTileFrame(i, j, true); //Updates Area
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
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 0, 0f, 0f, 171, new Color(33, 0, 255), 5.0f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 5) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                        }
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        updatedPosition = new Vector2(position.X - 10 / 2, position.Y - 10 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 10, 10, 148, 0f, 0.2631581f, 120, new Color(255, 226, 0), 2.039474f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 5) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(111, Main.LocalPlayer);
                            dust.fadeIn = 3f;
                        }
                    }
                    //------------
                }
            }
        }
    }
}