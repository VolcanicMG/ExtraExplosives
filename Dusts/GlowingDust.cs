using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Dusts
{
    public class GlowingDust : ModDust
    {
        public override void OnSpawn(Dust glowingDust)
        {
            glowingDust.noGravity = true;
            glowingDust.frame = new Rectangle(0, 10 * Main.rand.Next(3), 10, 10);
            glowingDust.velocity.X = Main.rand.NextFloat(0.001f);
            glowingDust.velocity.Y = Main.rand.NextFloat(0.001f);
        }

        public override bool Update(Dust glowingDust)
        {
            glowingDust.position += glowingDust.velocity;
            glowingDust.scale -= 0.01f;
            Lighting.AddLight(glowingDust.position, new Vector3(57f / 255f, 255f / 255f, 20f / 255f));

            if (glowingDust.scale <= 0.5f)
            {
                glowingDust.active = false;
            }
            return false;
        }
    }
}