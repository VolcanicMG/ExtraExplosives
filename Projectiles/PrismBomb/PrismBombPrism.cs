//All of these usings are required
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
//using System.Drawing;

namespace ExtraExplosives.Projectiles.PrismBomb //Namespace is set this way as projectiles are stored in a certain folder
{
    public class PrismBombPrism : ModProjectile
    {
        public int laser1, laser2, laser3, laser4 = -1;
        public int soundDelay;
        private bool resetBatchInPost;
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.height = 64;
            Projectile.width = 64;
            laser1 = -1;
            laser2 = -1;
            laser3 = -1;
            laser4 = -1;
            Projectile.timeLeft = 400;
            Projectile.tileCollide = false;

        }
        public override void AI()
        {
            if (soundDelay > 0) { soundDelay -= 1; }
            Projectile.ai[1] += 1;
            if (Projectile.ai[0] <= 80) { Projectile.velocity.Y = -3f; Projectile.velocity.X = 0; Projectile.ai[0] += 1; }
            else if (Projectile.ai[0] > 80) { Projectile.velocity.Y = 0; Projectile.velocity.X = 0; Projectile.rotation += 0.02f; if (soundDelay <= 0) { SoundEngine.PlaySound(SoundID.Item15, Projectile.Center); soundDelay = 20; } }
            if (laser1 == -1 && Projectile.ai[0] > 80)
            {

                laser1 = Projectile.NewProjectile(Projectile.Center, new Vector2(-14, 0), Mod.Find<ModProjectile>("PrismLaser").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
                laser2 = Projectile.NewProjectile(Projectile.Center, new Vector2(14, 0), Mod.Find<ModProjectile>("PrismLaser").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
                laser3 = Projectile.NewProjectile(Projectile.Center, new Vector2(0, 14), Mod.Find<ModProjectile>("PrismLaser").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
                laser4 = Projectile.NewProjectile(Projectile.Center, new Vector2(0, -14), Mod.Find<ModProjectile>("PrismLaser").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);

            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            resetBatchInPost = true;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            GameShaders.Armor.GetShaderFromItemId(ItemID.LivingRainbowDye).Apply(Projectile);

            return true;
        }
        public override void PostDraw(Color lightColor)
        {
            if (resetBatchInPost)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                resetBatchInPost = false;
            }
        }
    }
}