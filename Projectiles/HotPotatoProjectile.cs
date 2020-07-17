using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class HotPotatoProjectile : ModProjectile
    {
        private int _damage = 100;
        private int _pickPower = 0;
        private int _lifeTime = 300 + Main.rand.Next(60);    // How long to keep alive in ticks (currently 5-6 seconds)
        private bool _thrown;
        private float changeTotal;
        private float change;

        private int _fuze = 30;
        //private float[] rgb = new[] {1.58f, 1.58f, 0.0f};

        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hot Potato");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = _lifeTime + 5; // set higher than _lifeTime
            projectile.alpha = 0;    // Make it invisible
            projectile.alpha = 255;    // Make it invisible
            projectile.velocity = Vector2.Zero;
            Lighting.AddLight(projectile.Center, 0.5f, 0.5f, 0.3f);
        }

        public override string Texture => "ExtraExplosives/Projectiles/HotPotatoProjectile";

        /*public override bool PreAI()
        {
            projectile.ai[0]++;
            if ((Mouse.GetState().LeftButton == ButtonState.Released && !_thrown) || projectile.ai[0] >= _lifeTime - 15)    // Add support for controllers
            {
                projectile.alpha = 0;
                projectile.ai[1] = projectile.ai[0];
                _thrown = true;
            }
            else if(!_thrown)
            {
                projectile.position = Main.player[projectile.owner].position;
            }
            return _thrown;
        }

        public override void AI()
        {
            projectile.ai[1]++;
            if (projectile.ai[0] >= _lifeTime - 15 && projectile.ai[1] >= _lifeTime)
            {
                Kill(_lifeTime - (int)projectile.ai[0]);
            }
            else if (projectile.ai[1] >= _lifeTime)
            {
                Kill(_lifeTime - (int)projectile.ai[0]);
            }
            else if(projectile.ai[1] < 2f)
            {
                projectile.velocity.X += 100;
            }
            projectile.velocity.Y += 0.2f;
            projectile.velocity.X *= 0.99f;
        }*/


        //public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
       // {
       //     spriteBatch.End();
       //     spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
       //     return true;
       // }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive() && !_thrown)
            {

                //mod.Logger.Debug("_lifeTime - projectile.localAI[0]/60f(start at 0 raise till release)" + ((_lifeTime/60f) - (_lifeTime - projectile.localAI[0])/60f));

                //mod.Logger.Debug("_lifeTime - projectile.localAI[0]/60f(start at 0 raise till release)" + ((_lifeTime/60f) - (_lifeTime - projectile.localAI[0])/60f));
                //float progress = _lifeTime - (_lifeTime - projectile.timeLeft) / _lifeTime; // Will range from -3 to 3, 0 being the point where the bomb explodes.
                float progress = (projectile.timeLeft) / 60f; 
                Filters.Scene["BurningScreen"].GetShader().UseProgress((_lifeTime/60f) - (_lifeTime - projectile.localAI[0])/60f);
            }
            
            if (_thrown) return true;
            
            projectile.localAI[0]++;
            if (player.releaseUseItem || projectile.localAI[0] >= _lifeTime - _fuze)
            {
                _thrown = true;
                change = ((_lifeTime - projectile.localAI[0])/60f)/(_lifeTime - projectile.localAI[0]);
                changeTotal = 0;
                projectile.alpha = 0;
                projectile.localAI[1] = projectile.localAI[0];
                if (projectile.localAI[0] < _lifeTime - _fuze)
                {
                    float modifier = 0.075f;
                    float screenW = Main.screenWidth;
                    float mouseX = Main.MouseScreen.X - (screenW/2);
                    float screenH = Main.screenHeight;
                    float mouseY = Main.MouseScreen.Y - (screenH/2);
                    projectile.velocity.X = projectile.localAI[0] /_lifeTime * mouseX * modifier;
                    projectile.velocity.Y = projectile.localAI[0] /_lifeTime * mouseY * modifier;
                    //mod.Logger.DebugFormat("Potato thrown at {0} {1}, mouse {2} {3}, screen {4} {5}", projectile.velocity.X, projectile.velocity.Y,Main.MouseScreen.X,Main.MouseScreen.Y,screenW,screenH);
                }
                else
                {
                    projectile.velocity.Y = 1;
                    projectile.velocity.Y = 3;
                }
            }
            else
            {
                projectile.position = player.position;
                int type = Main.rand.Next(2) + 270;
                if(Main.rand.Next(100) < 10){Dust dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 70, 70, type, 0f, 0f, 154, new Color(255, 255, 255), 1.55f)];}
                
            }
            return _thrown;
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive())
            {
                changeTotal += change;
                float tmp = (5 - (_lifeTime - projectile.localAI[0]) / 60f - (changeTotal * (projectile.localAI[1] - projectile.localAI[0])));
                if (tmp < 0) tmp = 0;
                Filters.Scene["BurningScreen"].GetShader().UseProgress((tmp > 0) ? tmp : 0);
               // mod.Logger.DebugFormat("Overall progress made is {0} and decrease every tick is {1}",(_lifeTime - projectile.localAI[0]), tmp);
            }
            projectile.localAI[1]++;
            if (projectile.localAI[1] >= _lifeTime)
            {
                Kill(projectile.timeLeft);    // since this value will vary, just use localAI[0]
            }
            projectile.velocity.X *= 0.995f;
            if (projectile.velocity.Y < 20f) projectile.velocity.Y += 0.08f;
            if (projectile.velocity.Y < 20f) projectile.velocity.Y += 0.1f;
        }

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.Server && !Filters.Scene["BurningScreen"].IsActive() && !_thrown)        // Shader stuff
            {
                //Filters.Scene.Activate("BurningScreen", Main.player[projectile.owner].Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
                Filters.Scene.Activate("BurningScreen", Main.player[projectile.owner].position).GetShader()
                    .UseColor(255f, 0, 0).UseTargetPosition(Main.player[projectile.owner].position);
            }
        }
        

        private void CreateExplosion(Vector2 position, int radius)    // Ripped from troll bomb, changed where needed
        {                                                                // comments removed
            for (int x = -radius; x <= radius; x++) 
            {
                for (int y = -radius; y <= radius; y++) 
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) 
                    {
                        ushort tile = Main.tile[xPosition, yPosition].type;
                        if (!CanBreakTile(tile, _pickPower)) 
                        {
                        }
                        else //Breakable
                        {
                            if (CanBreakTiles) //User preferences dictates if this bomb can break tiles
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
                            }
                        }
                    }
                }
            }
        }
        
        public override void Kill(int ignore)
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive())        // Shader stuff
            {
                Filters.Scene["BurningScreen"].Deactivate();
            }
            
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int) projectile.Center.X, (int) projectile.Center.Y);

            //Create Bomb Damage
            ExplosionDamage(projectile.localAI[0] / 6, projectile.Center, (int)projectile.localAI[0], projectile.localAI[0]/4, projectile.owner);

            //Create Bomb Explosion
            CreateExplosion(projectile.Center, (int) projectile.localAI[0]/12);

            //Create Bomb Dust
            CreateDust(projectile.Center, (int) projectile.localAI[0]/50);
        }
        
        private void CreateDust(Vector2 position, int amount)    // TODO UPDATE DUST CODE THIS BIT ACTS STRANGE
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 0.1)    // dynamite gibs    // Standard
                    {
                        updatedPosition = new Vector2(position.X - 70 / 2, position.Y - 70 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 70, 70, 4, 0f, 0f, 154, new Color(255, 255, 255), 1.55f)];
                        dust.noGravity = false;
                        dust.fadeIn = 0.2763158f;
                    }
                    if (Main.rand.NextFloat() < 0.1)    // potato gibs    // change if a better dust exists
                    {
                        updatedPosition = new Vector2(position.X - 70 / 2, position.Y - 70 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 70, 70, 216, 0f, 0f, 154, new Color(255, 255, 255), 1.55f)];
                        dust.noGravity = false;
                        dust.fadeIn = 0.2763158f;
                    }
                    //------------
                }
            }
        }
        
    }
}
