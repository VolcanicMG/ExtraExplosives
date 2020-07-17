using ExtraExplosives.NPCs.CaptainExplosiveBoss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using ExtraExplosives.UI.AnarchistCookbookUI;


namespace ExtraExplosives
{
	public class ExtraExplosives : Mod
	{
		//move the first 4 over to player????
		internal static ModHotKey TriggerExplosion;

		internal static ModHotKey TriggerUIReforge;

		public static bool NukeActivated;
		public static bool NukeActive;
		public static Vector2 NukePos;
		public static bool NukeHit;

		public static int bossDropDynamite; 

		internal static float dustAmount;
		internal UserInterface ExtraExplosivesUserInterface;
		internal UserInterface ExtraExplosivesReforgeBombInterface;
		internal UserInterface CEBossInterface;
		internal UserInterface CEBossInterfaceNonOwner;

		public static Mod Instance;

		public static int CheckUIBoss = 0;
		public static bool CheckBossBreak;
		public static bool firstTick;
		public static float bossDirection;
		public static bool removeUIElements;

		public static string GithubUserName => "VolcanicMG";
		public static string GithubProjectName => "ExtraExplosives";

		public static string ModVersion;
		public static string CurrentVersion = "";

		private UserInterface cookbookInterface;
		private UserInterface buttonInterface;
		internal ButtonUI ButtonUI;
		internal CookbookUI CookbookUI;

		internal static ExtraExplosivesConfig EEConfig;

		// Create the item to item id reference (used with cpt explosive) Needs to stay loaded
		public ExtraExplosives()
		{

		}

		public override void Unload()
		{
			base.Unload();
			ExtraExplosivesUserInterface = null;
			ModVersion = null;
			Instance = null;
		}

		internal enum EEMessageTypes : byte
		{
			checkNukeActive,
			checkNukeHit,
			checkBossUIYes,
			checkBossUINo,
			BossCheckDynamite,
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
				case EEMessageTypes.checkNukeActive:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(1);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	NukeActivated = true;
					//}
					NukeActivated = true;
					break;

				case EEMessageTypes.checkNukeHit:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(2);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	NukeHit = false;
					//}
					NukeHit = false;
					break;

				case EEMessageTypes.BossCheckDynamite:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(check);
					//	myPacket.Write(boolBossCheckDynamite);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else 
					//{
					//	bossDropDynamite = check;
					//}
					int randomNumber = reader.ReadVarInt();

					bossDropDynamite = randomNumber;
					break;

				case EEMessageTypes.bossMovment:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(check);
					//	myPacket.Write(boolBossCheckDynamite);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else 
					//{
					//	bossDropDynamite = check;
					//}
					float randomFloat = reader.ReadSingle();

					bossDirection = randomFloat;
					break;

				case EEMessageTypes.checkBossUIYes:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkBossUIYes);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	CheckUIBoss = 2;
					//	CheckBossBreak = true;
					//}
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(3);
					//	myPacket.Write(false);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	CheckUIBoss = 2;
					//	CheckBossBreak = false;
					//}

					CheckUIBoss = 2;
					CheckBossBreak = true;


					break;

				case EEMessageTypes.checkBossUINo:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.Write((byte)ExtraExplosives.EEMessageTypes.checkBossUINo);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	CheckUIBoss = 2;
					//	CheckBossBreak = false;
					//}
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(3);
					//	myPacket.Write(false);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	CheckUIBoss = 2;
					//	CheckBossBreak = false;
					//}

					CheckUIBoss = 2;
					CheckBossBreak = false;


					break;

				case EEMessageTypes.checkBossActive:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(4);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	CheckUIBoss = 1;
					//}
					CheckUIBoss = 1;
					break;

				case EEMessageTypes.setBossInactive:
					//if (Main.netMode == NetmodeID.Server)
					//{
					//	ModPacket myPacket = GetPacket();
					//	myPacket.WriteVarInt(5);
					//	myPacket.Send(ignoreClient: whoAmI);
					//}
					//else
					//{
					//	CheckUIBoss = 3;
					//}
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

		//public override void MidUpdateInvasionNet()
		//{
		//	if (CheckUIBoss == 2 && !firstTick)
		//	{
		//		if (Main.netMode == NetmodeID.MultiplayerClient)
		//		{
		//			ModPacket myPacket = GetPacket();
		//			//myPacket.WriteVarInt(3);
		//			//myPacket.Write(false);
		//			myPacket.Write((byte)0);
		//			myPacket.Send();
		//		}
		//		firstTick = true;
		//	}
		//	base.MidUpdateInvasionNet();

		//}

		public override void PostSetupContent()
		{
			Mod censusMod = ModLoader.GetMod("Census");
			if (censusMod != null)
			{
				// Here I am using Chat Tags to make my condition even more interesting.
				// If you localize your mod, pass in a localized string instead of just English.
				// Additional lines for additional town npc that your mod adds
				// Simpler example:
				censusMod.Call("TownNPCCondition", NPCType("CaptainExplosive"), "Kill King Slime"); //Change later for the boss
			}

			base.PostSetupContent();
		}

