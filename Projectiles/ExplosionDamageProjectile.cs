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

namespace ExtraExplosives.Projectiles
{
    public class ExplosionDamageProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ExplosionDamageProjectile");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 20;   //This defines the hitbox width
            projectile.height = 20;    //This defines the hitbox height
            projectile.aiStyle = 23;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 500; //The amount of time the projectile is alive for
            //projectile.Opacity = 0f;
            projectile.damage = 100;
            projectile.knockBack = 1.5f;
        }

        //public override void PostAI()
        //{
        //    projectile.velocity.X = 0;
        //    projectile.velocity.Y = 0;
        //}

        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{

        //    //target.life -= damage;
        //}


        public override void Kill(int timeLeft)
        {
            



            
        }


    }
}