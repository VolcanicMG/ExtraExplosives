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

    class BunnyiteProjectile : ModProjectile
    {
        internal static bool CanBreakWalls;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunnyite");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 10;   //This defines the hitbox width
            projectile.height = 32;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 80; //The amount of time the projectile is alive for

        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            int bunnies = 200;
            int x = 0;

            for (x = 0; x < bunnies; x++)
            {
                NPC.NewNPC((int)position.X + Main.rand.Next(1000)-500, (int)position.Y, NPCID.Bunny, 0, 0f, 0f, 0f, 0f, 255); //Spawn 
            }
            Main.NewText("You don't have to do this... Resist the urge...", (byte)30, (byte)255, (byte)10, false);

            for (int ii = 0; ii <= 50; ii++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position1 = new Vector2(position.X - 800 / 2, position.Y - 100 / 2);
                dust = Main.dust[Terraria.Dust.NewDust(position1, 800, 100, 112, 0f, 0f, 0, new Color(255, 0, 0), 1.447368f)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(36, Main.LocalPlayer);
                dust.fadeIn = 1.144737f;
            }

        }

    }
}
