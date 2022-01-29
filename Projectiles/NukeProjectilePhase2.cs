using Microsoft.Xna.Framework;
using Terraria;
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
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false; //checks to see if the projectile can go through tiles
            projectile.width = 536;   //This defines the hitbox width
            projectile.height = 168;    //This defines the hitbox height
            projectile.aiStyle = 0;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 10000; //The amount of time the projectile is alive for
                                         //projectile.scale = 6f;
            projectile.netImportant = true;
            projectile.scale = .9f;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            //Main.NewText(projectile.timeLeft);

            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                //projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                if (++projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
            }

            if (!firstTick)
            {
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/air-raid"));

                FirstPos = projectile.position;

                projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;

                //soundPlane = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Plane"));

                firstTick = true;
            }


            if (projectile.timeLeft <= 9700 && done == false)
            {
                ExtraExplosives.NukePos = projectile.Center;

                if (firstTickStart)
                {
                    ExtraExplosives.NukeActive = true;

                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        ModPacket myPacket2 = mod.GetPacket();
                        myPacket2.Write((byte)ExtraExplosives.EEMessageTypes.nukeActive);
                        myPacket2.Send();
                    }
                }
                firstTickStart = true;

            }
            else if (projectile.timeLeft >= 9700 && done == false)
            {
                //int xPosition = (int)(Main.maxTilesX / 16.0f);
                projectile.position = FirstPos;
            }

            if ((projectile.position.X / 16) <= ((player.position.X + 2000) / 16) && (projectile.position.X / 16) >= ((player.position.X - 2000) / 16)) //when in range, change sprite
            {
                //Main.NewText("Set");
                projectile.frame = 3;
            }

            //if ((projectile.position.X / 16) > (player.position.X / 16) && done == false) //the player is behind the plane
            //{
            //	Main.NewText("Thought you could get away?\n" +
            //		"Dropping the Load!!");
            //	done = true;

            //	SpawnProjectileSynced(projectile.position, new Vector2(0, 0), ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);

            //	//Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);
            //}

            if ((projectile.position.X <= player.position.X + 40 && projectile.position.X >= player.position.X - 40) && done == false) //searching for the player
            {
                //Main.NewText("Drop the load");
                projectile.frame = 4;
                done = true;

                SpawnProjectileSynced(projectile.position, new Vector2(0, 0), ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);

                //Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);
            }

            //reset the plane
            if (done == true && ExtraExplosives.NukeActive == false && reset == false)
            {
                reset = true;

                projectile.timeLeft = 10000;
            }

            //The nuke has hit reset the effect of the shake
            if (projectile.timeLeft < 9900 && ExtraExplosives.NukeHit == true)
            {
                projectile.timeLeft = 100;
                ExtraExplosives.NukeHit = false;

                if (Main.netMode == NetmodeID.MultiplayerClient) //set NukeHit to false for all players
                {
                    ModPacket myPacket = mod.GetPacket();
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