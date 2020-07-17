//All of these usings are required
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using System.Drawing.Imaging;
//using System.Drawing;
using ExtraExplosives;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles.PrismBomb //Namespace is set this way as projectiles are stored in a certain folder
{
    public class PrismBombPrism : ModProjectile
    {
        public int laser1, laser2, laser3, laser4 = -1;
        public int soundDelay;
        private bool resetBatchInPost;
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.height = 64;
            projectile.width = 64;
            laser1 = -1;
            laser2 = -1;
            laser3 = -1;
            laser4 = -1;
            projectile.timeLeft = 400;
            projectile.tileCollide = false;
            
        }
        public override void AI()
        {
            if (soundDelay > 0) { soundDelay -= 1; }
            projectile.ai[1] += 1;
            if (projectile.ai[0] <= 80) { projectile.velocity.Y = -3f;projectile.velocity.X = 0;projectile.ai[0] += 1; }
            else if (projectile.ai[0] > 80) { projectile.velocity.Y = 0; projectile.velocity.X = 0; projectile.rotation += 0.02f;if (soundDelay <= 0) { Main.PlaySound(SoundID.Item15, projectile.Center); soundDelay = 20; } }
            if( laser1 == -1 && projectile.ai[0] > 80)
            { 
            
                laser1 = Projectile.NewProjectile(projectile.Center,new Vector2(-14,0),mod.ProjectileType("PrismLaser"),projectile.damage,0f,projectile.owner,projectile.whoAmI);
                laser2 = Projectile.NewProjectile(projectile.Center, new Vector2(14, 0), mod.ProjectileType("PrismLaser"), projectile.damage, 0f, projectile.owner, projectile.whoAmI);
                laser3 = Projectile.NewProjectile(projectile.Center, new Vector2(0, 14), mod.ProjectileType("PrismLaser"), projectile.damage, 0f, projectile.owner, projectile.whoAmI);
                laser4 = Projectile.NewProjectile(projectile.Center, new Vector2(0, -14), mod.ProjectileType("PrismLaser"), projectile.damage, 0f, projectile.owner, projectile.whoAmI);
               
            }
            
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            resetBatchInPost = true;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            GameShaders.Armor.GetShaderFromItemId(ItemID.LivingRainbowDye).Apply(projectile);

            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
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