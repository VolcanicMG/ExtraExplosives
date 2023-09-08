using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Items.Pets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.NPCs
{
    [AutoloadHead]
    public class CaptainExplosive : ModNPC
    {
        //Variables:
        public const string ShopName = "Shop";

        public static bool CaptianIsDed = true;

        private const int PickPower = 50;

        private bool shopOpen = false;
        private int nextSlot = 0;


        /* TODO public override bool IsLoadingEnabled(Mod mod)/* t-ModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead #1#
        {
            name = "CaptainExplosive";
            return Mod.Properties/* t-ModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) #1#.Autoload;
        }*/

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Captain Of Explosives");
            Main.npcFrameCount[NPC.type] = 25; //npc defines how many frames the npc sprite sheet has
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 750; //150 //npc defines the npc danger detect range
            NPCID.Sets.AttackType[NPC.type] = 0; //npc is the attack type,  0 (throwing), 1 (shooting), or 2 (magic). 3 (melee)
            NPCID.Sets.AttackTime[NPC.type] = 50; //npc defines the npc attack speed
            NPCID.Sets.AttackAverageChance[NPC.type] = 20;//npc defines the npc atack chance
            NPCID.Sets.HatOffsetY[NPC.type] = 4; //npc defines the party hat position
        }

        public override void SetDefaults()
        {
            //npc.name = "Custom Town NPC";   //the name displayed when hovering over the npc ingame.
            NPC.townNPC = true; //npc defines if the npc is a town Npc or not
            NPC.friendly = true;  //npc defines if the npc can hur you or not()
            NPC.width = 40; //the npc sprite width
            NPC.height = 56;  //the npc sprite height
            NPC.aiStyle = 7; //npc is the npc ai style, 7 is Pasive Ai
            NPC.defense = 25;  //the npc defense
            NPC.lifeMax = 600;// the npc life
            NPC.HitSound = SoundID.NPCHit1;  //the npc sound when is hit
            NPC.DeathSound = SoundID.NPCDeath1;  //the npc sound when he dies
            NPC.knockBackResist = 0.5f;  //the npc knockback resistance
            NPC.boss = false;
            AnimationType = NPCID.Guide;  //npc copy the guide animation
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */ //Whether or not the conditions have been met for npc town NPC to be able to move into town.
        {
            if (ExtraExplosivesSystem.BossCheckDead) return true;
            return false;

        }

        public override bool CheckActive()
        {
            Vector2 position = NPC.Center;
            CaptianIsDed = false;
            Dust dust;

            if (Main.rand.NextFloat() < 0.131579f && NPC.direction == -1)
            {
                Vector2 position1 = new Vector2(position.X + 3, position.Y - 20);

                dust = Terraria.Dust.NewDustPerfect(position1, 114, new Vector2(1f, -1f), 0, new Color(255, 125, 0), 0.7894737f);
                dust.noGravity = true;
                dust.noLight = false;
            }
            else if (Main.rand.NextFloat() < 0.131579f && NPC.direction == 1)
            {
                Vector2 position1 = new Vector2(position.X - 3, position.Y - 20);

                dust = Terraria.Dust.NewDustPerfect(position1, 114, new Vector2(-1f, -1f), 0, new Color(255, 125, 0), 0.7894737f);
                dust.noGravity = true;
                dust.noLight = false;
            }
            return base.CheckActive();
        }

        // TODO Check
        public override List<string> SetNPCNameList()//Allows you to give npc town NPC any name when it spawns
        {
            return new List<string>() { "Alfred", "Choe", "Robert", "Phineas", "Tarvish", "Unknown" };
        }

        public override bool CheckDead()
        {
            CaptianIsDed = true;

            //Create Bomb Sound
            //SoundEngine.PlaySound(SoundID.Item14, NPC.Center);

            //Create Bomb Dust
            CreateDust(NPC.Center, 100);

            //Create Bomb Damage
            ExplosionDamageByNPC(25, NPC.Center, 1000, NPC.whoAmI);

            //Create Bomb Explosion
            if (CanBreakTiles) CreateExplosion(NPC.Center, 12);

            return base.CheckDead();
        }

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
                            WorldGen.KillTile(xPosition, yPosition, false, false, false);
                            if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
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
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 6, 0f, 0.5263162f, 0, new Color(255, 0, 0), 10f)];
                        dust.noGravity = true;
                        dust.fadeIn = 2.486842f;
                    }
                    //------------

                    //---Dust 2---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 203, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------

                    //---Dust 3---
                    if (Main.rand.NextFloat() < 0.2f)
                    {
                        updatedPosition = new Vector2(position.X - 550 / 2, position.Y - 550 / 2);

                        dust = Main.dust[Terraria.Dust.NewDust(updatedPosition, 550, 550, 31, 0f, 0f, 0, new Color(255, 255, 255), 10f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    //------------
                }
            }
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)  //Allows you to define special conditions required for npc town NPC's house
        {
            return true;  //so when a house is available the npc will  spawn
        }

        public override void SetChatButtons(ref string button, ref string button2)  //Allows you to set the text for the buttons that appear on npc town NPC's chat window.
        {
            button = "Buy Explosives";   //npc defines the buy button name
            button2 = "Combine";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = ShopName;   //so when you click on buy button opens the shop
            }
            else
            {
                Main.playerInventory = true;
                Main.npcChatText = "";

                GetInstance<ExtraExplosivesSystem>().ExtraExplosivesUserInterface.SetState(new UI.ExtraExplosivesUI());
            }
        }

        public override void AddShops()
        {
            NPCShop shop = new NPCShop(Type, ShopName)
                .Add(ModContent.ItemType<BasicExplosiveItem>())
                .Add(ModContent.ItemType<SmallExplosiveItem>())
                .Add(ModContent.ItemType<MediumExplosiveItem>())
                .Add(ModContent.ItemType<LargeExplosiveItem>())
                .Add(ModContent.ItemType<TorchBombItem>())
                .Add(ModContent.ItemType<DynaglowmiteItem>())
                .Add(ModContent.ItemType<DeliquidifierItem>())
                .Add(ModContent.ItemType<HydromiteItem>())
                .Add(ModContent.ItemType<LavamiteItem>())
                .Add(ModContent.ItemType<HouseBombItem>())
                .Add(ModContent.ItemType<BunnyiteItem>())
                .Add(ModContent.ItemType<MeteoriteBusterItem>())
                .Add(ModContent.ItemType<HellavatorItem>())
                .Add(ModContent.ItemType<BombBag>())
                .Add(ModContent.ItemType<ShortFuse>())
                .Add(ModContent.ItemType<LightweightBombshells>());
            
            if (NPC.downedMechBoss1)
            {
                shop.Add(ModContent.ItemType<ArenaBuilderItem>());
                shop.Add(ModContent.ItemType<LandBridgeItem>());
            }

            if (NPC.downedPirates)
            {
                shop.Add(ModContent.ItemType<CritterBombItem>());
            }

            if (NPC.downedClown)
            {
                shop.Add(ModContent.ItemType<DaBombItem>());
            }

            if (NPC.downedBoss3)
            {
                shop.Add(ModContent.ItemType<HeavyBombItem>());
            }

            if (NPC.downedPlantBoss)
            {
                shop.Add(ModContent.ItemType<C4Item>());
            }

            if (NPC.downedMoonlord)
            {
                shop.Add(ModContent.ItemType<AtomBombItem>());
            }

            if (NPC.downedGoblins)
            {
                shop.Add(ModContent.ItemType<ReforgeBombItem>());
            }

            if (Main.hardMode)
            {
                shop.Add(ModContent.ItemType<BombBuddyItem>());
                shop.Add(ModContent.ItemType<MegaExplosiveItem>());
                shop.Add(ModContent.ItemType<GiganticExplosiveItem>());
                shop.Add(ModContent.ItemType<FlashbangItem>());
            }

            shop.Register();
        }

        public override string GetChat()       //Allows you to give npc town NPC a chat message when a player talks to it.
        {
            switch (Main.rand.Next(10)) //npc are the messages when you talk to the npc
            {
                case 0:
                    return "Psst... PSSST... how's it going?";

                case 1:
                    return "I have bombs. TONS OF BOMBS!!!";

                case 2:
                    return "Get scammed by the Tinkerer yet?";

                case 3:
                    return "My explosives are too powerful for a mortal man... why, I'd say it's impossible for you to even consider... oh, wait... you're a sprite. Nevermind, carry on!";

                case 4:
                    return "< Not Responsible for Any Casualties That May or May Not Result From These Devices >";

                case 5:
                    return "Like I, your friend... the one you worked so hard for... would sell you items that could hurt you. *scoffs* Nonsense!";

                case 6:
                    return "I really don't see why you think I shouldn't carry matches. I mean, come on, fire's a good thing, right?";

                case 7:
                    return "To all the haters: I'm not bald. I just have a short fuse.";

                case 8:
                    return "Sometimes I don't feel appreciated. After everything I've destroyed for you... couldn't I at least have a bigger house?";

                case 9:
                    return "If I were you, I'd spend your money here. You've got enough potions and and have reforged enough tools!";

                case 10:
                    return "Don't let me die... you'll regret it.";

                default:
                    return "Let me tell you a secret...";
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)//  Allows you to determine the damage and knockback of npc town NPC attack
        {
            damage = 150;  //npc damage
            knockback = 2f;   //npc knockback
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)  //Allows you to determine the cooldown between each of npc town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
        {
            cooldown = 5;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of npc town NPC's attack, and how long it takes for the projectile to actually appear
        {
            projType = Mod.Find<ModProjectile>("NPCProjectile").Type;
            attackDelay = 1;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            scale = .7f; //1f;
            base.DrawTownAttackSwing(ref item, ref itemFrame, ref itemSize, ref scale, ref offset);
        }
    }
}