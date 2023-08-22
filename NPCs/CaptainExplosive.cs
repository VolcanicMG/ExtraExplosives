using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Items.Pets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        public static bool CaptianIsDed = true;

        private const int PickPower = 50;

        public override bool Autoload(ref string name)
        {
            name = "CaptainExplosive";
            return Mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Captain Of Explosives");
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

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) //Whether or not the conditions have been met for npc town NPC to be able to move into town.
        {
            if (ExtraExplosivesWorld.BossCheckDead) return true;
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

        public override string TownNPCName()     //Allows you to give npc town NPC any name when it spawns
        {
            switch (WorldGen.genRand.Next(5))
            {
                case 0:
                    return "Alfred";
                case 1:
                    return "Choe";
                case 2:
                    return "Robert";
                case 3:
                    return "Phineas";
                case 4:
                    return "Tarvish";

                default:
                    return "Unknown";
            }
        }

        public override bool CheckDead()
        {
            CaptianIsDed = true;

            //Create Bomb Sound
            SoundEngine.PlaySound(SoundID.Item14, (int)NPC.Center.X, (int)NPC.Center.Y);

            //Create Bomb Dust
            CreateDust(NPC.Center, 100);

            //Create Bomb Damage
            ExplosionDamageEnemy(25, NPC.Center, 1000, NPC.whoAmI);

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
            button = "Buy Explosioves";   //npc defines the buy button name
            button2 = "Combine";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool openShop) //Allows you to make something happen whenever a button is clicked on npc town NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Set the shop parameter to true to open npc NPC's shop.
        {
            if (firstButton)
            {
                openShop = true;   //so when you click on buy button opens the shop
            }
            else
            {
                Main.playerInventory = true;
                Main.npcChatText = "";

                GetInstance<ExtraExplosives>().ExtraExplosivesUserInterface.SetState(new UI.ExtraExplosivesUI());
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)       //Allows you to add items to npc town NPC's shop. Add an item by setting the defaults of shop.item[nextSlot] then incrementing nextSlot.
        {
            if (NPC.downedMechBoss1)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ArenaBuilderItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<LandBridgeItem>());
                nextSlot++;
            }

            if (NPC.downedPirates)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<CritterBombItem>());
                nextSlot++;
            }

            if (NPC.downedClown)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DaBombItem>());
                nextSlot++;
            }

            if (NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<HeavyBombItem>());
                nextSlot++;
            }

            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<C4Item>());
                nextSlot++;
            }

            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<AtomBombItem>());
                nextSlot++;
            }

            if (NPC.downedGoblins)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ReforgeBombItem>());
                nextSlot++;
            }

            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<BombBuddyItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<MegaExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<GiganticExplosiveItem>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<FlashbangItem>());
                nextSlot++;
            }

            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BasicExplosiveItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<SmallExplosiveItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<MediumExplosiveItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<LargeExplosiveItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<TorchBombItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<DynaglowmiteItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<DeliquidifierItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<HydromiteItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<LavamiteItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<HouseBombItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BunnyiteItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<MeteoriteBusterItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<HellavatorItem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BombBag>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<ShortFuse>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<LightweightBombshells>());
            nextSlot++;
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

        //------------------------------------npc is an example of how to make the npc use a sward attack-------------------------------
        //public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)//Allows you to customize how npc town NPC's weapon is drawn when npc NPC is swinging it (npc NPC must have an attack type of 3). Item is the Texture2D instance of the item to be drawn (use Main.itemTexture[id of item]), itemSize is the width and height of the item's hitbox
        //{
        //	scale = 1f;
        //	item = Main.itemTexture[mod.ItemType("CustomSword")]; //npc defines the item that npc npc will use
        //	itemSize = 56;
        //}

        //public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight) //  Allows you to determine the width and height of the item npc town NPC swings when it attacks, which controls the range of npc NPC's swung weapon.
        //{
        //	itemWidth = 56;
        //	itemHeight = 56;
        //}

        //----------------------------------npc is an example of how to make the npc use a gun and a projectile ----------------------------------

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

        //public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness) //Allows you to customize how npc town NPC's weapon is drawn when npc NPC is shooting (npc NPC must have an attack type of 1). Scale is a multiplier for the item's drawing size, item is the ID of the item to be drawn, and closeness is how close the item should be drawn to the NPC.
        //{
        //	scale = .7f; //1f;
        //	item = 164; // mod.ItemType("Handgun");
        //	closeness = 0; //20S
        //}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of npc town NPC's attack, and how long it takes for the projectile to actually appear
        {
            projType = Mod.Find<ModProjectile>("NPCProjectile").Type;
            attackDelay = 1;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            scale = .7f; //1f;
            base.DrawTownAttackSwing(ref item, ref itemSize, ref scale, ref offset);
        }
    }
}