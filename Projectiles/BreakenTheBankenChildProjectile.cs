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
    public class BreakenTheBankenChildProjectile : ModProjectile
    {
        internal static bool CanBreakWalls;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BreakenTheBanken");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GoldCoin);
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 5;   //This defines the hitbox width
            projectile.height = 5;    //This defines the hitbox height
            projectile.aiStyle = 1; //16  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = 20; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 200; //The amount of time the projectile is alive for
        }

        public override string Texture => "Terraria/Projectile_" + ProjectileID.GoldCoin;

        public override void Kill(int timeLeft)
        {

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 8;     //this is the explosion radius, the highter is the value the bigger is the explosion


            Item.NewItem(position, new Vector2(10, 10), ItemID.GoldCoin);

        }


    }
}