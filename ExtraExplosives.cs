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

namespace ExtraExplosives
{
	public class NewBulletBoomItem
	{								// These are for
		public int itemID;			// crafting recipe
		public string projName;		// display name, tooltip, and registering with tml
		public string displayName;

		public NewBulletBoomItem(int itemID, string projName, string displayName)
		{
			this.itemID = itemID;
			this.projName = projName;
			this.displayName = displayName;
		}
	}

	public class NewBulletBoomProjectile
	{
		public int projectileID;		// so the projectile can be shot
		public string projName;			// registering with terraria and display name

		public NewBulletBoomProjectile(int projectileID, string projName)
		{
			this.projectileID = projectileID;
			this.projName = projName;
		}
	}
	
	public class ExtraExplosives : Mod
	{
		// Modded Bullet Boom Support (Not in use but necessary)
		public static IDictionary<int, int> mapItemToItemID;
		public static bool generateForeignBulletBooms;
		public static void AddPair(int item, int id) => mapItemToItemID.Add(item, id);

		public static void NewRegister(NewBulletBoomItem item, NewBulletBoomProjectile proj)
		{
			AddNewBulletItem(item);
			AddNewBulletProj(proj);
		}
		private static void AddNewBulletItem(NewBulletBoomItem item) => _bulletBoomItems.Add(item);

		private static void AddNewBulletProj(NewBulletBoomProjectile proj) => _bulletBoomProjectiles.Add(proj);


		// This is where the info for the bulletboom generation is stored, not quite (fully) dynamic sadly
		// Item List (Note lists are 1-1 and ordered, changing order will break loading)
		static List<NewBulletBoomItem> _bulletBoomItemsClone = new List<NewBulletBoomItem>()
		{
		};

		static List<NewBulletBoomProjectile> _bulletBoomProjectilesClone = new List<NewBulletBoomProjectile>()
		{
		};


		static List<NewBulletBoomItem> _bulletBoomItems = new List<NewBulletBoomItem>() 
		{
			new NewBulletBoomItem(ItemID.MusketBall, "MusketBall", "Musket"), 
			new NewBulletBoomItem(ItemID.SilverBullet, "SilverBullet", "Silver"),
			new NewBulletBoomItem(ItemID.MeteorShot, "MeteorShot", "Meteor"),
			new NewBulletBoomItem(ItemID.CrystalBullet, "CrystalBullet","Crystal"), 
			new NewBulletBoomItem(ItemID.CursedBullet, "CursedBullet","Cursed"), 
			new NewBulletBoomItem(ItemID.ChlorophyteBullet, "ChlorophyteBullet","Chlorophyte"), 
			new NewBulletBoomItem(ItemID.HighVelocityBullet, "HighVelocityBullet","High Velocity"), 
			new NewBulletBoomItem(ItemID.IchorBullet, "IchorBullet","Ichor"), 
			new NewBulletBoomItem(ItemID.VenomBullet, "VenomBullet","Venom"), 
			new NewBulletBoomItem(ItemID.PartyBullet, "PartyBullet","Party"), 
			new NewBulletBoomItem(ItemID.NanoBullet, "NanoBullet","Nano"), 
			new NewBulletBoomItem(ItemID.ExplodingBullet, "ExplodingBullet","Exploding"), 
			new NewBulletBoomItem(ItemID.GoldenBullet, "GoldenBullet","Golden"), 
			new NewBulletBoomItem(ItemID.MoonlordBullet, "LuminiteBullet","Luminite")
		};

