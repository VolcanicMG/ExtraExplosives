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
    public class HotPotatoProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/hot-potato_gore";
        private readonly int _lifeTime = 300 + Main.rand.Next(60);    // How long to keep alive in ticks (currently 5-6 seconds)
        private bool _thrown;    // If the projectile has been thrown yet
        private int _fuze = 30;   // The fuze length 
        private float change;
        private float changeTotal;

        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hot Potato");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 0;
            radius = 1;
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

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive() && !_thrown && Main.myPlayer == projectile.owner)
            {
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
                }
                else
                {
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
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive() && Main.myPlayer == projectile.owner)
            {
                changeTotal += change;
                float tmp = (5 - (_lifeTime - projectile.localAI[0]) / 60f - (changeTotal * (projectile.localAI[1] - projectile.localAI[0])));
                if (tmp < 0) tmp = 0;
                Filters.Scene["BurningScreen"].GetShader().UseProgress((tmp > 0) ? tmp : 0);
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
            if (Main.netMode != NetmodeID.Server && !Filters.Scene["BurningScreen"].IsActive() && !_thrown && Main.myPlayer == projectile.owner)        // Shader stuff
            {
                Filters.Scene.Activate("BurningScreen", Main.player[projectile.owner].position).GetShader()
                    .UseColor(255f, 0, 0).UseTargetPosition(Main.player[projectile.owner].position);
            }
        }

        public override void Kill(int ignore)
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["BurningScreen"].IsActive() && Main.myPlayer == projectile.owner)         // Shader stuff
            {
                Filters.Scene["BurningScreen"].Deactivate();
            }
            
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int) projectile.Center.X, (int) projectile.Center.Y);
            
            //Since these values change as the timer ticks down, they need to be set immedietly before an explosion
            // To ensure they are accurate
            projectile.damage = (int)projectile.localAI[0];
            projectile.knockBack = (int) projectile.localAI[0] / 4f;
            radius = (int) projectile.localAI[0] / 12;
            Main.NewText($"Damage: {projectile.damage}, Knockback: {projectile.knockBack}, radius: {radius}");
            Explosion();
            ExplosionDamage();
            CreateDust(projectile.Center, (int) projectile.localAI[0] * 2);
            projectile.timeLeft = 0;
            base.Kill(0);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(2f, 2f);
            Vector2 gVel2 = new Vector2(-2f, -1f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);
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
                    if (Main.rand.NextFloat() < 0.5f)    // dynamite gibs    // Standard
                    {
                        updatedPosition = new Vector2(position.X - radius * 16 / 2, position.Y - radius * 16 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 90)];
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
                        else dust.noGravity = true;
                    }
                    if (Main.rand.NextFloat() < 0.25f)    // potato gibs    // change if a better dust exists
                    {
                        updatedPosition = new Vector2(position.X - radius * 16 / 2, position.Y - radius * 16 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, radius * 16, radius * 16, 216)];//new Color(255, 255, 255)
                        if (Vector2.Distance(dust.position, projectile.Center) > radius * 8) dust.active = false;
                        else dust.noGravity = true;
                    }
                    //------------
                }
            }
        }
    }
}
