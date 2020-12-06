using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class MegaExplosiveProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Mega_Explosive_";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MegaExplosive");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 65;
            radius = 40;
            projectile.tileCollide = true;
            projectile.width = 32;
            projectile.height = 38;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            //projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 500;
            explodeSounds = new LegacySoundStyle[2];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

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

            Explosion();
            ExplosionDamage();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(4.0f, 0.0f);
            Vector2 gVel2 = new Vector2(0.0f, -4.0f);
            gVel1 = gVel1.RotatedBy(projectile.rotation);
            gVel2 = gVel2.RotatedBy(projectile.rotation);
            for (int num = 0; num < 4; num++)
            {
                Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1, mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale * 1.5f);
                Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2, mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale * 1.5f);
                gVel1 = gVel1.RotatedBy(Math.PI / 4);
                gVel2 = gVel2.RotatedBy(Math.PI / 4);
            }
        }
    }
}