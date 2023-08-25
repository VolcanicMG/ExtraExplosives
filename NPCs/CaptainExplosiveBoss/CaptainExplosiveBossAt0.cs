using ExtraExplosives.Items;
using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Accessories.BombardierClassAccessories;
using ExtraExplosives.Items.Explosives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.NPCs.CaptainExplosiveBoss
{
    [AutoloadBossHead]
    public class CaptainExplosiveBossAt0 : ModNPC
    {
        //Variables:
        //private static int hellLayer => Main.maxTilesY - 200;

        private const int sphereRadius = 300;
        private const int PickPower = 45;

        private float moveCool
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Captain Explosive");
            Main.npcFrameCount[NPC.type] = 4;

        }

        //public override void AutoStaticDefaults()
        //{
        //	//AltTextures[0] = "NPCs/CaptainExplosiveBoss/CaptainExplosiveBoss";
        //	//AltTextures[1] = "NPCs/CaptainExplosiveBoss/CaptainExplosiveBossDamaged";
        //}

        private bool firstTick;
        private bool flag;
        private bool flag2;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 9800;
            NPC.damage = 80;
            NPC.defense = 999999;
            NPC.knockBackResist = 0f;
            NPC.width = 200;
            NPC.height = 200;
            NPC.value = Item.buyPrice(0, 7, 0, 0);
            NPC.npcSlots = 15f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            //Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CaptainExplosiveMusic");

            //bossBag/* t- ModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ItemType<CaptainExplosiveTreasureBag>();
            NPC.immortal = true;

            DrawOffsetY = 50f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // TODO npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CaptainExplosiveTreasureBag>()));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        //public override bool CheckDead()
        //{
        //	if (!flag)
        //	{
        //		for (int i = 1; i < 12; i++)
        //		{
        //			for (int k = 0; k < 4; k++)
        //			{
        //				Vector2 pos = npc.position + new Vector2(Main.rand.Next(npc.width - 8), Main.rand.Next(npc.height / 2));
        //				Gore.NewGore(pos, new Vector2(Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10)), mod.GetGoreSlot("Gores/CaptainExplosiveBoss/gore" + i), 1.5f);
        //			}
        //		}

        //		for (int g = 0; g < 50; g++)
        //		{
        //			int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
        //			Main.gore[goreIndex].scale = 2.5f;
        //			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
        //			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
        //			goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
        //			Main.gore[goreIndex].scale = 2.5f;
        //			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
        //			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
        //			goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
        //			Main.gore[goreIndex].scale = 2.5f;
        //			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
        //			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
        //			goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
        //			Main.gore[goreIndex].scale = 2.5f;
        //			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
        //			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;

        //		}

        //		CreateExplosion(npc.Center, 25);

        //		ExplosionDamage(10f * 2f, npc.Center, 1000, 30, Main.myPlayer);

        //		Main.NewText("I only run once");

        //		flag = true;

        //	}
        //	return true;
        //}

        private void CreateExplosion(Vector2 position, int radius)
        {
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        ushort tile = Main.tile[xPosition, yPosition].TileType;
                        if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
                        {
                        }
                        else //Breakable
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, (float)xPosition, (float)yPosition, 0f, 0, 0, 0);
                        }
                    }
                }
            }
        }


        public override void AI()
        {
            Player player = Main.player[NPC.target];

            //Main.NewText(npc.active);

            if (!firstTick)
            {
                NPC.life = 1;
                firstTick = true;
            }

            //check for the players death
            //if (!player.active || player.dead)
            //{
            //	npc.TargetClosest(false);
            //	player = Main.player[npc.target];
            //	if (!player.active || player.dead)
            //	{
            //		npc.velocity = new Vector2(0f, -15f);
            //		if (npc.timeLeft > 120)
            //		{
            //			npc.timeLeft = 120;
            //		}
            //		return;
            //	}
            //}

            NPC.TargetClosest(true);
            Vector2 vector89 = new Vector2(NPC.Center.X, NPC.Center.Y);
            float num716 = Main.player[NPC.target].Center.X - vector89.X;
            float num717 = Main.player[NPC.target].Center.Y - vector89.Y;
            float num718 = (float)Math.Sqrt((double)(num716 * num716 + num717 * num717));
            float num719 = 24f;
            num718 = num719 / num718;
            num716 *= num718;
            num717 *= num718;
            NPC.velocity.X = ((NPC.velocity.X * 100f + num716) / 101f);
            NPC.velocity.Y = ((NPC.velocity.Y * 100f + num717) / 101f);

            //check to see if its time to kill the boss
            if (NPC.ai[3] >= 500)
            {

                //NPCLoot();
                ExtraExplosivesSystem.BossCheckDead = true;
                ////SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/CaptainExplosion")); //sound
                //SoundEngine.PlaySound(new SoundStyle("ExtraExplosives/Assets/Sounds/Custom/CaptainExplosion"));

                NPC.immortal = false;
                NPC.netUpdate = true;

                //NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, 99999f, 0f, 0f, 0, 0, 0);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    NPC.StrikeNPCNoInteraction(10000, 1, -NPC.direction, false, false, false);
                }

                if (!flag)
                {
                    for (int i = 1; i < 12; i++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            Vector2 pos = NPC.position + new Vector2(Main.rand.Next(NPC.width - 8), Main.rand.Next(NPC.height / 2));
                            Gore.NewGore(NPC.GetSource_Death(), pos, new Vector2(Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10)), Mod.Find<ModGore>("Gores/CaptainExplosiveBoss/gore" + i).Type, 1.5f);
                        }
                    }

                    for (int g = 0; g < 15; g++)
                    {
                        int goreIndex = Gore.NewGore(NPC.GetSource_Death(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 2.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(NPC.GetSource_Death(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 2.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(NPC.GetSource_Death(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 64f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 2.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(NPC.GetSource_Death(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 84f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 2.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;

                    }

                    if (ExtraExplosives.CheckBossBreak)
                    {
                        CreateExplosion(NPC.Center, 25);
                    }

                    //call the explostion at the end and check for damage
                    ExplosionDamageEnemy(25, NPC.Center, 1000, NPC.whoAmI);

                    //Main.NewText("I only run once");

                    flag = true;

                }

                //npc.active = false;
            }

            NPC.ai[3]++;
            //Main.NewText(npc.velocity);
        }

        public override bool PreKill()
        {
            return true;
        }

        public override void OnKill()  // What will drop when the npc is killed?
        {
            if (!flag2)
            {
                if (Main.expertMode)    // Expert mode only loot
                {
                    //npc.DropBossBags(); // Boss bag

                    NPC.DropItemInstanced(NPC.position, NPC.Size, ItemType<CaptainExplosiveTreasureBag>(), 1, false);

                }

                int drop = Main.rand.NextBool() ? ItemType<BombardierEmblem>() : ItemType<RandomFuel>();   // which item will 100% drop
                int dropChance = drop == ItemType<BombardierEmblem>() ? ItemType<RandomFuel>() : ItemType<BombardierEmblem>();    // find the other item
                NPC.DropItemInstanced(NPC.position, new Vector2(NPC.width, NPC.height), drop);  // drop the confirmed item
                NPC.DropItemInstanced(NPC.position, new Vector2(NPC.width, NPC.height), ItemID.GoldCoin, 7);

                //A litte over 50% boost if check break is true
                if (ExtraExplosives.CheckBossBreak)
                {
                    int cntr = 0; //to make sure the boss doesn't drop more than one bomb.

                    if (Main.rand.Next(3) == 0) NPC.DropItemInstanced(NPC.position, new Vector2(NPC.width, NPC.height), dropChance);    // if the roll is successful drop the other

                    foreach (int item in bombs) //for each bomb on the list test to see if it should drop
                    {
                        if (Main.rand.Next(3) == 0 && cntr != 5)
                        {
                            NPC.DropItemInstanced(NPC.position, new Vector2(NPC.width, NPC.height), item);    //spawn the bomb
                            cntr++;
                        }
                    }
                }
                else
                {
                    if (Main.rand.Next(7) == 0) NPC.DropItemInstanced(NPC.position, new Vector2(NPC.width, NPC.height), dropChance);    // if the roll is successful drop the other
                }

                flag2 = true;

            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Captain Explosive";
            potionType = ItemID.HealingPotion;
        }


        public override void FindFrame(int frameHeight)
        {

            NPC.frameCounter += 2.0; //change the frame speed
            NPC.frameCounter %= 100.0; //How many frames are in the animation
            NPC.frame.Y = frameHeight * ((int)NPC.frameCounter % 16 / 4); //set the npc's frames here

        }

        //public override void HitEffect(int hitDirection, double damage)
        //{
        //	if (npc.life <= 0)
        //	{

        //	}
        //}

        private float _timer = 0;
        private int _color = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            // Retrieve reference to shader
            var deathShader = GameShaders.Misc["bombshader"];
            switch (_color)
            {
                case 0:
                    deathShader.UseColor(0, 0, 0).UseSaturation((_timer / 5) * _timer); // Base (this does nothing but ensure the shader doesnt break)
                    if (_timer > 1f)
                    {
                        _color = 1;
                    }
                    break;
                case 1:
                    deathShader.UseColor(0.5f, 0.05f, 0.05f).UseSaturation((_timer / 3) * _timer);  // Red (increase the number to slow the speed, decrease to make it faster)
                    if (_timer > 2.5f)
                    {
                        _color = 2;
                    }
                    break;

                case 2:
                    deathShader.UseColor(0.5f, 0.25f, 0.05f).UseSaturation((_timer / 2) * _timer); // Orange (see previous)
                    if (_timer > 3.8f)
                    {
                        _color = 3;
                    }
                    break;
                case 3:
                    deathShader.UseColor(.5f, .5f, 0.05f).UseSaturation(_timer * _timer);   // Yellow (see previous)
                    break;
            }
            //Main.NewText(_timer);
            // Call Apply to apply the shader to the SpriteBatch. Only 1 shader can be active at a time.
            deathShader.Apply(null);

            _timer += 0.01f;
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            // As mentioned above, be sure not to forget this step.
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {

            //writer.Write((bool)ded);
            //writer.Write(firstTick);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {

            //ded = reader.ReadBoolean();
            //firstTick = reader.ReadBoolean();
        }

        private int[] bombs = new int[]        // List of bombs which should drop from CE boss bag,
            {
                ModContent.ItemType<BasicExplosiveItem>(),
                ModContent.ItemType<SmallExplosiveItem>(),
                ModContent.ItemType<MediumExplosiveItem>(),
                ModContent.ItemType<LargeExplosiveItem>(),
                ModContent.ItemType<MegaExplosiveItem>(),
                ModContent.ItemType<GiganticExplosiveItem>(),
                ModContent.ItemType<TorchBombItem>(),
                ModContent.ItemType<DynaglowmiteItem>(),
                ModContent.ItemType<BigBouncyDynamiteItem>(),
                ModContent.ItemType<HouseBombItem>(),
                ModContent.ItemType<ClusterBombItem>(),
                ModContent.ItemType<PhaseBombItem>(),
                ModContent.ItemType<TheLevelerItem>(),
                ModContent.ItemType<DeliquidifierItem>(),
                ModContent.ItemType<HydromiteItem>(),
                ModContent.ItemType<LavamiteItem>(),
                ModContent.ItemType<CritterBombItem>(),
                ModContent.ItemType<BunnyiteItem>(),
                ModContent.ItemType<BreakenTheBankenItem>(),
                ModContent.ItemType<HeavyBombItem>(),
                ModContent.ItemType<DaBombItem>(),
                ModContent.ItemType<ReforgeBombItem>(),
                ModContent.ItemType<MeteoriteBusterItem>(),
                ModContent.ItemType<TrollBombItem>(),
                ModContent.ItemType<FlashbangItem>(),
                ModContent.ItemType<RainboomItem>(),
				//ModContent.ItemType<HotPotatoItem>()
			};
    }
}