using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Items.Pets;
using ExtraExplosives.NPCs.CaptainExplosiveBoss.BossProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Audio;
using static ExtraExplosives.GlobalMethods;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss
{
	[AutoloadBossHead]
	public class CaptainExplosiveBoss : ModNPC
	{
		//Variables:
		//private static int hellLayer => Main.maxTilesY - 200;

		private const int sphereRadius = 300;

		private float attackCool
		{
			get => npc.ai[0];
			set => npc.ai[0] = value;
		}

		private float moveCool
		{
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}

		private float rotationSpeed
		{
			get => npc.ai[2];
			set => npc.ai[2] = value;
		}

		private float captiveRotation
		{
			get => npc.ai[3];
			set => npc.ai[3] = value;
		}

		private int moveTime = 200;
		private int moveTimer = 60;
		private bool dontDamage;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Captain Explosive");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 9800;
			npc.damage = 100;
			npc.defense = 15;
			npc.knockBackResist = 0f;
			npc.width = 200;
			npc.height = 200;
			npc.value = Item.buyPrice(0, 20, 0, 0);
			npc.npcSlots = 15f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.buffImmune[24] = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CaptainExplosiveMusic");

			drawOffsetY = 50f;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}


		public override void AI()
		{
			//spawn npcs
			//if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
			//{
			//	for (int k = 0; k < 5; k++)
			//	{
			//		//int captive = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCType<CaptainExplosive>());
			//		//Main.npc[captive].ai[0] = npc.whoAmI;
			//		//Main.npc[captive].ai[1] = k;
			//		//Main.npc[captive].ai[2] = 50 * (k + 1);
			//		//if (k == 2)
			//		//{
			//		//	Main.npc[captive].damage += 20;
			//		//}
			//		//CaptainExplosive.SetPosition(Main.npc[captive]);
			//		//Main.npc[captive].netUpdate = true;
			//	}
			//	npc.netUpdate = true;
			//	npc.localAI[0] = 1f;
			//}
			
			//Phases
			if(((float)npc.life / (float)npc.lifeMax) > .66f) //above 66%, Phase 1
			{
				
			}
			else if(((float)npc.life / (float)npc.lifeMax) <= .66f && ((float)npc.life / (float)npc.lifeMax) > .33f) //Between 66% and 33%, Phase 2
			{
				
			}
			else if(((float)npc.life / (float)npc.lifeMax) <= .33f) //Below 33%, Phase 3
			{
				
			}
			
			//check for the players death
			Player player = Main.player[npc.target];
			if (!player.active || player.dead)
			{
				npc.TargetClosest(false);
				player = Main.player[npc.target];
				if (!player.active || player.dead)
				{
					npc.velocity = new Vector2(0f, 10f);
					if (npc.timeLeft > 120)
					{
						npc.timeLeft = 120;
					}
					return;
				}
			}

			//movement cool down
			moveCool -= 1f;

			//set the movement and move to the position
			if (Main.netMode != NetmodeID.MultiplayerClient && moveCool <= 0f)
			{
				npc.TargetClosest(false);
				player = Main.player[npc.target];
				//double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
				int distance = sphereRadius + Main.rand.Next(300);
				Vector2 playerPlus = new Vector2(player.Center.X, player.Center.Y - 320);
				Vector2 moveTo = playerPlus;
				moveCool = (float)moveTime - 20 - (float)Main.rand.Next(20);
				npc.velocity = ((moveTo - npc.Center) / moveCool); //depending on how far the player is increase speed
				rotationSpeed = (float)(Main.rand.NextDouble() + Main.rand.NextDouble());
				if (rotationSpeed > 1f)
				{
					rotationSpeed = 1f + (rotationSpeed - 1f) / 2f;
				}
				if (Main.rand.NextBool())
				{
					rotationSpeed *= -1;
				}
				//dust debug to check where the npc is going
				Dust dust = Main.dust[Terraria.Dust.NewDust(moveTo, 10, 10, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 15f)];
				dust.noGravity = true;
				dust.fadeIn = 10f;

				if(npc.velocity.X >= 0)
				{
					npc.direction = 1;
				}
				else
				{
					npc.direction = -1;
				}

				Main.NewText(moveCool);
				Main.NewText($"Velocity {npc.velocity}");
				//Main.NewText($"Direction {npc.direction}");

				rotationSpeed *= 0.01f;
				npc.netUpdate = true;
			}

			//the farther the player gets, make the movements happen more often
			if (Vector2.Distance(Main.player[npc.target].position, npc.position) > sphereRadius)
			{
				moveTimer--;
			}
			else
			{
				moveTimer += 3;
				if (moveTime >= 300 && moveTimer > 60)
				{
					moveTimer = 60;
				}
			}

			//Check the moveTimer and change it depending on the distance from the player and the boss
			if (moveTimer <= 0)
			{
				moveTimer += 60;
				moveTime -= 3;
				if (moveTime < 99)
				{
					moveTime = 99;
					moveTimer = 0;
				}
				npc.netUpdate = true;
			}
			else if (moveTimer > 60)
			{
				moveTimer -= 60;
				moveTime += 3;
				npc.netUpdate = true;
			}
			//sets the speed of captiveRotation for the npc to travel by
			captiveRotation += rotationSpeed;

			//checks the speed of captiveRotation to see how fast the npc should move
			if (captiveRotation < 0f)
			{
				captiveRotation += 2f * (float)Math.PI;
			}
			if (captiveRotation >= 2f * (float)Math.PI)
			{
				captiveRotation -= 2f * (float)Math.PI;
			}

			//attack cool down
			attackCool -= 1f;

			//The boss will spawn in projectiles depending on the life and a random chance
			if (Main.netMode != NetmodeID.MultiplayerClient && attackCool <= 0f)
			{
				attackCool = 200f + 200f * (float)npc.life / (float)npc.lifeMax - (float)Main.rand.Next(200);
				Vector2 delta = player.Center - npc.Center;
				float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
				if (magnitude > 0)
				{
					delta *= 10f / magnitude;
				}
				else
				{
					delta = new Vector2(0f, 5f);
				}
				int damage = (npc.damage - 30) / 2;
				if (Main.expertMode)
				{
					damage = (int)(damage / Main.expertDamage);
				}


				//spawn  the projectile
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, delta.X, delta.Y, ProjectileType<BossGooBombProjectile>(), 0, 3f, Main.myPlayer);
				npc.netUpdate = true;
			}

			//check if the mode is expert
			if (Main.expertMode)
			{
				
			}

			//Random chance for this to happen
			if (Main.rand.NextBool())
			{
				float radius = (float)Math.Sqrt(Main.rand.Next(sphereRadius * sphereRadius));
				double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
				//Dust.NewDust(new Vector2(npc.Center.X + radius * (float)Math.Cos(angle), npc.Center.Y + radius * (float)Math.Sin(angle)), 0, 0, DustType<Sparkle>(), 0f, 0f, 0, default(Color), 1.5f);
			}

			if (npc.direction == 1)
			{
				npc.spriteDirection = 1;
			}
			if (npc.direction == -1)
			{
				npc.spriteDirection = -1;
			}
			Main.NewText($"Direction {npc.direction}");

		}


		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 2.0; //change the frame speed
			npc.frameCounter %= 100.0; //How many frames are in the animation
			npc.frame.Y = frameHeight * ((int)npc.frameCounter % 16 / 4); //set the npc's frames here
		}
	}
}