using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.Dusts
{
    public class DebrisDust : ModDust
    {
        private int lifeTime;
        Vector2 oldVelocity;

        public override void OnSpawn(Dust DebrisDust)
        {
            DebrisDust.frame = new Rectangle(0, 10 * Main.rand.Next(3), 10, 10);

            lifeTime = 100 + Main.rand.Next(60);

            oldVelocity = DebrisDust.velocity;

            while (lifeTime % 5 != 0)
            {
                lifeTime++;
            }
        }

        public override bool Update(Dust DebrisDust)
        {

            lifeTime--;

            //Every 3rd tick change the frame of the dust
            if (lifeTime % 4 == 0)
            {
                DebrisDust.frame = new Rectangle(0, 10 * Main.rand.Next(3), 10, 10);
            }


            //Shrink the dust slowly
            if (lifeTime % 5 == 0)
            {
                DebrisDust.scale -= DebrisDust.scale * 0.1f;

            }

            //lighting
            Lighting.AddLight(DebrisDust.position, new Vector3(89f/255f, 35f/255f, 13f/255f));
            Lighting.maxX = 3;
            Lighting.maxY = 3;

            //Once it touches the ground stop moving
            if (!WorldGen.TileEmpty((int)(DebrisDust.position.X / 16f), (int)(DebrisDust.position.Y / 16f)))
            {
                DebrisDust.velocity = Vector2.Zero;
            }

            if (DebrisDust.scale <= 0)
            {
                DebrisDust.active = false;
            }

            DebrisDust.rotation += DebrisDust.velocity.X * 0.2f;

            return true;
        }
    }
}