//All of these usings are required
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles //Namespace is set this way as projectiles are stored in a certain folder
{
    [Autoload(false)]
    public class ExtraExplosives_DemoProjectileWithComments : ModProjectile //Class implements Mod Projectile
    {
        // TODO public override bool IsLoadingEnabled(Mod mod) => false;

        private const int PickPower = 0; //power of the bomb
        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile"; //This is the texture. You can have the same name as the class be the png in place of the texture override

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ExtraExplosives_DemoProjectileWithComments"); //Projectile's Name, same as class name
        }

        public override void SetDefaults()
        {
            //Checks to see if the projectile can go through tiles
            Projectile.tileCollide = true;

            //This defines the hitbox width
            Projectile.width = 20;

            //This defines the hitbox height
            Projectile.height = 20;

            //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.aiStyle = 16;

            //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.friendly = true;

            //Tells the game how many enemies it can hit before being destroyed, -1 means unlimited
            Projectile.penetrate = -1;

            //The amount of time in ticks the projectile is alive for
            Projectile.timeLeft = 150;
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            //SoundID.Item14 is the default bomb sound

            //Create Bomb Damage
            ExplosionDamage(5f, Projectile.Center, 70, 20, Projectile.owner);
            //float Radius, Vector2 damagePosition, int Damage, float knockback, int projectileOwner

            //Create Bomb Explosion
            CreateExplosion(Projectile.Center, 2);
            //Vector2 explosionPosition, int Radius

            //Create Bomb Dust
            CreateDust(Projectile.Center, 10);
            //Vector2 dustPosition, int dustAmount
        }

        /// <summary>
        /// This function will create an explosion - All explosion related things happen in here
        /// </summary>
        /// <param name="position"> Stores the center point of the explosion - Try: projectile.Center </param>
        /// <param name="radius"> Stores the radius of the explosion </param>
        private void CreateExplosion(Vector2 position, int radius)
        {
            //These two for-statements create a rectangle
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f); //This converts the X to worldSpace
                    int yPosition = (int)(y + position.Y / 16.0f); //This converts the Y to worldSpace

                    //Uses this if-statement for a circular shape, remove it for a square or rectangular shape
                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition)))
                    {
                        //Make sure to check for unbreakable tiles
                        ushort tile = Main.tile[xPosition, yPosition].TileType;
                        if (!CanBreakTile(tile, PickPower)) //Unbreakable
                        {
                        }
                        else //Breakable
                        {
                            //Code for [if block is breakable] goes here

                            //-----===THIS IS WHERE THE BOMBS UNIQUE CODE GOES===-----\\

                            //The following is code that would likely go in here:
                            //Demo for Tile Breaking - This makes the explosion destroy tiles
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);

                            //Demo for Wall Breaking - Breaks walls if user allows it in config file
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);

                            //Demo for Liquid Breaking
                            //This makes the explosion destroy liquids of any kind
                            Main.tile[xPosition, yPosition].LiquidAmount = LiquidID.Water;
                            //Used to update the liquid
                            WorldGen.SquareTileFrame(xPosition, yPosition, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function will create dust at a set point
        /// </summary>
        /// <param name="position">
        /// Stores the center point of the explosion - Try: projectile.Center </param>
        /// <param name="amount">
        /// Stores max intended amount of dust, this will be overridden by user preferences </param>
        private void CreateDust(Vector2 position, int amount)
        {
            Dust dust; //Stores the current dust being spawned
            Vector2 updatedPosition; //Stores the new center point for dust being spawned

            //This loop spawns dust according to the amount set by its creator
            for (int i = 0; i <= amount; i++)
            {
                //Limits dust to user preferences
                if (Main.rand.NextFloat() < DustAmount)
                {
                    //Use this if-statement repeatedly for as many dusts as you need
                    //Example: one for sparks, one for smoke, one for flames, etc.
                    //The float value determines how much of this dust type is
                    //allowed to spawn, 1 = 100%, 0.5 for 50%, 0 = 0%.
                    if (Main.rand.NextFloat() < 1f) //1f so this 100% spawn chance
                    {
                        //The top-left corner of the dust will automatically spawn at the
                        //updatedPosition. To center it, subtract the dust's width / 2 from
                        //position.X and height/2 from position.Y
                        updatedPosition = new Vector2(position.X - 100 / 2, position.Y - 100 / 2);

                        //This is the current dust being spawned in
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 100, 100,
                            6, 0f, 0.5f, 0, new Color(255, 0, 0), 4.5f)];
                        dust.noGravity = true; //Not affected by gravity
                        dust.fadeIn = 2.5f; //Fades in to screen
                        dust.noLight = true; //Doesn't emit light
                    }

                    //Here is a second dust type, note how it is wrapped in its own if-statement
                    if (Main.rand.NextFloat() < .4f) //.4f so this is 40% spawn chance
                    {
                        //Note the dust has a width of 50 and height of 75 so updatedPosition
                        //is dividing accordingly
                        updatedPosition = new Vector2(position.X - 50 / 2, position.Y - 75 / 2);

                        //This is the current dust being spawned in
                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 50, 75,
                            18, 0f, 0f, 0, new Color(200, 100, 50), 3f)];
                        dust.noGravity = false; //Is affected by gravity
                        dust.fadeIn = 0f; //Doesn't fade in to screen
                        dust.noLight = false; //Does emit light
                    }
                }
            }
        }
    }
}