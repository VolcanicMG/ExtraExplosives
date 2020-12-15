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

        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Holy_Hand_Granade_";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        private int rippleCount = 2;
        private int rippleSize = 10;
        private int rippleSpeed = 50;
        private float distortStrength = 150f;
        private int timeLeft = 90;

        private bool triggered;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Hand Grenade");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 20;
            projectile.tileCollide = true;
            projectile.width = 32;
            projectile.height = 38;
            projectile.aiStyle = 16;
            projectile.friendly = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 400;
            explodeSounds = new LegacySoundStyle[3];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
        }

        public override void AI()
        {
            // ai[0] = state
            // 0 = unexploded
            // 1 = exploded

            if (projectile.timeLeft <= timeLeft)
            {
                if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                {
                    Filters.Scene.Activate("Shockwave", projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(projectile.Center);
                }

                if (!triggered) //So it only runs once while in AI()
                {
                    projectile.alpha = 255; // Make the projectile invisible.

                    //Create Bomb Sound
                    Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

                    //Create Bomb Dust
                    DustEffects();

                    //Explosion();
                    ExplosionDamage();

                    triggered = true;
                }


                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float progress = (timeLeft - projectile.timeLeft) / 60f;
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
            if (Main.player[projectile.owner].EE().ExplosiveCrit > Main.rand.Next(1, 101)) crit = true;
            foreach (NPC npc in Main.npc)
            {
                float dist = Vector2.Distance(npc.Center, projectile.Center);
                if (dist / 16f <= radius)
                {
                    int dir = (dist > 0) ? 1 : -1;
                    if (DamageReducedNps.Contains(npc.type))
                    {
                        npc.StrikeNPC((int)(projectile.damage * .5f), projectile.knockBack, dir, crit);
                    }
                    else if(npc.boss && !DamageReducedNps.Contains(npc.type)) npc.StrikeNPC(projectile.damage * 2, projectile.knockBack, dir, crit);
                    else npc.StrikeNPC(projectile.damage, projectile.knockBack, dir, crit);
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
                if (dist / 16f <= radius && Main.netMode == NetmodeID.SinglePlayer && InflictDamageSelf)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir);
                    player.hurtCooldowns[0] += 15;
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient && dist / 16f <= radius && player.whoAmI == projectile.owner && InflictDamageSelf)
                {
                    NetMessage.SendPlayerHurt(projectile.owner, PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), (int)(projectile.damage * (crit ? 1.5 : 1)), dir, crit, pvp: true, 0);
                }
            }
        }
    }
}