		public override void UpdateUI(GameTime gameTime)
		{
			ExtraExplosivesUserInterface?.Update(gameTime);
			CEBossInterface?.Update(gameTime);
			CEBossInterfaceNonOwner?.Update(gameTime);
			//ExtraExplosivesReforgeBombInterface?.Update(gameTime);
			if (CookbookUI.Visible)
			{
				ButtonUI.Visible = false;
			}
			else if (ButtonUI.Visible)
			{
				//Main.playerInventory = true;
				CookbookUI.Visible = false;
			}

			buttonInterface?.Update(gameTime);
			cookbookInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

			int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

			if (inventoryIndex != -1)
			{
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"ExtraExplosives: UI",
					delegate
					{
						// If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
						ExtraExplosivesUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"ExtraExplosives: ReforgeBombUI",
					delegate
					{
						// If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
						ExtraExplosivesReforgeBombInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"ExtraExplosives: CEBossUI",
					delegate
					{
						// If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
						CEBossInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"ExtraExplosives: CEBossUINonOwner",
					delegate
					{
						// If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
						CEBossInterfaceNonOwner.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
				"ExtraExplosives: CookbookButton",
					delegate
					{
						if (ButtonUI.Visible && Main.playerInventory)
						{
							buttonInterface.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI));
			}

			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"ExtraExplosives: CookbookUI",
					delegate
					{
						if (CookbookUI.Visible && !Main.playerInventory)
						{
							cookbookInterface.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI));
			}


		}

		public override void Load()
		{
			Instance = this;

			Logger.InfoFormat($"{0} Extra Explosives logger", Name);

			//UI stuff
			ExtraExplosivesUserInterface = new UserInterface();
			ExtraExplosivesReforgeBombInterface = new UserInterface();
			CEBossInterface = new UserInterface();
			CEBossInterfaceNonOwner = new UserInterface();

			//Hotkey stuff
			TriggerExplosion = RegisterHotKey("Explode", "Mouse2");
			TriggerUIReforge = RegisterHotKey("Open Reforge Bomb UI", "P");
			ToggleCookbookUI = RegisterHotKey("UIToggle", "\\");
			TriggerBoost = RegisterHotKey("TriggerBoost", "S");

			if (!Main.dedServ)
			{
				cookbookInterface = new UserInterface();
				buttonInterface = new UserInterface();

				ButtonUI = new ButtonUI();
				ButtonUI.Activate();

				CookbookUI = new CookbookUI();
				CookbookUI.Deactivate();

				cookbookInterface.SetState(CookbookUI);
				buttonInterface.SetState(ButtonUI);
			}

			//shaders
			if (Main.netMode != NetmodeID.Server)
			{
				//load in the shaders
				Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/Shader")); // The path to the compiled shader file.
				Filters.Scene["Bang"] = new Filter(new ScreenShaderData(screenRef, "Bang"), EffectPriority.VeryHigh); //float4 name
				Filters.Scene["Bang"].Load();

				Ref<Effect> screenRef2 = new Ref<Effect>(GetEffect("Effects/NukeShader")); // The path to the compiled shader file.
				Filters.Scene["BigBang"] = new Filter(new ScreenShaderData(screenRef2, "BigBang"), EffectPriority.VeryHigh); //float4 name
				Filters.Scene["BigBang"].Load();

				// Shader stuff sent in this pull cuz i didnt want to delete it, ignore for now
				Ref<Effect> burningScreenFilter = new Ref<Effect>(GetEffect("Effects/HPScreenFilter"));
				Filters.Scene["BurningScreen"] = new Filter(new ScreenShaderData(burningScreenFilter, "BurningScreen"), EffectPriority.Medium); // Shouldnt override more important shaders
				Filters.Scene["BurningScreen"].Load();
				
				//Bomb shader
				Ref<Effect> bombShader = new Ref<Effect>(GetEffect("Effects/bombshader"));
				GameShaders.Misc["bombshader"] = new MiscShaderData(bombShader, "BombShader");
			}

			ModVersion = "v" + Version.ToString().Trim();

			//Goes out and grabs the version that the mod browser has
			using (WebClient client = new WebClient())
			{
				if (CheckForInternetConnection())
				{
					//Parsing the data we need from the api
					var json = client.DownloadString("https://raw.githubusercontent.com/VolcanicMG/ExtraExplosives/master/Version.TXT");
					json.ToString().Trim();
					CurrentVersion = json;
					client.Dispose();
				}
			}

			Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
			if (yabhb != null)
			{
				yabhb.Call("hbStart");
				yabhb.Call("hbSetTexture",
				 GetTexture("NPCs/CaptainExplosiveBoss/healtbar_left"),
				 GetTexture("NPCs/CaptainExplosiveBoss/healtbar_frame"),
				 GetTexture("NPCs/CaptainExplosiveBoss/healtbar_right"),
				 GetTexture("NPCs/CaptainExplosiveBoss/healtbar_fill"));
				//yabhb.Call("hbSetMidBarOffset", 20, 12);
				//yabhb.Call("hbSetBossHeadCentre", 22, 34);
				yabhb.Call("hbFinishSingle", ModContent.NPCType<CaptainExplosiveBoss>());
			}
		}

		//so if the Internet is out the client won't crash on loading
		public static bool CheckForInternetConnection()
		{
			try
			{
				using (var client = new WebClient())
				using (client.OpenRead("http://google.com/generate_204"))
					return true;
			}
			catch
			{
				return false;
			}
		}

	}
}
