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
    class CritterBombProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CritterBomb");
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
            projectile.timeLeft = 100; //The amount of time the projectile is alive for
            projectile.damage = 0;
 
        }



        public override void Kill(int timeLeft)
        {
            //Player player = Main.player[Main.myPlayer];
            Vector2 position = projectile.Center;

            int spread = 0;
            int[] variety = {442, 443, 445, 446, 447, 448, 539, 444}; //442:GoldenBird - 443:GoldenBunny - 445:GoldenFrog - 446:GoldenGrasshopper - 447:GoldenMouse - 539:GoldenSquirrel - 448:GoldenWorm - 444:GoldenButterfly
            

            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            for(int i = 0; i <= 10; i++)
            {
                spread = Main.rand.Next(1200); //Random spread

                int pick = 0; 
                pick = variety[Main.rand.Next(variety.Length)];

                NPC.NewNPC((int)position.X + (spread - 600), (int)position.Y, pick, 0, 0f, 0f, 0f, 0f, 255); //Spawn 
                spread = 0;

            }

            for (int ii = 0; ii <= 50; ii++) //dust
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position1 = new Vector2(position.X - 600 / 2, position.Y - 100 / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position1, 600, 100, 1, 0f, 0f, 0, new Color(159, 255, 0), 1.776316f)];
                    dust.noLight = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(112, Main.LocalPlayer);
                    dust.fadeIn = 1.697368f;
                }
            }

        }

    }
}
