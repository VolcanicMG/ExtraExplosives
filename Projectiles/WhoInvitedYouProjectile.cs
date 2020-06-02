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
using static ExtraExplosives.GlobalMethods;
using IL.Terraria.Chat;

namespace ExtraExplosives.Projectiles
{
    public class WhoInvitedYouProjectile : ModProjectile
    {
        //Variables:
        private String[] PeanutGallery = {"","","","","","", "Ahhh! Who are you!?", "Why are you here?", "Oh well, I guess this means...", "I have company!", "What's your favorite ice cream?", "Grenade Grape or Explosive Mint perhaps?",
            "I'm not a picky eater...", "I'll eat anything!", "Hey! is that a... Goblin...", "Pfft, I shouldn't, but yet...", "Ah whatever, cool place you got here.", "Are these blocks imported?", "Super fancy...",
            "You don't talk much do you?", "It's alright, I get shy too.", "I feel like I say so little.", "I hate when other people do all the talking.", "Please allow me to contribute to this conversation.",
            "Did know there's a name for an old snowman?", "Yeah! It's true!",  "Someone actually came up with one...", "Its called water! :P", "Syke!", "Say, you mind if I bunk with you?",
            "The last guy I met tried to shoot me.", "No manners that man...", "Besides, I'm sure you'd appreciate the company.", "I'm fairly quiet and make a mad frozen bagel.", "I know its forward of me...",
            "But I tell ya, the zombies scare me man!", "It's like they have no manners at all.", "Not even a chuckle?", "Your really good at that quiet bit.", "Fine, you wanna dance, lets dance!",
            "Bet I can be quiet longer than you!", "Ready, Go!","","","","","","","","","","","AHH!! I can't take it anymore.", "Silence is my worst nightmare.", "Welp, I'm bored with this.", "Have safe travels my friend!", "I must return...",
            "To that glorious gallery of peanuts in the sky!", "My people need me!"};
        private int PeanutCntr = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WhoInvitedYouProjectile");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 12000000;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity.X = 0;
            projectile.rotation = 0f;
            projectile.velocity.Y = -8;

            if (!PeanutGallery[PeanutCntr].Equals("null"))
                Main.NewText(PeanutGallery[PeanutCntr], (byte)35, (byte)255, (byte)100, false);
            PeanutCntr++;

            return false;
        }

        public override void PostAI()
        {
            projectile.velocity.X = 0;
            projectile.rotation = 0f;

            if (PeanutCntr == PeanutGallery.Length)
                projectile.Kill();

            base.PostAI();
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            Main.PlaySound(SoundID.Item14, (int)projectile.Center.X, (int)projectile.Center.Y);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 2);

            //Create Bomb Dust
            CreateDust(projectile.Center, 100);
        }

        private void CreateExplosion(Vector2 position, int radius)
        {
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left 
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5) //Circle
                    {
                        if (CheckForUnbreakableTiles(Main.tile[xPosition, yPosition].type)) //Unbreakable
                        {

                        }
                        else //Breakable
                        {

                        }
                    }
                }
            }
        }

        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust;
            Vector2 updatedPosition;

            for (int i = 0; i <= amount; i++)
            {
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //---Dust 1---
                    if (Main.rand.NextFloat() < 1f)
                    {
                        updatedPosition = new Vector2(position.X - 70 / 2, position.Y - 70 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 70, 70, 4, 0f, 0f, 154, new Color(255, 255, 255), 3.55f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.2763158f;
                    }
                    //------------
                }
            }
        }
    }
}

