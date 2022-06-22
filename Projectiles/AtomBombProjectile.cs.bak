using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class AtomBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/atom_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Atom Bomb");
        }

        public override void SafeSetDefaults()
        {
            pickPower = -1; // Can destroy anything
            radius = 1;
            projectile.tileCollide = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;

            drawOffsetX = -15;
            drawOriginOffsetY = -15;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(1, projectile.Center, 5000, 1.0f, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Bottom, 40);

            Explosion();

            ExplosionDamage();

            //Create Bomb Dust
            DustEffects();

            //Create Bomb Gore
            Vector2 gVel1 = Vector2.One.RotatedBy(projectile.rotation);
            int gore1ID = mod.GetGoreSlot(goreFileLoc + "1");
            for (int num = 0; num < 4; num++)
            {
                Gore.NewGore(projectile.position + gVel1, gVel1, gore1ID, projectile.scale);
                gVel1 = gVel1.RotatedBy(Math.PI / 2.0);
            }
            Vector2 gVel2 = Vector2.One.RotatedBy(Math.PI / 4.0);
            Gore.NewGore(projectile.position + gVel2, gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
        }

        public override void Explosion()    // Special (more efficient) explosion, leaving it
        {
            int xPosition = (int)(projectile.Bottom.X / 16.0f);
            int yPosition = (int)(projectile.Bottom.Y / 16.0f);
            WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this make the explosion destroy tiles
        }
        
    }
}