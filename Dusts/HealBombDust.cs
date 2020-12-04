using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Dusts
{
    public class HealBombDust : ModDust
    {

        public override void OnSpawn(Dust HealBombDust)
        {
            HealBombDust.noGravity = true;
            HealBombDust.frame = new Rectangle(0, 0, 18, 18);
            HealBombDust.scale = 1f;
        }

        public override bool Update(Dust HealBombDust)
        {
            HealBombDust.position += HealBombDust.velocity;
            HealBombDust.scale -= 0.01f;
            if (HealBombDust.scale < 0.1f)
            {
                HealBombDust.active = false;
            }
            return false;
        }
    }
}