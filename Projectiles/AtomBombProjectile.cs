using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class AtomBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "atom_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Atom Bomb");
        }

        public override void SafeSetDefaults()
        {
            pickPower = -1; // Can destroy anything
            radius = 1;
            Projectile.tileCollide = true;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            //Create Bomb Damage
            //ExplosionDamage(1, projectile.Center, 5000, 1.0f, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Bottom, 40);

            ExplosionTileDamage();

            ExplosionEntityDamage();

            //Create Bomb Dust
            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = Vector2.One.RotatedBy(Projectile.rotation);
            int gore1ID = Mod.Find<ModGore>($"{goreName}1").Type;
            for (int num = 0; num < 4; num++)
            {
                Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + gVel1, gVel1, gore1ID, Projectile.scale);
                gVel1 = gVel1.RotatedBy(Math.PI / 2.0);
            }
            Vector2 gVel2 = Vector2.One.RotatedBy(Math.PI / 4.0);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + gVel2, gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);
        }

        public override void ExplosionTileDamage()    // Special (more efficient) explosion, leaving it
        {
            int xPosition = (int)(Projectile.Bottom.X / 16.0f);
            int yPosition = (int)(Projectile.Bottom.Y / 16.0f);
            WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this make the explosion destroy tiles
        }
        
    }
}