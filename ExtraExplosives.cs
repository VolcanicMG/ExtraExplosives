using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Accessories.BombardierClassAccessories;
using ExtraExplosives.Items.Accessories.ChaosBomb;
using ExtraExplosives.Items.Armors.Asteroid;
using ExtraExplosives.Items.Armors.CorruptedAnarchy;
using ExtraExplosives.Items.Armors.CrimsonAnarchy;
using ExtraExplosives.Items.Armors.DungeonBombard;
using ExtraExplosives.Items.Armors.Hazard;
using ExtraExplosives.Items.Armors.HeavyAutomated;
using ExtraExplosives.Items.Armors.Lizhard;
using ExtraExplosives.Items.Armors.Meltbomber;
using ExtraExplosives.Items.Armors.Nova;
using ExtraExplosives.Items.Armors.SpaceDemolisher;
using ExtraExplosives.Items.Armors.TunnelRat;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.NPCs.CaptainExplosiveBoss;
using ExtraExplosives.NPCs.CaptainExplosiveBoss.BossProjectiles;
using ExtraExplosives.Projectiles;
using ExtraExplosives.Projectiles.Weapons.DutchmansBlaster;
using ExtraExplosives.Projectiles.Weapons.NovaBuster;
using ExtraExplosives.Projectiles.Weapons.TrashCannon;
using ExtraExplosives.UI.AnarchistCookbookUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExtraExplosives
{
    public class ExtraExplosives : Mod
    {
        //Hotkeys
        internal static ModKeybind TriggerExplosion;
        internal static ModKeybind TriggerUIReforge;
        internal static ModKeybind ToggleCookbookUI;
        internal static ModKeybind TriggerBoost;
        internal static ModKeybind TriggerNovaBomb;
        internal static ModKeybind TriggerLizhard;

        //nuke
        public static bool NukeActivated;
        public static bool NukeActive;
        public static Vector2 NukePos;
        public static bool NukeHit;

        //boss
        public static int bossDropDynamite;

        //dust
        internal static float dustAmount;

        //Mod instance
        public static Mod Instance;

        //Boss checks
        public static int CheckUIBoss = 0;
        public static bool CheckBossBreak;
        public static bool firstTick;
        public static float bossDirection;
        public static bool removeUIElements;

        //Cookbook ui
        internal UserInterface cookbookInterface;
        internal UserInterface buttonInterface;
        internal ButtonUI ButtonUI;
        internal CookbookUI CookbookUI;

        //config
        internal static ExtraExplosivesConfig EEConfig;

        //Arrays and Lists
        internal static HashSet<int> avoidList;
        internal static int[] _doNotDuplicate;
        internal static HashSet<int> _tooltipWhitelist;
        internal static HashSet<int> disclaimerTooltip;

        // Create the item to item id reference (used with cpt explosive) Needs to stay loaded
        public ExtraExplosives()
        {

        }

        public override void Unload()
        {
            base.Unload();
            //ExtraExplosivesUserInterface = null;
            Instance = null;
        }

        internal enum EEMessageTypes : byte
        {
            checkNukeActivated,
            nukeDeactivate,
            checkNukeHit,
            nukeHit,
            nukeNotActive,
            nukeActive,
            checkBossUIYes,
            checkBossUINo,
            BossCheckDynamite,
            bossCheckRocket,
            boolBossCheck,
            checkBossActive,
            setBossInactive,
            bossMovment,
            removeUI
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            EEMessageTypes msgType = (EEMessageTypes)reader.ReadByte();

            switch (msgType)
            {
                //Nuke stuff ------------------------
                case EEMessageTypes.checkNukeActivated:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket myPacket = GetPacket();
                        myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkNukeActivated);
                        myPacket.Send(ignoreClient: whoAmI);
                    }
                    else
                    {
                        NukeActivated = true;
                    }
                    break;

                case EEMessageTypes.nukeDeactivate:

                    NukeActivated = false;
                    break;

                case EEMessageTypes.checkNukeHit:

                    NukeHit = false;
                    break;

                case EEMessageTypes.nukeHit:

                    NukeHit = true;
                    break;

                case EEMessageTypes.nukeNotActive:

                    NukeActive = false;
                    break;

                case EEMessageTypes.nukeActive:

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket myPacket = GetPacket();
                        myPacket.Write((byte)ExtraExplosives.EEMessageTypes.nukeActive);
                        myPacket.Send(ignoreClient: whoAmI);
                    }
                    else
                    {
                        NukeActive = true;
                    }
                    break;
                //Nuke stuff ------------------------

                case EEMessageTypes.BossCheckDynamite:

                    int randomNumber = reader.Read7BitEncodedInt();

                    bossDropDynamite = randomNumber;
                    break;

                case EEMessageTypes.bossCheckRocket:

                    int randomNumber2 = reader.Read7BitEncodedInt();

                    bossDropDynamite = randomNumber2;
                    break;

                case EEMessageTypes.bossMovment:

                    float randomFloat = reader.ReadSingle();

                    bossDirection = randomFloat;
                    break;

                case EEMessageTypes.checkBossUIYes:

                    CheckUIBoss = 2;
                    CheckBossBreak = true;


                    break;

                case EEMessageTypes.checkBossUINo:

                    CheckUIBoss = 2;
                    CheckBossBreak = false;


                    break;

                case EEMessageTypes.checkBossActive:

                    CheckUIBoss = 1;
                    break;

                case EEMessageTypes.setBossInactive:

                    CheckUIBoss = 3;
                    break;

                case EEMessageTypes.removeUI:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket myPacket = GetPacket();
                        myPacket.Write((byte)ExtraExplosives.EEMessageTypes.removeUI);
                        myPacket.Send(ignoreClient: whoAmI);
                    }
                    else
                    {
                        removeUIElements = true;
                    }

                    //removeUIElements = true;
                    break;
            }

        }

        public override void PostSetupContent()
        {
            /*Mod censusMod = ModLoader.GetMod("Census");
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");

            if (censusMod != null)
            {
                // Here I am using Chat Tags to make my condition even more interesting.
                // If you localize your mod, pass in a localized string instead of just English.
                // Additional lines for additional town npc that your mod adds
                // Simpler example:
                censusMod.Call("TownNPCCondition", Find<ModNPC>("CaptainExplosive").Type, $"Kill King Slime or Eye Of Cthulhu, then you can buy the[i:{ModContent.ItemType<Unhinged_Letter>()}] from the demolitionist");
            }

            if (bossChecklist != null)
            {
                // AddBoss, bossname, order or value in terms of vanilla bosses, inline method for retrieving downed value.
                bossChecklist.Call("AddBoss", 6, ModContent.NPCType<CaptainExplosiveBoss>(), this, "Captain Explosive", (Func<bool>)(() => ExtraExplosivesSystem.BossCheckDead), ModContent.ItemType<Unhinged_Letter>(), ModContent.ItemType<BombHat>(), ModContent.ItemType<CaptainExplosiveTreasureBag>(), $"Kill King Slime or Eye Of Cthulhu, then you can buy the[i:{ModContent.ItemType<Unhinged_Letter>()}] from the demolitionist");
            }*/

            _tooltipWhitelist = new HashSet<int> //Whitelist for the (Bombard Item) tag at the end of bombard items.
			{
                //armors
                ModContent.ItemType<AsteroidMiner_B>(),
                ModContent.ItemType<AsteroidMiner_B_O>(),
                ModContent.ItemType<AsteroidMiner_H>(),
                ModContent.ItemType<AsteroidMiner_H_O>(),
                ModContent.ItemType<AsteroidMiner_L>(),
                ModContent.ItemType<AsteroidMiner_L_O>(),

                ModContent.ItemType<Nova_B>(),
                ModContent.ItemType<Nova_H>(),
                ModContent.ItemType<Nova_L>(),

                ModContent.ItemType<CorruptedAnarchy_B>(),
                ModContent.ItemType<CorruptedAnarchy_H>(),
                ModContent.ItemType<CorruptedAnarchy_L>(),

                ModContent.ItemType<CrimsonAnarchy_B>(),
                ModContent.ItemType<CrimsonAnarchy_H>(),
                ModContent.ItemType<CrimsonAnarchy_L>(),

                ModContent.ItemType<DungeonBombard_B>(),
                ModContent.ItemType<DungeonBombard_H>(),
                ModContent.ItemType<DungeonBombard_L>(),
                ModContent.ItemType<DungeonBombard_B>(),

                ModContent.ItemType<Hazard_B>(),
                ModContent.ItemType<Hazard_B_T>(),
                ModContent.ItemType<Hazard_H>(),
                ModContent.ItemType<Hazard_H_T>(),
                ModContent.ItemType<Hazard_L>(),
                ModContent.ItemType<Hazard_L_T>(),

                ModContent.ItemType<HeavyAutomated_B>(),
                ModContent.ItemType<HeavyAutomated_H>(),
                ModContent.ItemType<HeavyAutomated_L>(),

                ModContent.ItemType<Lizhard_B>(),
                ModContent.ItemType<Lizhard_H>(),
                ModContent.ItemType<Lizhard_L>(),

                ModContent.ItemType<Meltbomber_B>(),
                ModContent.ItemType<Meltbomber_H>(),
                ModContent.ItemType<Meltbomber_L>(),

                ModContent.ItemType<SpaceDemolisher_B>(),
                ModContent.ItemType<SpaceDemolisher_B_C>(),
                ModContent.ItemType<SpaceDemolisher_H>(),
                ModContent.ItemType<SpaceDemolisher_H_C>(),
                ModContent.ItemType<SpaceDemolisher_L>(),
                ModContent.ItemType<SpaceDemolisher_L_C>(),

                ModContent.ItemType<Tunnelrat_B>(),
                ModContent.ItemType<Tunnelrat_H>(),
                ModContent.ItemType<Tunnelrat_L>(),

                //Accessories
                //ModContent.ItemType<NovaBooster>(),
                ModContent.ItemType<BombardierEmblem>(),
                ModContent.ItemType<BombardsLaurels>(),
                ModContent.ItemType<BombardsPouch>(),
                ModContent.ItemType<BombCloak>(),
                ModContent.ItemType<BombersCap>(),
                ModContent.ItemType<CertificateOfDemolition>(),
                ModContent.ItemType<FleshyBlastingCaps>(),
                ModContent.ItemType<RavenousBomb>(),

                ModContent.ItemType<AlienExplosive>(),
                ModContent.ItemType<Bombshroom>(),
                ModContent.ItemType<ChaosBomb>(),
                ModContent.ItemType<EclecticBomb>(),
                ModContent.ItemType<LihzahrdFuzeset>(),
                ModContent.ItemType<SupernaturalBomb>(),
                ModContent.ItemType<WyrdBomb>(),

                ModContent.ItemType<AnarchistCookbook>(),
                ModContent.ItemType<BlastShielding>(),
                ModContent.ItemType<BombBag>(),
                ModContent.ItemType<CrossedWires>(),
                ModContent.ItemType<GlowingCompound>(),
                ModContent.ItemType<HandyNotes>(),
                ModContent.ItemType<LightweightBombshells>(),
                ModContent.ItemType<MysteryBomb>(),
                ModContent.ItemType<RandomFuel>(),
                ModContent.ItemType<RandomNotes>(),
                ModContent.ItemType<ReactivePlating>(),
                ModContent.ItemType<ResourcefulNotes>(),
                ModContent.ItemType<SafetyNotes>(),
                ModContent.ItemType<ShortFuse>(),
                ModContent.ItemType<StickyGunpowder>(),
                ModContent.ItemType<UtilityNotes>()


            };


            _doNotDuplicate = new int[]
            {
                ModContent.ProjectileType<HouseBombProjectile>(),
                ModContent.ProjectileType<TheLevelerProjectile>(),
                ModContent.ProjectileType<ArenaBuilderProjectile>(),
                ModContent.ProjectileType<ReforgeBombProjectile>(),
                ModContent.ProjectileType<HellavatorProjectile>(),
                ModContent.ProjectileType<RainboomProjectile>(),
                ModContent.ProjectileType<BulletBoomProjectile>(),
                ModContent.ProjectileType<AtomBombProjectile>()
            };

            avoidList = new HashSet<int>
            {
                        ModContent.ProjectileType<BossArmorBreakBombProjectile>(),
                        ModContent.ProjectileType<BossChillBombProjectile>(),
                        ModContent.ProjectileType<BossDazedBombProjectile>(),
                        ModContent.ProjectileType<BossFireBombProjectile>(),
                        ModContent.ProjectileType<BossGooBombProjectile>(),
                        ModContent.ProjectileType<ExplosionDamageProjectileEnemy>(),
                        ProjectileID.BombSkeletronPrime,
                        ProjectileID.DD2GoblinBomb,
                        ProjectileID.HappyBomb,
                        ProjectileID.SantaBombs,
                        ProjectileID.SmokeBomb,
                        ModContent.ProjectileType<HouseBombProjectile>(),
                        ModContent.ProjectileType<CritterBombProjectile>(),
                        ModContent.ProjectileType<BunnyiteProjectile>(),
                        ModContent.ProjectileType<BreakenTheBankenProjectile>(),
                        ModContent.ProjectileType<BreakenTheBankenChildProjectile>(),
                        ModContent.ProjectileType<DaBombProjectile>(),
                        ModContent.ProjectileType<ArenaBuilderProjectile>(),
                        ModContent.ProjectileType<ReforgeBombProjectile>(),
                        ModContent.ProjectileType<TornadoBombProjectile>(),
                        ModContent.ProjectileType<HellavatorProjectile>(),
						//ModContent.ProjectileType<InfinityBombProjectile>(),
						ModContent.ProjectileType<LandBridgeProjectile>(),
                        ModContent.ProjectileType<BoomBoxProjectile>(),
                        ModContent.ProjectileType<FlashbangProjectile>(),
                        ProjectileID.RocketI,
                        ProjectileID.RocketII,
                        ProjectileID.RocketIII,
                        ProjectileID.RocketIV,
                        ProjectileID.RocketSnowmanI,
                        ProjectileID.RocketSnowmanII,
                        ProjectileID.RocketSnowmanIII,
                        ProjectileID.RocketSnowmanIV,
                        ProjectileID.ProximityMineI,
                        ProjectileID.ProximityMineII,
                        ProjectileID.ProximityMineIII,
                        ProjectileID.ProximityMineIV,
                        ProjectileID.Grenade, //Might come back later -----------
						ProjectileID.GrenadeI,
                        ProjectileID.GrenadeII,
                        ProjectileID.GrenadeIII,
                        ProjectileID.GrenadeIV,
                        ProjectileID.BouncyGrenade,
                        ProjectileID.PartyGirlGrenade,
                        ProjectileID.StickyGrenade,//----------------------------
						ModContent.ProjectileType<DynaglowmiteProjectile>(),
                        ModContent.ProjectileType<CleanBombProjectile>(),
                        ModContent.ProjectileType<CleanBombExplosionProjectile>(),
                        ModContent.ProjectileType<RainboomProjectile>(),
                        ModContent.ProjectileType<TrollBombProjectile>(),
                        ModContent.ProjectileType<TorchBombProjectile>(),
                        ModContent.ProjectileType<HydromiteProjectile>(),
                        ModContent.ProjectileType<LavamiteProjectile>(),
                        ModContent.ProjectileType<DeliquidifierProjectile>(),
                        ModContent.ProjectileType<BulletBoomProjectile>(),
                        ModContent.ProjectileType<NPCProjectile>(),
                        ProjectileID.Beenade,
                        ProjectileID.Explosives,
                        ProjectileID.DD2GoblinBomb,
                        ModContent.ProjectileType<DutchmansBlasterProjectile>(),
                        ModContent.ProjectileType<NovaBusterProjectile>(),
                        ModContent.ProjectileType<HealBombProjectile>(),
                        ModContent.ProjectileType<BiomeCleanerProjectile>(),
                        ModContent.ProjectileType<HotPotatoProjectile>(),
                        ModContent.ProjectileType<TrashCannonProjectile>(),
						//ModContent.ProjectileType<WallBombProjectile>()
			};

            disclaimerTooltip = new HashSet<int>
            {
                ModContent.ItemType<HouseBombItem>(),
                ModContent.ItemType<CritterBombItem>(),
                ModContent.ItemType<BunnyiteItem>(),
                ModContent.ItemType<BreakenTheBankenItem>(),
                ModContent.ItemType<DaBombItem>(),
                ModContent.ItemType<ArenaBuilderItem>(),
                ModContent.ItemType<ReforgeBombItem>(),
                ModContent.ItemType<TornadoBombItem>(),
                ModContent.ItemType<HellavatorItem>(),
                ModContent.ItemType<InfinityBombItem>(),
                ModContent.ItemType<LandBridgeItem>(),
                ModContent.ItemType<BoomBoxItem>(),
                ModContent.ItemType<FlashbangItem>(),
                ModContent.ItemType<DynaglowmiteItem>(),
                ModContent.ItemType<CleanBombItem>(),
                ModContent.ItemType<RainboomItem>(),
                ModContent.ItemType<TrollBombItem>(),
                ModContent.ItemType<TorchBombItem>(),
                ModContent.ItemType<HydromiteItem>(),
                ModContent.ItemType<LavamiteItem>(),
                ModContent.ItemType<DeliquidifierItem>(),
                ModContent.ItemType<BulletBoomItem>(),
                ModContent.ItemType<HealBomb>(),
                ModContent.ItemType<BiomeCleanerItem>(),
				//ModContent.ItemType<WallBombItem>()
			};

            base.PostSetupContent();
        }





        public override void Load()
        {
            Instance = this;

            Logger.InfoFormat($"{0} Extra Explosives logger", Name);

            //UI stuff
            cookbookInterface = new UserInterface();

            //Hotkey stuff
            TriggerExplosion = KeybindLoader.RegisterKeybind(this, "Explode", "Mouse2");
            TriggerUIReforge = KeybindLoader.RegisterKeybind(this, "Open Reforge Bomb UI", "P");
            ToggleCookbookUI = KeybindLoader.RegisterKeybind(this, "UIToggle", "\\");
            TriggerBoost = KeybindLoader.RegisterKeybind(this, "TriggerBoost", "S");
            TriggerNovaBomb = KeybindLoader.RegisterKeybind(this, "TriggerNovaSetBonus", "X");
            TriggerLizhard = KeybindLoader.RegisterKeybind(this, "TriggerMissleFireLizhard", "Z");

            if (!Main.dedServ)
            {
                buttonInterface = new UserInterface();

                ButtonUI = new ButtonUI();
                ButtonUI.Activate();

                buttonInterface.SetState(ButtonUI);
            }

            //shaders
            if (Main.netMode != NetmodeID.Server)
            {
                //load in the shaders
                Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("ExtraExplosives/Effects/Flashbang", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["Flashbang"] = new Filter(new ScreenShaderData(screenRef, "Flashbang"), EffectPriority.VeryHigh); //float4 name

                Ref<Effect> screenRef2 = new Ref<Effect>(ModContent.Request<Effect>("ExtraExplosives/Effects/NukeShader", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["BigBang"] = new Filter(new ScreenShaderData(screenRef2, "BigBang"), EffectPriority.VeryHigh); //float4 name

                // Hot Potato Shader
                Ref<Effect> burningScreenFilter = new Ref<Effect>(ModContent.Request<Effect>("ExtraExplosives/Effects/HPScreenFilter", AssetRequestMode.ImmediateLoad).Value);
                Filters.Scene["BurningScreen"] = new Filter(new ScreenShaderData(burningScreenFilter, "BurningScreen"), EffectPriority.Medium); // Shouldnt override more important shaders

                // Bomb shader
                // TODO: -> tModLoader - Shaders registered as MiscShaderData now require float4 uShaderSpecificData; as a parameter
                //  unsure if this is accurate but something to keep in mind 
                Ref<Effect> bombShader = new Ref<Effect>(ModContent.Request<Effect>("ExtraExplosives/Effects/bombshader", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["bombshader"] = new MiscShaderData(bombShader, "BombShader");

                //shockwave
                Ref<Effect> screenRef3 = new Ref<Effect>(ModContent.Request<Effect>("ExtraExplosives/Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef3, "Shockwave"), EffectPriority.High);
            }

            //Health bar
            Mod yabhb = null; //ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                 ModContent.Request<Texture2D>("NPCs/CaptainExplosiveBoss/healtbar_left").Value,
                 ModContent.Request<Texture2D>("NPCs/CaptainExplosiveBoss/healtbar_frame").Value,
                 ModContent.Request<Texture2D>("NPCs/CaptainExplosiveBoss/healtbar_right").Value,
                 ModContent.Request<Texture2D>("NPCs/CaptainExplosiveBoss/healtbar_fill").Value);
                //yabhb.Call("hbSetMidBarOffset", 20, 12);
                //yabhb.Call("hbSetBossHeadCentre", 22, 34);
                yabhb.Call("hbFinishSingle", ModContent.NPCType<CaptainExplosiveBoss>());
            }
        }
    }
}
