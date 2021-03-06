using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class DynaglowmiteProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Dynaglowmite_";
        protected override string goreFileLoc => "Gores/Explosives/Dynaglowmite_Gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynaglowmite");
        }

        public override void SafeSetDefaults()
        {
            pickPower = -2;
            radius = 10;
            projectile.tileCollide = true;
            projectile.width = 16;
            projectile.height = 32;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 45;
            projectile.damage = 0;
            //projectile.light = .9f;
            //projectile.glowMask = 2;
            explodeSounds = new LegacySoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, explodeSoundsLoc + num);
            }
        }

        public override void PostAI()
        {
            Lighting.AddLight(projectile.position, new Vector3(.1f, 1f, 2.2f));
            Lighting.maxX = 10;
            Lighting.maxY = 10;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 0);

            Explosion();

            //Create Bomb Dust
            DustEffects(type: 1, shake: false, dustType: 91, color: new Color(0, 67, 255));

            //Create Bomb Gore
            int goreType = Main.rand.Next(2);
            Vector2 gVel = Vector2.One.RotatedByRandom(Math.PI * 2) * 2;
            if (goreType == 0)
                for (int num = 0; num < 2; num++)
                {
                    Gore.NewGore(projectile.position + Vector2.Normalize(gVel), gVel.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
                    gVel = gVel.RotatedByRandom(Math.PI * 2);
                }
            else
                Gore.NewGore(projectile.position + Vector2.Normalize(gVel), gVel.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);

        }

        public override void Explosion()
        {
            Vector2 position = projectile.Center;
            float x = 0;
            float y = 0;
            float speedX = -22f;
            float speedY = -22f;
            float[] z = { .1f, .2f, .3f, .4f, .5f, .6f, .7f, .8f };
            int yCntr = 1;
            int xCntr = 1;

            for (y = position.Y - 70; y < position.Y + 71; y++)
            {
                for (x = position.X - 70; x < position.X + 71; x++)
                {
                    speedX += 5.5f; //Change X Velocity

                    if (speedX < 0f)
                        speedX -= z[Main.rand.Next(7)];

                    if (speedX > 0f)
                        speedX += z[Main.rand.Next(7)];

                    if (yCntr == 1 || yCntr == 7)
                        Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square

                    if ((xCntr == 1 || xCntr == 7) && (yCntr != 1 || yCntr != 7))
                        Projectile.NewProjectile(x, y, speedX, speedY, ProjectileID.StickyGlowstick, 0, 0, projectile.owner, 0.0f, 0); //Spawns in the glowsticks in square

                    x = x + 20;
                    xCntr++;
                }

                y = y + 20;
                speedY += 5.5f; //Change Y Velocity
                speedX = -22f; //Reset X Velocity
                xCntr = 1;
                yCntr++;
            }
        }
    }
}