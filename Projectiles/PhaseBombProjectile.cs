using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Drawing;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class PhaseBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Phase_Bomb_Explode_";
        protected override string goreName => "phase_gore";
        //private Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //private Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");
        internal static bool CanBreakWalls;
        private SoundStyle phaseSound;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 10;
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 10;
            Projectile.tileCollide = false; //checks to see if the projectile can go through tiles
            Projectile.width = 22;   //This defines the hitbox width
            Projectile.height = 22; //This defines the hitbox height
            Projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 100; //The amount of time the projectile is alive for
            phaseSound = new SoundStyle("ExtraExplosives/Assets/Sounds/Custom/Explosives/Phase_Bomb");
            if (!Main.dedServ)
            {
                phaseSound.Volume = 0.5f;
            }
            explodeSounds = new SoundStyle[3];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void AI()
        {
            // Not needed ?
            /*if (SoundEngine.TryGetActiveSound(phaseSound.))
                phaseSound = SoundEngine.PlaySound(phaseSound);*/

            //if (!SoundEngine.FindActiveSound(phaseSound).IsPlaying)
            //    SoundEngine.PlaySound(phaseSound);

            if (!Main.mouseLeft && Projectile.timeLeft <= 50)
            {
                //SoundEngine.FindActiveSound(phaseSound).Stop();
                Projectile.Kill();
            }

            //Run through the frames
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 10)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            ExplosionTileDamage();

            //Create Bomb Damage
            ExplosionEntityDamage();

            ExplosionDust(radius, Projectile.Center, type: 1, shake: false, dustType: 155);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(0.0f, 3.0f);
            Vector2 gVel2 = new Vector2(0.0f, -3.0f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }
    }
}