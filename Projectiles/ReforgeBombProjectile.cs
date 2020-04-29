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
using ExtraExplosives;
using ExtraExplosives.UI;
using ExtraExplosives.Dusts;

namespace ExtraExplosives.Projectiles
{
    public class ReforgeBombProjectile : ModProjectile
    {
        //Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ReforgeBomb");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 22;   //This defines the hitbox width
            projectile.height = 22;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 100; //The amount of time the projectile is alive for
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;

            ExtraExplosivesReforgeBombUI.reforge = true;

            if(ExtraExplosivesReforgeBombUI.IsVisible == false)
            {
                Main.NewText("Nothing to reforge, press 'P' to toggle reforge UI");
                ExtraExplosivesReforgeBombUI.reforge = false;
            }
            //Item.NewItem(position, new Vector2(20, 20), ItemID.GoldAxe, 1, false, -2);

            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            for (int i = 0; i < 100; i++) //spawn dust
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        Vector2 position1 = new Vector2(position.X - 26 / 2, position.Y - 24 /2);
                        Dust.NewDust(position1, 26, 24, ModContent.DustType<ReforgeBombDust>());

                        Dust dust;
                        Vector2 position2 = new Vector2(position.X - 105 / 2, position.Y - 105 / 2);
                        dust = Main.dust[Terraria.Dust.NewDust(position2, 105, 105, 1, 0f, 0f, 0, new Color(255, 255, 255), 1.4f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1f;

                        Dust dust2;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position3 = new Vector2(position.X - 131 / 2, position.Y - 131 / 2);
                        dust2 = Main.dust[Terraria.Dust.NewDust(position3, 131, 131, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
                        dust2.noGravity = true;
                        dust.position += dust.velocity;


                    }
                }
            }

        }
    }
}