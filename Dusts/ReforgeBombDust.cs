using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Dusts
{
    public class ReforgeBombDust : ModDust
    {
        public override void OnSpawn(Dust dustReforge)
        {
            dustReforge.noGravity = true;
            dustReforge.frame = new Rectangle(0, 0, 26, 24);
        }

        public override bool Update(Dust dustReforge)
        {
            dustReforge.position += dustReforge.velocity;
            dustReforge.scale -= 0.01f;
            if (dustReforge.scale < 0.75f)
            {
                dustReforge.active = false;
            }
            return false;
        }
    }
}