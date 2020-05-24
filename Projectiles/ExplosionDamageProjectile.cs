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
using ExtraExplosives.Projectiles;

namespace ExtraExplosives.Projectiles
{
    public class ExplosionDamageProjectile : ModProjectile
    {
        internal static float DamageRadius;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ExplosionDamage");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 20;   //This defines the hitbox width
            projectile.height = 20;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 5; //The amount of time the projectile is alive for
            projectile.Opacity = 0f;
            projectile.scale = DamageRadius; //DamageRadius
            //projectile.scale = 5;
        }

        //public override void PostAI()
        //{
        //    projectile.velocity.X = 0;
        //    projectile.velocity.Y = 0;
        //}


        public override void Kill(int timeLeft)
        {
            



            
        }


    }
}