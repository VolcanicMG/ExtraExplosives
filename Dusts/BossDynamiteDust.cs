using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Dusts
{
    public class BossDynamiteDust : ModDust
    {
        public override void OnSpawn(Dust bossDynamiteDust)
        {
            bossDynamiteDust.noGravity = true;
            bossDynamiteDust.frame = new Rectangle(0, 10 * Main.rand.Next(3), 10, 10);
        }

        public override bool Update(Dust bossDynamiteDust)
        {
            bossDynamiteDust.position += bossDynamiteDust.velocity;
            bossDynamiteDust.scale -= 0.01f;
            if (bossDynamiteDust.scale < 0.75f)
            {
                bossDynamiteDust.active = false;
            }
            return false;
        }

    }
}