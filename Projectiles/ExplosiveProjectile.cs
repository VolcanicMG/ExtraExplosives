using System;
using ExtraExplosives.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public abstract class ExplosiveProjectile : ModProjectile
    {
        public bool IgnoreTrinkets = false;
        
        public readonly bool Explosive = true;              // This marks the item as part of the explosive class
        public int radius = 0;                                  // Radius of the explosion
        public int pickPower = 0;                           // Strength of the explosion
        internal bool crit = false;                         // If it crits (dont edit this it is used internally Left internal so other bombs can edit the crit chances 
        protected LegacySoundStyle[] explodeSounds;         // The sounds that are played as the bomb explodes
        protected abstract string explodeSoundsLoc { get; } // Where the explosion sound files are located (relative to project dir)
        protected abstract string goreFileLoc { get; }      // Where the explosion gore sprites are located (relative to project dir)

        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            //constants throughout all bombs
            SafeSetDefaults();
            projectile.melee = false;
            projectile.ranged = false;
            projectile.magic = false;
            projectile.thrown = false;
            projectile.minion = false;
            DangerousSetDefaults();
        }

        public virtual void DangerousSetDefaults()
        {
            // Does nothing, this should be used to override the values locked by SetDefaults
            // Only use if you need to since those values are set to ensure the bombs function as intended
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            return;
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            return;
        }

        /// <summary>
        /// Takes the projectiles radius attribute in place of passing variables
        /// Creates a circular explosion in the radius defined
        /// Efficient but most blocks dont drop due to optimization methods (WIP)
        /// </summary>
        public virtual void Explosion()
        {
            
            // x and y are the tile offset of the current tile relative to the player
            // i and j are the true tile cords relative to 0,0 in the world
            Player player = Main.player[projectile.owner];
            if (pickPower < -1) return;
            if (player.EE().BombardEmblem) return;

            Vector2 position = new Vector2(projectile.Center.X / 16f, projectile.Center.Y / 16f);    // Converts to tile cords for convenience

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
                    int i = (int) (x + position.X);
                    int j = (int) (y + position.Y);
                    if (!WorldGen.InWorld(i, j)) continue;
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        //Main.NewText($"({i}, {j})");
                        //Dust dust = Dust.NewDustDirect(new Vector2(i, j), 1, 1, 54);
                        //dust.noGravity = true;
                        if (!WorldGen.TileEmpty(i, j))
                        {
                            if (!CanBreakTile(Main.tile[i, j].type, pickPower)) continue;
                            if (!CanBreakTiles) continue;
                            // Using KillTile is laggy, use ClearTile when working with larger tile sets    (also stops sound spam)
                            // But it must be done on outside tiles to ensure propper updates so use it only on outermost tiles
                            if (Math.Abs(x) >= radius - 1 || Math.Abs(y) >= radius - 1)
                                WorldGen.KillTile((int) (i), (int) (j), false, false, false);
                            else Main.tile[i,j].ClearTile();   
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

        /// <summary>
        /// Cycles through every npc and player, checking the distance, and deals damage accordingly
        /// Damage is not dealt if Blast Shielding is equipped
        /// </summary>
        public virtual void ExplosionDamage()
        {
            if (Main.player[projectile.owner].EE().ExplosiveCrit > Main.rand.Next(1, 101)) crit = true;
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, projectile.Center);
                if (dist/16f <= radius)
                {
                    int dir = (dist > 0) ? 1 : -1;
                    npc.StrikeNPC(projectile.damage, projectile.knockBack, dir, crit);
                }
            }

            foreach (Player player in Main.player)
            {
                if (player == null || player.whoAmI == 255 || !player.active) return;
                if (!CanHitPlayer(player)) continue;
                if (player.EE().BlastShielding &&
                    player.EE().BlastShieldingActive) continue;
                float dist = Vector2.Distance(player.Center, projectile.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (dist/16f <= radius)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir);
                    player.hurtCooldowns[0] += 15;
                }
                if (Main.netMode != 0)
                {
                    NetMessage.SendPlayerHurt(projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir, crit, pvp: true, 0);
                }
            }
            
        }
    }
}