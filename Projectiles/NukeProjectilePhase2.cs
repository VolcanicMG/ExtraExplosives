using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Projectiles
{
	public class NukeProjectilePhase2 : ModProjectile
	{
		internal static bool CanBreakWalls;
		private bool done = false;
		private bool reset = false;
		private bool firstTick;

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
			projectile.height = 168;	//This defines the hitbox height
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

				//soundPlane = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Plane"));

				firstTick = true;
			}

			if (projectile.timeLeft <= 9700 && done == false)
			{
				//send the projectiles postion to the player's camera and set NukeActive to true

				//projectile.velocity.X = 30;
				ExtraExplosives.NukePos = projectile.Center;
				ExtraExplosives.NukeActive = true; //since the projectile is active set it active in the player class

				//if (Main.netMode == NetmodeID.MultiplayerClient)
				//{
				//	ModPacket myPacket = mod.GetPacket(); //clean up later
				//	myPacket.Write("boom");
				//	myPacket.Send();

				//	ModPacket myPacket2 = mod.GetPacket();
				//	myPacket2.WriteVector2(projectile.Center);
				//	myPacket2.Send();
				//}
			}
			else if (projectile.timeLeft >= 9700 && done == false)
			{
				int xPosition = (int)(Main.maxTilesX / 16.0f);
				projectile.position = new Vector2(xPosition, 1500);
			}

			if ((projectile.position.X / 16) <= ((player.position.X + 2000) / 16) && (projectile.position.X / 16) >= ((player.position.X - 2000) / 16)) //when in range, change sprite
			{
				//Main.NewText("Set");
				projectile.frame = 3;
			}

			if ((projectile.position.X / 16) > (player.position.X / 16) && done == false) //the player is behind the plane
			{
				Main.NewText("Thought you could get away?\n" +
					"Droping Load!!");
				done = true;

				SpawnProjectileSynced(projectile.position, new Vector2(0, 0), ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);

				//Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ModContent.ProjectileType<NukeProjectile>(), 0, 0, projectile.owner);
			}

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
					myPacket.WriteVarInt(2);
					myPacket.Send();
				}
			}

			Dust dust;
			Dust dust2;
			Vector2 pos = new Vector2(projectile.position.X + projectile.width / 2 - 110, projectile.position.Y + projectile.height / 2 + 25);
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			dust = Terraria.Dust.NewDustPerfect(pos, 35, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 3.289474f);
			if (Main.rand.NextFloat() < 0.6578947f)
			{
				dust2 = Terraria.Dust.NewDustPerfect(pos, 31, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 4.605263f);
				dust2.noGravity = true;
			}
			dust.noGravity = true;
		}

		public override void PostAI()
		{
		}

		public override void Kill(int timeLeft)
		{
		}
	}
}