using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class MegaExplosiveProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Mega_Explosive_";
        protected override string goreName => "basic-explosive_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("MegaExplosive");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 65;
            radius = 40;
            Projectile.tileCollide = true;
            Projectile.width = 32;
            Projectile.height = 38;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            //projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 500;
            explodeSounds = new SoundStyle[2];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

            /* ===== ABOUT THE BOMB SOUND =====
			 * 
			 * Because the KillTile() and KillWall() methods used in CreateExplosion()
			 * produce a lot of sounds, the bomb's own explosion sound is difficult to
			 * hear. The solution to eliminate those unnecessary sounds is to alter
			 * the fields of each Tile that the explosion affects, but this creates
			 * additional problems (no dropped Tile items, adjacent Tiles not updating
			 * their sprites, etc). I've decided to ignore doing the changes because
			 * it would entail making the same changes to multiple projectiles and the
			 * projectile template.
			 * 
			 * -- V8_Ninja
			 */

            //Create Bomb Dust
            DustEffects();

            ExplosionTileDamage();
            ExplosionEntityDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(4.0f, 0.0f);
            Vector2 gVel2 = new Vector2(0.0f, -4.0f);
            gVel1 = gVel1.RotatedBy(Projectile.rotation);
            gVel2 = gVel2.RotatedBy(Projectile.rotation);
            for (int num = 0; num < 4; num++)
            {
                Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1, Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale * 1.5f);
                Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2, Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale * 1.5f);
                gVel1 = gVel1.RotatedBy(Math.PI / 4);
                gVel2 = gVel2.RotatedBy(Math.PI / 4);
            }
        }
    }
}