using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles.Weapons.NovaBuster
{
    public class NovaBusterProjectile : ModProjectile
    {
        private int radius = 10;
        private bool crit;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NovaBuster");
        }

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.height = 20;
            projectile.width = 12;
            projectile.tileCollide = true;
            projectile.aiStyle = 16;
            projectile.timeLeft = 200;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            CreateDust(projectile.Center, 100);
            ExplosionDamage();
        }

        public override void AI()
        {
            
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
                if (dist / 16f <= radius)
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
                if (dist / 16f <= radius && Main.netMode == NetmodeID.SinglePlayer)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir);
                    player.hurtCooldowns[0] += 15;
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient && dist / 16f <= radius)
                {
                    NetMessage.SendPlayerHurt(projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir, crit, pvp: true, 0);
                }
            }

        }


        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    //Dust 1
                    if (Main.rand.NextFloat() < 0.9f)
                    {
                        //updatedPosition = new Vector2(position.X, position.Y);

                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 2.5f;
                        }
                    }

                    //Dust 2
                    if (Main.rand.NextFloat() < 0.6f)
                    {
                        //updatedPosition = new Vector2(position.X - 78 / 2, position.Y - 78 / 2);

                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }

                    //Dust 3
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        //updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);
                        updatedPosition = new Vector2(position.X - radius * 8, position.Y - radius * 8);
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                }
            }
        }

    }
}