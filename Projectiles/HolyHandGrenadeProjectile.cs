using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class HolyHandGrenadeProjectile : ExplosiveProjectile
    {

        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Holy_Hand_Granade_";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        private int rippleCount = 2;
        private int rippleSize = 10;
        private int rippleSpeed = 50;
        private float distortStrength = 150f;
        private int timeLeft = 90;

        private bool triggered;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Holy Hand Grenade");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 20;
            Projectile.tileCollide = true;
            Projectile.width = 32;
            Projectile.height = 38;
            Projectile.aiStyle = 16;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            //explodeSounds = new SoundStyle[3];
            /*for (int num = 1; num <= explodeSounds.Length; num++)
            {
                //explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }*/
        }

        public override void AI()
        {
            // ai[0] = state
            // 0 = unexploded
            // 1 = exploded

            if (Projectile.timeLeft <= timeLeft)
            {
                if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                {
                    Filters.Scene.Activate("Shockwave", Projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Projectile.Center);
                }

                if (!triggered) //So it only runs once while in AI()
                {
                    Projectile.alpha = 255; // Make the projectile invisible.

                    //Create Bomb Sound
                    ////SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

                    //Create Bomb Dust
                    DustEffects();

                    //Explosion();
                    ExplosionDamage();

                    triggered = true;
                }


                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float progress = (timeLeft - Projectile.timeLeft) / 60f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene["Shockwave"].Deactivate();
            }
        }

        public override void ExplosionDamage()
        {
            if (Main.player[Projectile.owner].EE().ExplosiveCrit > Main.rand.Next(1, 101)) crit = true;
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, Projectile.Center);
                if (dist / 16f <= radius)
                {
                    int dir = (dist > 0) ? 1 : -1;
                    if (DamageReducedNps.Contains(npc.type))
                    {
                        npc.StrikeNPC((int)(Projectile.damage * .5f), Projectile.knockBack, dir, crit);
                    }
                    else if(npc.boss && !DamageReducedNps.Contains(npc.type)) npc.StrikeNPC(Projectile.damage * 2, Projectile.knockBack, dir, crit);
                    else npc.StrikeNPC(Projectile.damage, Projectile.knockBack, dir, crit);
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
                if (dist / 16f <= radius && Main.netMode == NetmodeID.SinglePlayer && InflictDamageSelf)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), dir);
                    player.hurtCooldowns[0] += 15;
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient && dist / 16f <= radius && player.whoAmI == Projectile.owner && InflictDamageSelf)
                {
                    NetMessage.SendPlayerHurt(Projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), (int)(Projectile.damage * (crit ? 1.5 : 1)), dir, crit, pvp: true, 0);
                }
            }
        }
    }
}