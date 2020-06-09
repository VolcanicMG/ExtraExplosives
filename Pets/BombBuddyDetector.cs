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
using ExtraExplosives.UI;
using ExtraExplosives.Projectiles;
using ExtraExplosives.Buffs;

namespace ExtraExplosives.Pets
{
    public class BombBuddyDetector : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Buddy Follow");
            
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 30;   //This defines the hitbox width
            projectile.height = 40;    //This defines the hitbox height
            projectile.aiStyle = 0;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 50000; //The amount of time the projectile is alive for
            projectile.width = 38;
            projectile.height = 40;
            projectile.Opacity = 0f;
        }

        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile";

        //public override bool PreAI()
        //{
        //    Player player = Main.player[projectile.owner];
        //    //player.BabyFaceMonster = false; // Relic from aiType
        //    return true;
        //}

        public override void AI()
        {
            projectile.timeLeft = 5;
            Player player = Main.player[projectile.owner];
            ExtraExplosivesPlayer modPlayer = player.GetModPlayer<ExtraExplosivesPlayer>();
            projectile.position = modPlayer.BuddyPos;
            //Main.NewText(projectile.position);

            if (!player.HasBuff(ModContent.BuffType<BombBuddyBuff>()))
            {
                projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 5;     //this is the explosion radius, the highter is the value the bigger is the explosion

            ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 100, 40, projectile.owner, 0.0f, 0);

            for (int i = 0; i <= 10; i++)
            {
                Dust dust;
                Vector2 vev = new Vector2(position.X - (78 / 2), position.Y - (78 / 2));
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                    {


                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        dust = Main.dust[Terraria.Dust.NewDust(vev, 78, 78, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 4.539474f)];
                        dust.noGravity = true;
                        dust.fadeIn = 2.486842f;
                    }

                    if (Main.rand.NextFloat() < 0.5921053f)
                    {

                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        dust = Main.dust[Terraria.Dust.NewDust(vev, 78, 78, 203, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }

                    if (Main.rand.NextFloat() < 0.2763158f)
                    {
                        Vector2 vev2 = new Vector2(position.X - (100 / 2), position.Y - (100 / 2));
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        dust = Main.dust[Terraria.Dust.NewDust(vev2, 100, 100, 31, 0f, 0f, 0, new Color(255, 255, 255), 5f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                }



            }
        }


    }

}