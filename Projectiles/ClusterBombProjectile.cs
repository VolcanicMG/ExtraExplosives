using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class ClusterBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/cluster_gore";
        private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ClusterBomb");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 14;
            Projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            Projectile.width = 40;   //This defines the hitbox width
            Projectile.height = 40; //This defines the hitbox height
            Projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 150; //The amount of time the projectile is alive for
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)Projectile.Center.X, (int)Projectile.Center.Y);

            //Create Bomb Dust
            DustEffects();

            Explosion();

            ExplosionDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-3f, 0f);
            Vector2 gVel2 = new Vector2(1f, -3f);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);

        }

        public override void Explosion()
        {
            Player player = Main.player[Projectile.owner];

            Vector2 position = Projectile.Center;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int i = (int)(x + position.X / 16.0f);
                    int j = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(i, j))) //Circle
                    {
                        ushort tile = Main.tile[i, j].TileType;
                        if (!CanBreakTile(tile, pickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            if (Math.Abs(x) >= radius - 1 || Math.Abs(y) >= radius - 1 || Terraria.ID.TileID.Sets.Ore[Main.tile[i, j].TileType])
                            {
                                int type = Main.tile[i, j].TileType;
                                WorldGen.KillTile((int)(i), (int)(j), false, false, false);

                                if (Main.netMode == NetmodeID.MultiplayerClient) //update if in mp
                                {
                                    WorldGen.SquareTileFrame(i, j, true); //Updates Area
                                    NetMessage.SendData(MessageID.TileChange, -1, -1, null, 2, (float)i, (float)j, 0f, 0, 0, 0);
                                }

                                if (player.EE().DropOresTwice && Main.rand.NextFloat() <= player.EE().dropChanceOre) //chance to drop 2 ores
                                {
                                    WorldGen.PlaceTile(i, j, type);
                                    WorldGen.KillTile((int)(i), (int)(j), false, false, false);

                                    if (Main.netMode == NetmodeID.MultiplayerClient)
                                    {
                                        WorldGen.SquareTileFrame(i, j, true); //Updates Area
                                        NetMessage.SendData(MessageID.TileChange, -1, -1, null, 2, (float)i, (float)j, 0f, 0, 0, 0);
                                    }
                                }
                            }
                            else
                            {

                                if (Main.rand.Next(100) == 1) Projectile.NewProjectile(position.X + x, position.Y + y, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), ModContent.ProjectileType<ClusterBombChildProjectile>(), 150, 10, Projectile.owner);
                                if (Main.player[Projectile.owner].EE().BombardEmblem) continue;
                                Main.tile[i, j].ClearTile();
                                //WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                                if (CanBreakWalls) WorldGen.KillWall(i, j, false); //This destroys Walls
                            }
                        }
                    }
                }
            }
        }
    }
}