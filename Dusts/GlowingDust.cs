using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Dusts
{
    public class GlowingDust : ModDust
    {

        private int lifeTime = 3600 + Main.rand.Next(600);

        public override void OnSpawn(Dust glowingDust)
        {
            glowingDust.noGravity = true;
            glowingDust.frame = new Rectangle(0, 10 * Main.rand.Next(3), 10, 10);
            glowingDust.velocity.X = Main.rand.NextFloat(0.001f);
            glowingDust.velocity.Y = Main.rand.NextFloat(0.001f);
        }

        public override bool Update(Dust glowingDust)
        {
            Lighting.AddLight(glowingDust.position, new Vector3(57f / 255f, 255f / 255f, 20f / 255f));//new Vector3(57, 255, 20)

            lifeTime--;
            if(WorldGen.TileEmpty((int)(glowingDust.position.X/16f), (int)(glowingDust.position.Y/16f)))
            {
                glowingDust.position += glowingDust.velocity;
                if (glowingDust.velocity.Y < 0.01f)glowingDust.velocity.Y *= 1.005f;
                if (glowingDust.velocity.X > 0.01f)glowingDust.velocity.X = 1.005f;
            }
            else
            {
                glowingDust.velocity = Vector2.Zero;
            }
            return false;
        }
    }
}