using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles.PrismBomb
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class PrismLaser : ModProjectile
    {

        //The distance charge particle from the npc center
        private const float START_DISTANCE = 30f;
        // MAx possible laser 
        private const float MAX_LENGTH = 2200f;
        // rotation
        private const float ROTATION_SPEED = 0.02f;

        // The actual distance is stored in the ai1 field
        // By making a property to handle this it makes our life easier, and the accessibility more readable
        public float laserLength
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        // Are we at max charge? With c#6 you can simply use => which indicates this is a get only property


        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.aiStyle = -1;


        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Main.instance.DrawCacheProjsBehindProjectiles.Add(index);

        }

        public override bool PreDraw(ref Color lightColor)
        {

            Projectile.Center = Main.projectile[(int)Projectile.ai[0]].Center;

            SetLaser();
            /*spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);*/
            GameShaders.Armor.GetShaderFromItemId(ItemID.LivingRainbowDye).Apply(Projectile);

            /*DrawLaser(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, Projectile.Center,
                   Projectile.velocity, 10f, Projectile.damage, -1.57f, 1f, laserLength, Color.White, (int)START_DISTANCE);*/

            return false;


        }

        // The core function of drawing a laser
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;

            // Draws the laser 'body' -- laserLength / maxDist
            for (float i = transDist; i <= maxDist; i += step)
            {
                Color c = Color.White;
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                  new Rectangle(0, 26, 44, 28), i < transDist ? Color.Transparent : c, r,
                    new Vector2(44 * .5f, 28 * .5f), scale, 0, 0);

            }

            // Draws the laser 'tail'
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 44, 22), Color.White, r, new Vector2(44 * .5f, 22 * .5f), scale, 0, 0);

            // Draws the laser 'head'
            spriteBatch.Draw(texture, start + (maxDist + step) * unit - Main.screenPosition,
                new Rectangle(0, 56, 44, 22), Color.White, r, new Vector2(44 * .5f, 22 * .5f), scale, 0, 0);
        }

        // Change the way of collision check of the projectile
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            // We can only collide if we are at max charge, which is when the laser is actually fire           
            Vector2 unit = Projectile.velocity;
            float point = 0f;
            //Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
            //It will look for collisions on the given line using AABB
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
             Projectile.Center + unit * laserLength, 44, ref point);

        }

        // Set custom immunity time on hitting an NPC
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 5;
            if(DamageReducedNps.Contains(target.type))
            {
               damage = damage / 2;
            }
        }


        // The AI of the projectile
        public override void AI()
        {
            Projectile.velocity = Rotate(Projectile.velocity, ROTATION_SPEED);
            CheckKill();
            SpawnDusts();
            CastLights();
        }

        private void SpawnDusts()
        {
            Vector2 unit = Projectile.velocity * -1;
            Vector2 dustPos = Projectile.Center + Projectile.velocity * laserLength;

            for (int i = 0; i < 2; ++i)// end dust
            {
                float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 181, dustVel.X, dustVel.Y)];
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 31,
                    -unit.X * laserLength, -unit.Y * laserLength);
                dust.fadeIn = 0f;
                dust.noGravity = true;
                dust.scale = 0.88f;
                dust.color = Color.Cyan;
            }

            if (Main.rand.NextBool(5))
            {
                Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width; // start dust
                Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 181, 0.0f, 0.0f, 100, new Color(), 1.5f)];
                dust.velocity *= 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);
                unit = dustPos - Projectile.Center;
                unit.Normalize();
                dust = Main.dust[Dust.NewDust(Projectile.Center + 55 * unit, 8, 8, 181, 0.0f, 0.0f, 100, new Color(), 1.5f)];
                dust.velocity = dust.velocity * 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);
            }
        }

        public override void Kill(int time)
        {

        }
        /*
         * Sets the end of the laser position based on where it collides with something, and set velocity 
         */
        private void SetLaser()
        {
            Vector2 diff = Projectile.velocity;
            diff.Normalize();
            Projectile.velocity = diff;
            Vector2 rotatedVelocity = Rotate(diff, Projectile.ai[1]);
            for (laserLength = START_DISTANCE; laserLength <= MAX_LENGTH; laserLength += 5f)
            {
                Vector2 start = Projectile.Center + Projectile.velocity * laserLength;
                if (!Collision.CanHit(Projectile.Center, 1, 1, start, 1, 1))
                {
                    laserLength -= 5f;
                    break;
                }
            }
        }

        private void CheckKill()
        {
            // Kill the projectile if the npc isnt active or pushes in ai[0] of -1 
            if (Projectile.ai[0] == -1 || Main.projectile[(int)Projectile.ai[0]].active == false)
            {
                Projectile.active = false;
            }
        }



        private static Vector2 Rotate(Vector2 v, float radians)
        {
            double ca = Math.Cos(radians);
            double sa = Math.Sin(radians);
            return new Vector2((float)(ca * v.X - sa * v.Y), (float)(sa * v.X + ca * v.Y));
        }

        private void CastLights()
        {
            // Cast a light along the line of the laser
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (laserLength - START_DISTANCE), 26, DelegateMethods.CastLight);
        }

        public override bool ShouldUpdatePosition() => false;

        /*
         * Update CutTiles so the laser will cut tiles (like grass)
         */
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = Projectile.velocity;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * laserLength, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
        }
    }
}