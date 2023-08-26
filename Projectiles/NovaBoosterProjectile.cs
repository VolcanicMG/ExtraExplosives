using Microsoft.Xna.Framework;
using Terraria;

namespace ExtraExplosives.Projectiles
{
    public class NovaBoosterProjectile : LargeExplosiveProjectile
    {
        public override string Texture { get; } = "ExtraExplosives/Projectiles/InvisibleProjectile";

        public override void Explosion()
        {
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
                    // TODO npc.StrikeNPC(Projectile.damage, Projectile.knockBack, dir, crit);
                }
            }
        }
    }
}