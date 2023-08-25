using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
    public class NukeProjectilePhase2 : ModProjectile
    {
        internal static bool CanBreakWalls;
        private bool done = false;
        private bool reset = false;
        private bool firstTick;
        private bool firstTickStart;

        private Vector2 FirstPos;

        //SoundEffectInstance soundPlane;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plane");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.tileCollide = false; //checks to see if the projectile can go through tiles
            Projectile.width = 536;   //This defines the hitbox width
            Projectile.height = 168;    //This defines the hitbox height
            Projectile.aiStyle = 0;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 10000; //The amount of time the projectile is alive for
                                         //projectile.scale = 6f;
            Projectile.netImportant = true;
            Projectile.scale = .9f;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            //Main.NewText(projectile.timeLeft);

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                //projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }

            if (!firstTick)
            {
                //SoundEngine.PlaySound(new SoundStyle("ExtraExplosives/Assets/Sounds/Custom/air-raid"));

                FirstPos = Projectile.position;

                Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

                //soundPlane = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Plane"));

                firstTick = true;
            }


            if (Projectile.timeLeft <= 9700 && done == false)
            {
                ExtraExplosives.NukePos = Projectile.Center;

                if (firstTickStart)
                {
                    ExtraExplosives.NukeActive = true;

                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        ModPacket myPacket2 = Mod.GetPacket();
                        myPacket2.Write((byte)ExtraExplosives.EEMessageTypes.nukeActive);
                        myPacket2.Send();
                    }
                }
                firstTickStart = true;

            }
            else if (Projectile.timeLeft >= 9700 && done == false)
            {
                //int xPosition = (int)(Main.maxTilesX / 16.0f);
                Projectile.position = FirstPos;
            }

            if ((Projectile.position.X / 16) <= ((player.position.X + 2000) / 16) && (Projectile.position.X / 16) >= ((player.position.X - 2000) / 16)) //when in range, change sprite
            {
                //Main.NewText("Set");
                Projectile.frame = 3;
            }

            //if ((projectile.position.X / 16) > (player.position.X / 16) && done == false) //the player is behind the plane
            //{
            //	Main.NewText("Thought you could get away?\n" +
            //		"Dropping the Load!!");
            //	done = true;

            //	SpawnProjectileSynced(projectile.position, new Vector2(0, 0), ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);

            //	//Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);
            //}

            if ((Projectile.position.X <= player.position.X + 40 && Projectile.position.X >= player.position.X - 40) && done == false) //searching for the player
            {
                //Main.NewText("Drop the load");
                Projectile.frame = 4;
                done = true;

                SpawnProjectileSynced(Projectile.position, new Vector2(0, 0), ModContent.ProjectileType<NukeProjectile>(), 0, 0, Projectile.owner);

                //Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);
            }

            //reset the plane
            if (done == true && ExtraExplosives.NukeActive == false && reset == false)
            {
                reset = true;

                Projectile.timeLeft = 10000;
            }

            //The nuke has hit reset the effect of the shake
            if (Projectile.timeLeft < 9900 && ExtraExplosives.NukeHit == true)
            {
                Projectile.timeLeft = 100;
                ExtraExplosives.NukeHit = false;

                if (Main.netMode == NetmodeID.MultiplayerClient) //set NukeHit to false for all players
                {
                    ModPacket myPacket = Mod.GetPacket();
                    myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkNukeHit);
                    myPacket.Send();
                }
            }
        }

        public override void PostAI()
        {
        }

        public override void Kill(int timeLeft)
        {
        }
    }
}