		// Projectile List
		static List<NewBulletBoomProjectile> _bulletBoomProjectiles = new List<NewBulletBoomProjectile>()
		{
			new NewBulletBoomProjectile(ProjectileID.Bullet, "MusketBall"),
			new NewBulletBoomProjectile(ProjectileID.Bullet, "SilverBullet"),
			new NewBulletBoomProjectile(ProjectileID.MeteorShot, "MeteorShot"),
			new NewBulletBoomProjectile(ProjectileID.CrystalBullet, "CrystalBullet"),
			new NewBulletBoomProjectile(ProjectileID.CursedBullet, "CursedBullet"),
			new NewBulletBoomProjectile(ProjectileID.ChlorophyteBullet, "ChlorophyteBullet"),
			new NewBulletBoomProjectile(ProjectileID.BulletHighVelocity, "HighVelocityBullet"),
			new NewBulletBoomProjectile(ProjectileID.IchorBullet, "IchorBullet"),
			new NewBulletBoomProjectile(ProjectileID.VenomBullet, "VenomBullet"),
			new NewBulletBoomProjectile(ProjectileID.PartyBullet, "PartyBullet"),
			new NewBulletBoomProjectile(ProjectileID.NanoBullet, "NanoBullet"),
			new NewBulletBoomProjectile(ProjectileID.ExplosiveBullet, "ExplodingBullet"),
			new NewBulletBoomProjectile(ProjectileID.GoldenBullet, "GoldenBullet"),
			new NewBulletBoomProjectile(ProjectileID.MoonlordBullet, "LuminiteBullet")
		};
		
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
			mapItemToItemID = new Dictionary<int,int>();
		}
		
		// Registers modded projectiles and items for the bullet boom
		public void RunRegistry()
		{
			for (int i = 0; i < _bulletBoomItems.Count; i++)	// loop through array
			{
				// creates projectile and registers it with tml
				BulletBoomProjectile projectile = new BulletBoomProjectile(_bulletBoomProjectiles[i].projectileID, _bulletBoomProjectiles[i].projName);
				AddProjectile(_bulletBoomProjectiles[i].projName, projectile);
				//Creates items and register it
				BulletBoomItem item = new BulletBoomItem(_bulletBoomItems[i].displayName, projectile);
				AddItem(_bulletBoomItems[i].projName, item);
				// map the item to its new id and ammo to the item 
				ExtraExplosives.AddPair(_bulletBoomItems[i].itemID, item.item.type);
			}
		}

		public override void Unload()
		{
			//wipe everything out
			mapItemToItemID.Clear();
			_bulletBoomProjectiles.Clear();
			_bulletBoomItems.Clear();
			
			base.Unload();
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

			//set the clone instances to the original on the first go
			if(_bulletBoomItems.Count > 0)
			{
				foreach(NewBulletBoomItem newBulletBoomItem in _bulletBoomItems)
				{
					_bulletBoomItemsClone.Add(newBulletBoomItem);
				}

				foreach (NewBulletBoomProjectile newBulletBoomProjectile in _bulletBoomProjectiles)
				{
					_bulletBoomProjectilesClone.Add(newBulletBoomProjectile);
				}
			}
			else
			{
				foreach (NewBulletBoomItem newBulletBoomItem in _bulletBoomItemsClone)
				{
					_bulletBoomItems.Add(newBulletBoomItem);
				}

				foreach (NewBulletBoomProjectile newBulletBoomProjectile in _bulletBoomProjectilesClone)
				{
					_bulletBoomProjectiles.Add(newBulletBoomProjectile);
				}
			}
			
			// Check config setting, then run registry
			// If config setting is enabled, warns the user since it might cause problems when handling poorly written mods
			if (generateForeignBulletBooms)
			{
				// Logger info because this feature is janky as can be
				// Use warn on first so its stands out since it will eventually cause problems
				Logger.Warn("You are using the dynamic bullet boom generation feature, this may result in insability while loading");
				Logger.Info("This feature, while stable, can be problematic with both lots of mods and mods with strange naming conventions for their items\n" +
							"If you see this, you are probably having problems loading, disabling Extra Explosives may solve them");
				ForeignModParsing.PostLoad();   // Run if config setting is set
			}
			RunRegistry();      // Always run to load standard bullet booms

			ModVersion = "v" + Version.ToString().Trim();

			using (WebClient client = new WebClient())
			{
				//Parsing the data we need from the api
				var json = client.DownloadString("http://javid.ddns.net/tModLoader/tools/latestmodversionsimple.php?modname=extraexplosives");
				json.ToString().Trim();
				//JObject o = JObject.Parse(json);
				CurrentVersion = json; //(string)o["Version"];
			}
		}
	}
}
