using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

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
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.height = 20;
            Projectile.width = 12;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 16;
            Projectile.timeLeft = 200;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            ExplosionDust(radius, Projectile.Center, shake: false);
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
            if (Main.player[Projectile.owner].EE().ExplosiveCrit > Main.rand.Next(1, 101)) crit = true;
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, Projectile.Center);
                if (dist / 16f <= radius && !npc.friendly)
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
                float dist = Vector2.Distance(player.Center, Projectile.Center);
                int dir = (dist > 0) ? 1 : -1;
                if (dist / 16f <= radius && Main.netMode == NetmodeID.SinglePlayer)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), dir);
                    player.hurtCooldowns[0] += 15;
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient && dist / 16f <= radius)
                {
                    NetMessage.SendPlayerHurt(Projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), dir, crit, pvp: true, 0);
                }
            }

        }
    }
}