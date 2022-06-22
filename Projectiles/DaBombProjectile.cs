using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class DaBombProjectile : ExplosiveProjectile
    {
        //Variables:
        public bool buffActive;
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "n/a";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DaBomb");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            pickPower = 50;
            radius = 20;
            Projectile.tileCollide = true;
            Projectile.width = 22;
            Projectile.height = 42;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 20;
            Projectile.timeLeft = 400;

            buffActive = true;
        }

        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];

            if (buffActive == true)
            {
                player.AddBuff(Mod.Find<ModBuff>("ExtraExplosivesDaBombBuff").Type, 50, false);
            }

            base.PostAI();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)Projectile.Center.X, (int)Projectile.Center.Y);

            //Create Bomb Dust
            ExplosionDust(radius, player.Center, new Color(255, 255, 255), new Color(189, 24, 22), 1);

            Explosion();
            ExplosionDamage();

            //Disables the debuff
            buffActive = false;
        }

        public override void ExplosionDamage()
        {
            Player playerO = Main.player[Projectile.owner];

            if (Main.player[Projectile.owner].EE().ExplosiveCrit > Main.rand.Next(1, 101)) crit = true;
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, playerO.Center);
                if (dist / 16f <= radius)
                {
                    int dir = (dist > 0) ? 1 : -1;
                    npc.StrikeNPC(Projectile.damage, Projectile.knockBack, dir, crit);
                }
            }

            foreach (Player player in Main.player)
            {
                if (player == null || player.whoAmI == 255 || !player.active) return;
                if (!CanHitPlayer(player)) continue;
                if (player.EE().BlastShielding &&
                    player.EE().BlastShieldingActive) continue;
                float dist = Vector2.Distance(player.Center, playerO.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (dist / 16f <= radius)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), dir);
                    player.hurtCooldowns[0] += 15;
                }
                if (Main.netMode != 0)
                {
                    NetMessage.SendPlayerHurt(Projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), dir, crit, pvp: true, 0);
                }
            }

        }

        public override void Explosion()
        {

            // x and y are the tile offset of the current tile relative to the player
            // i and j are the true tile cords relative to 0,0 in the world
            Player player = Main.player[Projectile.owner];
            if (pickPower < -1) return;
            if (player.EE().BombardEmblem) return;

            Vector2 position = new Vector2(player.Center.X / 16f, player.Center.Y / 16f);    // Converts to tile cords for convenience

            radius = (int)((radius + player.EE().RadiusBonus) * player.EE().RadiusMulti);
            for (int x = -radius;
                x <= radius;
                x++)
            {
                //int x = (int)(i + position.X);
                for (int y = -radius;
                    y <= radius;
                    y++)
                {
                    //int y = (int)(j + position.Y);
                    int i = (int)(x + position.X);
                    int j = (int)(y + position.Y);
                    if (!WorldGen.InWorld(i, j)) continue;
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        //Main.NewText($"({i}, {j})");
                        //Dust dust = Dust.NewDustDirect(new Vector2(i, j), 1, 1, 54);
                        //dust.noGravity = true;
                        if (!WorldGen.TileEmpty(i, j))
                        {
                            if (!CanBreakTile(Main.tile[i, j].TileType, pickPower)) continue;
                            if (!CanBreakTiles) continue;
                            // Using KillTile is laggy, use ClearTile when working with larger tile sets    (also stops sound spam)
                            // But it must be done on outside tiles to ensure propper updates so use it only on outermost tiles
                            if (Math.Abs(x) >= radius - 1 || Math.Abs(y) >= radius - 1)
                                WorldGen.KillTile((int)(i), (int)(j), false, false, false);
                            else
                            {
                                if (!TileID.Sets.BasicChest[Main.tile[i, j - 1].TileType] && !TileLoader.IsDresser(Main.tile[i, j - 1].TileType))
                                {
                                    Main.tile[i, j].ClearTile();
                                    Main.tile[i, j].HasTile = false;
                                }
                            }
                            //
                        }

                        if (CanBreakWalls)
                        {
                            //WorldGen.KillWall((int) (i), (int) (j));
                        }
                    }
                }
            }
        }
    }
}