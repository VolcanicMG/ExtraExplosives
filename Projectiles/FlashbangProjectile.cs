using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using System;
using ExtraExplosives.Buffs;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    internal class FlashbangProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "flashbang_gore";

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            Projectile.tileCollide = true;
            Projectile.width = 12;
            Projectile.height = 32;
            Projectile.hostile = true;
            Projectile.aiStyle = 16;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 70;
            Projectile.damage = 0;
        }

        public override void Kill(int timeLeft)
        {
            //add lighting
            Lighting.AddLight(Projectile.position, new Vector3(255f, 255f, 255f));

            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center); //Sound Effect
            
            foreach(Player player in Main.player)
            {
                float dist = Projectile.position.Distance(player.position);
                if (dist < 300)
                {
                    player.AddBuff(BuffID.Confused, 300);
                    player.AddBuff(BuffID.Dazed, 300);
                    player.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
                }
            }

            foreach (NPC npc in Main.npc)
            {
                float dist = Projectile.position.Distance(npc.position);
                if (dist < 300)
                {
                    npc.AddBuff(BuffID.Confused, 300);
                    npc.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
                }
            }
            Vector2 gVel1 = new Vector2(-2f, 2f);
            Vector2 gVel2 = new Vector2(2f, -2f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
            DustEffects();
        }
    }
}