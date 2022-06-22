using ExtraExplosives.NPCs.CaptainExplosiveBoss.BossProjectiles;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
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
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        private float moveCool
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private float rotationSpeed
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        private float captiveRotation
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        //public virtual string[] AltTexturesCap => new string[0];

        private int moveTime = 200;
        private int moveTimer = 60;
        private bool dontDamage;

        private int _droneTimer = 300;
        private int _dronesLeft = Main.rand.Next(1, 3) + 2;

        private bool _dropDynamite = false;


        private int amount = 3;

        private bool _carpetBombing = false;    // When true, will hijack CE's movement and fully control him to avoid any conflicts with other movement methods
        private int _carpetBombingCooldown = 360;
        private int _carpetBombingDelayTimer = 0;

        private bool firstTick = false;
        private bool firstAiTick = false;

        private int Check;
        private bool go;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Explosive");
            Main.npcFrameCount[NPC.type] = 4;

        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 9800;
            NPC.damage = 25;
            NPC.defense = 5;
            NPC.knockBackResist = 0f;
            NPC.width = 200;
            NPC.height = 200;
            NPC.value = Item.buyPrice(0, 20, 0, 0);
            NPC.npcSlots = 15f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CaptainExplosiveMusic");
            //bossBag = ItemType<CaptainExplosiveTreasureBag>();

            DrawOffsetY = 50f;
        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = 40;

            for (int i = 0; i < numPlayers; i++)
            {
                NPC.lifeMax += 2000;
            }
        }

        public override bool CheckDead()
        {
            Player player = Main.player[NPC.target];

            for (int k = 0; k < 12; k++)
            {
                Vector2 pos = NPC.position + new Vector2(Main.rand.Next(NPC.width - 8), Main.rand.Next(NPC.height / 2));
                Gore.NewGore(pos, new Vector2(Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10)), Mod.Find<ModGore>("Gores/CaptainExplosiveBoss/gore2").Type, 1.2f);
            }
            for (int k = 0; k < 12; k++)
            {
                Vector2 pos = NPC.position + new Vector2(Main.rand.Next(NPC.width - 8), Main.rand.Next(NPC.height / 2));
                Gore.NewGore(pos, new Vector2(Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10)), Mod.Find<ModGore>("Gores/CaptainExplosiveBoss/gore1").Type, 1.2f);
            }

            int Boss0 = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y + 50, NPCType<CaptainExplosiveBossAt0>(), 0, 0, 0, 0, 0, player.whoAmI);
            Main.npc[Boss0].netUpdate = true;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, Boss0);

            ExtraExplosives.CheckUIBoss = 3;

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket myPacket = Mod.GetPacket();
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.setBossInactive);
                myPacket.Send();
            }

            return false;
        }

        public override void AI()
        {
            if (!firstTick)
            {

                //get the player
                ExtraExplosives.CheckUIBoss = 1;

                if (Main.netMode == NetmodeID.Server)
                {
                    ModPacket myPacket = Mod.GetPacket();
                    myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkBossActive);
                    myPacket.Send();

                }

                firstTick = true; //so this only happens once

                //Main.NewText(ExtraExplosives.CheckUIBoss);

                NPC.immortal = true; //Check into since I think it has something wrong in MP, check example mod to see how they do it again

                NPC.damage = 0; //make the npc does not do damage to the player

                //Main.NewText(player.whoAmI);
                Check = 1; //pause the boss

                //Main.NewText("Only run once");
            }

            if (Check != 3)
            {
                Check = ExtraExplosives.CheckUIBoss; //Get the button click
                                                     //Main.NewText(Check);
                                                     //Main.NewText(ExtraExplosives.CheckUIBoss);
            }

            if (Check == 2 || go)
            {
                //Main.NewText("Set");

                if (!firstAiTick)
                {

                    NPC.damage = 100;
                    NPC.immortal = false;

                    go = true;
                    Check = 3;

                    firstAiTick = true;
                }

                //Phases
                //##############################################
                if (((float)NPC.life / (float)NPC.lifeMax) > .66f) //above 66%, Phase 1
                {
                    callDrones(1);
                    callBombAtk(200);
                }
                else if (((float)NPC.life / (float)NPC.lifeMax) <= .66f && ((float)NPC.life / (float)NPC.lifeMax) > .33f) //Between 66% and 33%, Phase 2
                {
                    callDrones(2);
                    callBombAtk(250);
                    if (_dropDynamite && attackCool >= 200)
                    {
                        NPC.velocity = Vector2.Multiply(NPC.velocity, 0.75f);
                        if (Main.expertMode)
                        {
                            fireRocket();
                        }
                        else
                        {
                            dropDynamite();
                        }
                    }
                }
                else if (((float)NPC.life / (float)NPC.lifeMax) <= .33f) //Below 33%, Phase 3
                {
                    if (_carpetBombingCooldown-- <= 0)
                    {
                        _carpetBombing = true;
                    }
                    if (_carpetBombing)
                    {
                        CarpetBombing();
                        return;
                    }
                    callDrones(3);
                    callBombAtk(260);
                }
                //##############################################

                //check for the players death
                Player player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.TargetClosest(false);
                    player = Main.player[NPC.target];
                    if (!player.active || player.dead)
                    {
                        NPC.velocity = new Vector2(0f, -15f);
                        if (NPC.timeLeft > 120)
                        {
                            NPC.timeLeft = 120;
                        }
                        return;
                    }
                }

                //movement cool down
                moveCool -= 1f;

                //set the movement and move to the position
                if (Main.netMode != NetmodeID.MultiplayerClient && moveCool <= 0f)
                {

                    NPC.TargetClosest(false);
                    player = Main.player[NPC.target];
                    //double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
                    //int distance = sphereRadius + Main.rand.Next(300);

                    float nextFloat;

                    nextFloat = Main.rand.NextFloat(-250, 250);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket myPacket = Mod.GetPacket();
                        myPacket.Write((byte)ExtraExplosives.EEMessageTypes.bossMovment);
                        myPacket.Write((float)nextFloat);
                        myPacket.Send();

                        nextFloat = ExtraExplosives.bossDirection;
                    }

                    //set the movement
                    Vector2 playerPlus = new Vector2(player.Center.X + nextFloat, player.Center.Y - 320);


                    //get a head of the player while moving -------------------------------
                    if (player.direction == 1 && player.velocity.X > 5f)
                    {
                        nextFloat = Main.rand.NextFloat(400, 600); //set the amount

                        if (Main.netMode == NetmodeID.Server) //set it to the clients
                        {
                            ModPacket myPacket = Mod.GetPacket();
                            myPacket.Write((byte)ExtraExplosives.EEMessageTypes.bossMovment);
                            myPacket.Write((float)nextFloat);
                            myPacket.Send();

                            nextFloat = ExtraExplosives.bossDirection;
                        }

                        playerPlus = new Vector2(player.Center.X + nextFloat, player.Center.Y - 320);

                    }
                    else if (player.direction == -1 && player.velocity.X < -5f)
                    {
                        nextFloat = Main.rand.NextFloat(-400, -600); //set the amount

                        if (Main.netMode == NetmodeID.Server) //set it to the clients
                        {
                            ModPacket myPacket = Mod.GetPacket();
                            myPacket.Write((byte)ExtraExplosives.EEMessageTypes.bossMovment);
                            myPacket.Write((float)nextFloat);
                            myPacket.Send();

                            nextFloat = ExtraExplosives.bossDirection;
                        }

                        playerPlus = new Vector2(player.Center.X + nextFloat, player.Center.Y - 320);
                    }//-------------------------------------------------------------------

                    //move to the player position
                    Vector2 moveTo = playerPlus;
                    moveCool = (float)moveTime - 20 - (float)Main.rand.Next(20);
                    NPC.velocity = ((moveTo - NPC.Center) / moveCool * 1.5f); //depending on how far the player is increase speed
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
                    //Dust dust = Main.dust[Terraria.Dust.NewDust(moveTo, 10, 10, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 15f)];
                    //dust.noGravity = true;
                    //dust.fadeIn = 10f;

                    if (NPC.velocity.X >= 0)
                    {
                        NPC.direction = 1;
                    }
                    else
                    {
                        NPC.direction = -1;
                    }

                    //Main.NewText(moveCool);
                    //Main.NewText($"Velocity {npc.velocity}");
                    //Main.NewText($"Direction {npc.direction}");

                    rotationSpeed *= 0.01f;
                    NPC.netUpdate = true;

                    //Main.NewText($"Rotation: {npc.rotation}");
                }

                //Main.NewText($"Rotation: {npc.rotation}");
                //rotation code
                if (NPC.velocity.X > 17f && NPC.rotation <= .5f) //right
                {
                    NPC.rotation += .05f;
                    if (NPC.rotation >= .5f)
                    {
                        NPC.rotation = .5f;
                    }
                }
                else if (NPC.rotation >= 0f)
                {
                    NPC.rotation -= .05f;
                }


                if (NPC.velocity.X < -17f && NPC.rotation >= -.5f) //left
                {
                    NPC.rotation -= .05f;
                    if (NPC.rotation <= -.5f)
                    {
                        NPC.rotation = -.5f;
                    }
                }
                else if (NPC.rotation <= 0f)
                {
                    NPC.rotation += .05f;
                }



                //the farther the player gets, make the movements happen more often
                if (Vector2.Distance(Main.player[NPC.target].position, NPC.position) > sphereRadius)
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
                    NPC.netUpdate = true;
                }
                else if (moveTimer > 60)
                {
                    moveTimer -= 60;
                    moveTime += 3;
                    NPC.netUpdate = true;
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



                //check if the mode is expert
                if (Main.expertMode)
                {

                }

                //Random chance for this to happen
                //if (Main.rand.NextBool())
                //{
                //	float radius = (float)Math.Sqrt(Main.rand.Next(sphereRadius * sphereRadius));
                //	double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
                //	//Dust.NewDust(new Vector2(npc.Center.X + radius * (float)Math.Cos(angle), npc.Center.Y + radius * (float)Math.Sin(angle)), 0, 0, DustType<Sparkle>(), 0f, 0f, 0, default(Color), 1.5f);
                //}

                if (NPC.direction == 1)
                {
                    NPC.spriteDirection = 1;
                }
                if (NPC.direction == -1)
                {
                    NPC.spriteDirection = -1;
                }


            }
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                player.AddBuff(BuffID.OnFire, 600);
                player.AddBuff(BuffID.BrokenArmor, 600);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / NPC.lifeMax * 100.0; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, hitDirection, -1f, 0, new Color(255, 255, 255), 2.5f);
            }

            SoundEngine.PlaySound(SoundID.NPCHit4, (int)NPC.position.X, (int)NPC.position.Y);
        }


        public override void FindFrame(int frameHeight)
        {

            NPC.frameCounter += 2.0; //change the frame speed
            NPC.frameCounter %= 100.0; //How many frames are in the animation
            NPC.frame.Y = frameHeight * ((int)NPC.frameCounter % 16 / 4); //set the npc's frames here

        }

        private void _spawnMinions()
        {
            int spawnCase = Main.rand.Next(3);  // gets the side the drone will spawn from
            Vector2 spawnCord = new Vector2(NPC.position.X + (spawnCase * 100), NPC.position.Y + (spawnCase == 1 ? 60 : 0) + 180);  // calculates the cords of the spawn loc
            int drone = NPC.NewNPC((int)spawnCord.X, (int)spawnCord.Y, NPCType<CEDroneNPC>(), 0, spawnCase, 0, 0, 0, this.NPC.type);    // spawns the drone at those cords
            SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/DronePlop")); //sound
                                                                                                                                //Dust.NewDust(spawnCord, 8, 8, Main.rand.Next(250)); // spawns dust
            Main.npc[drone].netUpdate = true;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, drone);
        }

        public void chooseBomb(int direction)
        {
            int x = 0;
            int y = 0;

            Vector2 delta = new Vector2();

            if (direction == 1) //left
            {
                delta = new Vector2(-3, -2);
                x = 0;
                y = 180;
            }
            else if (direction == 2) //center
            {
                delta = new Vector2(0, 0);
                x = 100;
                y = 240;
            }
            else if (direction == 3) //right
            {
                delta = new Vector2(3, -2);
                x = 200;
                y = 180;
            }

            //spawn the projectile
            int choose = Main.rand.Next(0, 5); //might need to be a global var for syncing?

            switch (choose)
            {
                case 0:
                    Projectile.NewProjectile(NPC.position.X + x, NPC.position.Y + y, delta.X, delta.Y, ProjectileType<BossGooBombProjectile>(), 0, 3f, Main.myPlayer);
                    break;
                case 1:
                    Projectile.NewProjectile(NPC.position.X + x, NPC.position.Y + y, delta.X, delta.Y, ProjectileType<BossArmorBreakBombProjectile>(), 0, 3f, Main.myPlayer);
                    break;
                case 2:
                    Projectile.NewProjectile(NPC.position.X + x, NPC.position.Y + y, delta.X, delta.Y, ProjectileType<BossChillBombProjectile>(), 0, 3f, Main.myPlayer);
                    break;
                case 3:
                    Projectile.NewProjectile(NPC.position.X + x, NPC.position.Y + y, delta.X, delta.Y, ProjectileType<BossFireBombProjectile>(), 0, 3f, Main.myPlayer);
                    break;
                case 4:
                    Projectile.NewProjectile(NPC.position.X + x, NPC.position.Y + y, delta.X, delta.Y, ProjectileType<BossDazedBombProjectile>(), 0, 3f, Main.myPlayer);
                    break;
                default:
                    Projectile.NewProjectile(NPC.position.X + x, NPC.position.Y + y, delta.X, delta.Y, ProjectileType<BossFireBombProjectile>(), 0, 3f, Main.myPlayer);
                    break;
            }

        }

        public void callDrones(int multi)
        {
            if (_droneTimer-- <= 0) // if drones can be spawned
            {
                if (_dronesLeft > 0 && _droneTimer % 30 == 0)   // is there another drone ready to spawn, and has enough time passed since the last spawn
                {
                    _dronesLeft--;  // use one available drone
                    _spawnMinions();// spawns a drone
                }
                else if (_dronesLeft <= 0)  // if no drones are left to spawn
                {
                    _droneTimer = 240 + (Main.rand.Next(240) - 120);    // reset the timer (simplifies to between 2-6 seconds)
                    _dronesLeft = (Main.rand.Next(1, 3) + 2) * multi;     // how many drones to spawn the in the next round (between 3-5)
                }
            }
        }

        public void callBombAtk(int cooldown)
        {
            // The boss will spawn in projectiles depending on the life and a random chance
            if (Main.netMode != NetmodeID.MultiplayerClient && attackCool <= 0f)
            {
                attackCool = 200f + 200f * (float)NPC.life / (float)NPC.lifeMax - (float)Main.rand.Next(cooldown);

                for (int i = 0; i < 25; i++)
                {
                    Dust dust = Main.dust[Terraria.Dust.NewDust(new Vector2(NPC.position.X + 0, NPC.position.Y + 180), 30, 30, 6, -3f, -2f, 0, new Color(255, 0, 0), 5f)];
                    Dust dust2 = Main.dust[Terraria.Dust.NewDust(new Vector2(NPC.position.X + 100, NPC.position.Y + 240), 30, 30, 6, 0f, 0f, 0, new Color(255, 0, 0), 5f)];
                    Dust dust3 = Main.dust[Terraria.Dust.NewDust(new Vector2(NPC.position.X + 200, NPC.position.Y + 180), 30, 30, 6, 3f, -22f, 0, new Color(255, 0, 0), 5f)];
                }

                //direction 1 = left, 2 = center, 3 = right
                //chooseBomb(1); //Left
                //chooseBomb(2); //Center
                //chooseBomb(3); //Right

                _dropDynamite = true;

                //create all the bombs
                for (int i = 0; i < 3; i++)
                {
                    chooseBomb(i + 1);
                }

                //npc.ai[3] = 0;
                //go = true;
                //npc.netUpdate = true;
            }
        }


        // Holds the current direction of travel
        private int _dir = 0;

        private float[] _carpetBombingValues = new float[]
        {
            0,	// Starting position, DO NOT CHANGE WHILE HANDLING ATTACK
			0,	// Rockets remaining, remove 1 for each rocket fired
			0,	// Cooldown before next rocket is fired
			0	// Reset time, time to wait after attacking before attacking again (NOT A SECOND CB RUN BUT ANY ATTACK)
		};
        /// <summary>
        /// THis is a wrapper method which just choosed which method should be used and then holds that until the next attack
        /// DO NOT CALL EITHER OF THE OTHER METHODS, THEY ONLY RUN ONCE
        /// </summary>
        public void CarpetBombing()
        {
            //Main.NewText("Carpet");
            if (_carpetBombingDelayTimer >= 120)
            {
                //Main.NewText("Hit Carpet");
                if (_carpetBombingValues[3] == 1)   // Reset
                {
                    _carpetBombingValues[0] = 0;
                    _carpetBombingValues[1] = 0;
                    _carpetBombingValues[2] = 0;
                    _carpetBombingValues[3] = 0;
                    _carpetBombingDelayTimer = 0;
                    _dir = 0;
                    _carpetBombing = false;
                    _carpetBombingCooldown = 720;   // Time before next run
                    return; // stop the rest of the method running
                }
                if (_dir == 1) CarpetBombingLeftToRight();  // type 1
                else if (_dir == -1) CarpetBombingRightToLeft();    // type 2
                else if (_dir == 0) ChooseDirection();
                //Main.NewText("Something went wrong will setting up a carpet bombing run");
            }
            else
            {
                _carpetBombingDelayTimer++;
                NPC.velocity = new Vector2(0, 0);
            }

        }

        private void ChooseDirection()  // sets up the needed values prior to carrying out the run
        {
            _dir = Main.rand.NextBool() ? 1 : -1;   // Get the direction to run in
            NPC.netUpdate = true;
            _carpetBombingValues[0] = NPC.position.X;   // get the starting position
        }
        /// <summary>
        /// DO NOT CALL THIS METHOD, CALL THE WRAPPER (CarpetBombing)
        /// </summary>
        private void CarpetBombingLeftToRight()
        {
            // 1-Move to left of screen (1000),
            // 2-Move from left to right dropping bombs in chosen pattern TODO make patterns
            // 3-Reset TODO figure out exact reset pattern
            /*if (_carpetBombingValues[3] != 0)	// if CE is resetting
			{
				// TODO basic movement code, should be slow and floaty to allow the player some time to get attacks in
				//Main.NewText("Resetting");//debug
				_carpetBombingValues[3]--;	// each tick
				npc.velocity.X += (Main.player[npc.target].position.X - npc.position.X)/16f;	// change the x velocity so its more randomized
				if (npc.velocity.X > 10) npc.velocity.X = 10;	// if the velocity is too high, lower it
				else if (npc.velocity.X < -10) npc.velocity.X = -10;	// if the velocity is to low, raise it
				if (npc.position.Y - 100 < Main.player[npc.target].position.Y) npc.velocity.Y += 0.8f;	// if the y pos is too high, lower it
			}*/
            if (_carpetBombingValues[1] != 0)   // if currently dropping bombs
            {
                if (_carpetBombingValues[2]-- <= 0) // if rocket cooldown is done
                {
                    Projectile.NewProjectileDirect(new Vector2(NPC.position.X + 100, NPC.position.Y + 240), new Vector2(0, 15), ModContent.ProjectileType<BossCarpetBomb>(), 30, 20);
                    _carpetBombingValues[1]--;  // one less rocket
                    _carpetBombingValues[2] = 4;    // time between rocket drop
                                                    //Main.NewText(_carpetBombingValues[1]);	//debug
                    if (_carpetBombingValues[1] <= 0)   // if no rockets left
                    {
                        _carpetBombingValues[3] = 1;    //reset time
                    }
                }
            }
            else
            {
                if (NPC.position.Y - 60 < Main.player[NPC.target].position.Y)   // deal with y position
                {
                    NPC.position.Y -= 5f;   // to low
                }
                else
                {
                    NPC.velocity.Y *= 0.75f;    // high enough, just hover
                }
                if (NPC.position.X > _carpetBombingValues[0] - 1000)    // not far enough left
                {
                    NPC.velocity.X -= 1.1f; // move further right
                }
                else if (NPC.velocity.X > 0.25f)    // if still moving right after moving 1000 tiles	
                {
                    NPC.velocity.X *= 0.75f;    //slow down
                }
                else  // if x and y are set
                {
                    NPC.velocity.X = 12;    // set x velocity
                    NPC.velocity.Y = 0;     // stop y movement
                    _carpetBombingValues[1] = 40;   // rockets left
                }
            }
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI);
        }

        /// <summary>
        /// DO NOT CALL THIS METHOD, CALL THE WRAPPER (CarpetBombing)
        /// </summary>
        private void CarpetBombingRightToLeft()     // Methods are identical but values revered, just reference other one for comments
        {
            // 1-Move to left of screen (1000),
            // 2-Move from left to right dropping bombs in chosen pattern TODO make patterns
            // 3-Reset TODO figure out exact reset pattern
            /*if (_carpetBombingValues[3] != 0)
			{
				// TODO basic movement code, should be slow and floaty to allow the player some time to get attacks in
				//Main.NewText("Resetting");
				_carpetBombingValues[3]--;
				npc.velocity.X += Main.player[npc.target].position.X - npc.position.X;
				if (npc.velocity.X > 10) npc.velocity.X = 10;
				else if (npc.velocity.X < -10) npc.velocity.X = -10;
				if (npc.position.Y - 100 < Main.player[npc.target].position.Y) npc.velocity.Y += 0.8f;
			}*/
            if (_carpetBombingValues[1] != 0)
            {
                if (_carpetBombingValues[2]-- <= 0)
                {
                    Projectile.NewProjectileDirect(new Vector2(NPC.position.X + 100, NPC.position.Y + 240), new Vector2(0, 15), ModContent.ProjectileType<BossCarpetBomb>(), 30, 20);
                    _carpetBombingValues[1]--;
                    _carpetBombingValues[2] = 4;
                    //Main.NewText(_carpetBombingValues[1]);
                    if (_carpetBombingValues[1] <= 0)
                    {
                        _carpetBombingValues[3] = 1;
                    }
                }
            }
            else
            {
                if (NPC.position.Y - 60 < Main.player[NPC.target].position.Y)
                {
                    NPC.position.Y -= 5f;   // to low	
                }
                else
                {
                    NPC.velocity.Y *= 0.75f;
                }
                if (NPC.position.X < _carpetBombingValues[0] + 1000)
                {
                    NPC.velocity.X += 1.1f;
                }
                else if (NPC.velocity.X > 0.25f)
                {
                    NPC.velocity.X *= 0.75f;
                }
                else
                {
                    NPC.velocity.X = -12;
                    NPC.velocity.Y = 0;
                    _carpetBombingValues[1] = 40;
                }
            }

            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI);
        }

        public void dropDynamite()
        {//might need to get rid of the Main.rand change it to a global so we can sync it

            int rand = 0;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                rand = Main.rand.Next(5);
            }

            if (Main.netMode == NetmodeID.Server)
            {
                rand = Main.rand.Next(5);

                ModPacket myPacket = Mod.GetPacket();
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.BossCheckDynamite);
                myPacket.WriteVarInt(rand);
                myPacket.Send();

                rand = ExtraExplosives.bossDropDynamite;
            }

            //Main.NewText(rand);

            if (rand == 0 && Vector2.Distance(NPC.velocity, Vector2.Zero) < 0.1f && attackCool >= 200)
            {
                int drop = NPC.NewNPC((int)(NPC.position.X + 100), (int)(NPC.position.Y + 240), ModContent.NPCType<BossDynamiteNPC>());
                Main.npc[drop].netUpdate = true;
                _dropDynamite = false;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, drop);
            }
        }

        public void fireRocket()
        {//might need to get rid of the Main.rand change it to a global so we can sync it

            int rand = 0;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                rand = Main.rand.Next(5);
            }

            if (Main.netMode == NetmodeID.Server)
            {
                rand = Main.rand.Next(5);

                ModPacket myPacket = Mod.GetPacket();
                myPacket.Write((byte)ExtraExplosives.EEMessageTypes.bossCheckRocket);
                myPacket.WriteVarInt(rand);
                myPacket.Send();

                rand = ExtraExplosives.bossDropDynamite;
            }

            //Main.NewText(rand);

            if (rand == 0 && Vector2.Distance(NPC.velocity, Vector2.Zero) < 0.1f && attackCool >= 200)
            {
                int drop = NPC.NewNPC((int)(NPC.position.X + 100), (int)(NPC.position.Y + 240), ModContent.NPCType<BossRocket>());
                Main.npc[drop].netUpdate = true;
                _dropDynamite = false;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, drop);
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        //big tnt and carpet bomb need fixed using netcode
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((bool)firstTick);
            writer.Write((bool)firstAiTick);
            writer.Write((short)Check);
            writer.Write((short)moveTime);
            writer.Write((short)moveTimer);
            writer.Write((short)_droneTimer);
            writer.Write((short)_dronesLeft);
            //writer.Write((bool)_dropDynamite);
            writer.Write((short)_carpetBombingCooldown);
            writer.Write((short)_carpetBombingDelayTimer);
            writer.Write((bool)_carpetBombing);
            writer.Write((short)_dir);
            //writer.Write((short)rand);

            for (int i = 0; i < _carpetBombingValues.Length; i++)
            {
                writer.Write((float)_carpetBombingValues[i]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            firstTick = reader.ReadBoolean();
            firstAiTick = reader.ReadBoolean();
            Check = reader.ReadInt16();
            moveTime = reader.ReadInt16();
            moveTimer = reader.ReadInt16();
            _droneTimer = reader.ReadInt16();
            _dronesLeft = reader.ReadInt16();
            _carpetBombingCooldown = reader.ReadInt16();
            _carpetBombingDelayTimer = reader.ReadInt16();
            //_dropDynamite = reader.ReadBoolean();
            _carpetBombing = reader.ReadBoolean();
            _dir = reader.ReadInt16();
            //rand = reader.ReadInt16();

            for (int i = 0; i < _carpetBombingValues.Length; i++)
            {
                _carpetBombingValues[i] = reader.ReadSingle();
            }
        }

    }
}
