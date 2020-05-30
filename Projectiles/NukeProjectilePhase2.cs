using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class NukeProjectilePhase2 : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;
        bool done = false;
        bool reset = false;
        bool firstTick;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plane");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false; //checks to see if the projectile can go through tiles
            projectile.width = 416;   //This defines the hitbox width
            projectile.height = 152;    //This defines the hitbox height
            projectile.aiStyle = 0;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 10000; //The amount of time the projectile is alive for
            projectile.netImportant = true;
            //projectile.scale = 1.5f;
        }


        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            //Main.NewText(projectile.timeLeft);

            if (!firstTick)
            {
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/air-raid"));
                firstTick = true;
            }

            if (projectile.timeLeft < 9700 && done == false)
            {
                //send the projectiles postion to the player's camera and set NukeActive to true

                ExtraExplosives.NukePos = projectile.Center;
                ExtraExplosives.NukeActive = true; //since the projectile is active set it active in the player class

                //if (Main.netMode == NetmodeID.MultiplayerClient)
                //{
                //    ModPacket myPacket = mod.GetPacket(); //clean up later
                //    myPacket.Write("boom");
                //    myPacket.Send();

                //    ModPacket myPacket2 = mod.GetPacket();
                //    myPacket2.WriteVector2(projectile.Center);
                //    myPacket2.Send();
                //}
            }
            else if (projectile.timeLeft > 9700)
            {
                projectile.position = new Vector2(Main.maxTilesX, 1000);

            }

            if ((projectile.position.X <= player.position.X + 40 && projectile.position.X >= player.position.X - 40) && done == false)
            {
                //Main.NewText("Drop the load");

                done = true;
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);
                //Main.PlaySound(SoundLoader.customSoundType, (int)player.position.X, (int)player.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/wizz"));

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
            }

            Dust dust;
            Vector2 pos = new Vector2(projectile.position.X + projectile.height - 300 / 2, projectile.position.Y + 80);
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            dust = Terraria.Dust.NewDustPerfect(pos, 35, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 3.289474f);
            dust.noGravity = true;


        }


        public override void Kill(int timeLeft)
        {


        }


    }
}