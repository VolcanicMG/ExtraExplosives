using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

using static ExtraExplosives.GlobalMethods;
using System;
using Terraria.ModLoader.UI.ModBrowser;
using System.Net;
using System.Data;
using ExtraExplosives.NPCs.CaptainExplosiveBoss;

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

		internal static float dustAmount;
		internal UserInterface ExtraExplosivesUserInterface;
		internal UserInterface ExtraExplosivesReforgeBombInterface;

		public static string GithubUserName => "VolcanicMG";
		public static string GithubProjectName => "ExtraExplosives";

		public static string ModVersion;
		public static string CurrentVersion;

		// Create the item to item id reference (used with cpt explosive) Needs to stay loaded
		public ExtraExplosives()
		{
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			int check = reader.ReadVarInt();
			////Don't use as of right now
			//if (reader.ReadString() == "boom") //set to a byte,
			//{
			//	if (Main.netMode == NetmodeID.Server)//set the other players to have the same properties besides the client
			//	{
			//		ModPacket myPacket = GetPacket();
			//		myPacket.Write("boom");
			//		myPacket.Send(ignoreClient: whoAmI);
			//	}
			//	else//set what you want to happen
			//	{
			//		NukeActive = true;
			//	}
			//}

			//if (reader.ReadString() == "Set")
			//{
			//	if (Main.netMode == NetmodeID.Server)
			//	{
			//		ModPacket myPacket = GetPacket();
			//		myPacket.Write("Set");
			//		myPacket.Send(ignoreClient: whoAmI);
			//	}
			//	else
			//	{
			//		NukeActivated = true;
			//	}
			//}

			//Vector2 pos = reader.ReadPackedVector2();
			//NukePos = pos;

			if (check == 1) //to make sure only one player can spawn in a nuke at a time in MP
			{
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket myPacket = GetPacket();
					myPacket.WriteVarInt(1);
					myPacket.Send(ignoreClient: whoAmI);
				}
				else
				{
					NukeActivated = true;
				}
			}

			if (check == 2) //sets NukeHit to false for all players
			{
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket myPacket = GetPacket();
					myPacket.WriteVarInt(2);
					myPacket.Send(ignoreClient: whoAmI);
				}
				else
				{
					NukeHit = false;
				}
			}
		}

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
			//ExtraExplosivesReforgeBombInterface?.Update(gameTime);
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
			}
		}

		public override void Load()
		{
			Logger.InfoFormat($"{0} Extra Explosives logger", Name);

			ExtraExplosivesUserInterface = new UserInterface();
			ExtraExplosivesReforgeBombInterface = new UserInterface();

			TriggerExplosion = RegisterHotKey("Explode", "Mouse2");
			TriggerUIReforge = RegisterHotKey("Open Reforge Bomb UI", "P");

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
				Filters.Scene["BurningScreen"] = new Filter(new ScreenShaderData(burningScreenFilter, "BurningScreen"), EffectPriority.Medium);	// Shouldnt override more important shaders
				Filters.Scene["BurningScreen"].Load();
			}

			ModVersion = "v" + Version.ToString().Trim();

			//Goes out and grabs the version that the mod browser has
			using (WebClient client = new WebClient())
			{
				if (CheckForInternetConnection())
				{
					//Parsing the data we need from the api
					var json = client.DownloadString("http://javid.ddns.net/tModLoader/tools/latestmodversionsimple.php?modname=extraexplosives");
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
