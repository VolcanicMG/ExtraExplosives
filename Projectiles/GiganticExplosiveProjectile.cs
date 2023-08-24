using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class GiganticExplosiveProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Gigantic_Explosion_";
        protected override string goreFileLoc => "Gores/Explosives/gigantic-explosive_gore";
        private SoundStyle fuseSound;
        private bool fusePlayed = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("GiganticExplosive");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 70;
            radius = 80;
            Projectile.tileCollide = true;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            //projectile.scale = 1.5f;
            fuseSound = new SoundStyle(explodeSoundsLoc + "Wick");
            if (!Main.dedServ)
            {
                // fuseSound = fuseSound.WithVolume(0.5f);
                fuseSound.Volume = 0.5f; // This is the fix, i think, dont quote me tho
            }
            explodeSounds = new SoundStyle[2];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void AI()
        {
            if (!fusePlayed)
            {
                SoundEngine.PlaySound(fuseSound);
                fusePlayed = true;
            }
            Projectile.rotation = 0;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

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
            //CreateDust(projectile.Center, 300);


            Explosion();
            ExplosionDamage();
            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(0f, 3f);
            Vector2 gVel2 = new Vector2(-3f, -3f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }
    }
}