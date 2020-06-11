using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Pets;
using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using static ExtraExplosives.GlobalMethods;
using Lang = On.Terraria.Lang;


namespace ExtraExplosives
{

	class NewBulletBoomItem
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

	class NewBulletBoomProjectile
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
		// This is where the info for the bulletboom generation is stored, not quite (fully) dynamic sadly
		// TODO make the generation of this array dynamic, it should be possible
		private NewBulletBoomItem[] _bulletBoomItems = new NewBulletBoomItem[]
		{
			new NewBulletBoomItem(ItemID.MusketBall, "MusketBall", "Musket Ball"), 
			new NewBulletBoomItem(ItemID.MeteorShot, "MeteorShot", "Meteor Shot"),
			new NewBulletBoomItem(ItemID.CrystalBullet, "CrystalBullet","Crystal Bullet"), 
			new NewBulletBoomItem(ItemID.CursedBullet, "CursedBullet","Cursed Bullet"), 
			new NewBulletBoomItem(ItemID.ChlorophyteBullet, "ChlorophyteBullet","Chlorophyte Bullet"), 
			new NewBulletBoomItem(ItemID.HighVelocityBullet, "HighVelocityBullet","High Velocity Bullet"), 
			new NewBulletBoomItem(ItemID.IchorBullet, "IchorBullet","Ichor Bullet"), 
			new NewBulletBoomItem(ItemID.VenomBullet, "VenomBullet","Venom Bullet"), 
			new NewBulletBoomItem(ItemID.PartyBullet, "PartyBullet","Party Bullet"), 
			new NewBulletBoomItem(ItemID.NanoBullet, "NanoBullet","Nano Bullet"), 
			new NewBulletBoomItem(ItemID.ExplodingBullet, "ExplodingBullet","Exploding Bullet"), 
			new NewBulletBoomItem(ItemID.GoldenBullet, "GoldenBullet","Golden Bullet"), 
			new NewBulletBoomItem(ItemID.MoonlordBullet, "LuminiteBullet","Luminite Bullet")
		};
		private NewBulletBoomProjectile[] _bulletBoomProjectiles = new NewBulletBoomProjectile[]
		{
			new NewBulletBoomProjectile(ProjectileID.Bullet, "MusketBall"), 
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

		public static IDictionary<int, int> mapItemToItemID;	// dictionary used to find the ammo item id for each bulletboom
		
		public ExtraExplosives()
		{

		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
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

			if (reader.ReadVarInt() == 1) //to make sure only one player can spawn in a nuke at a time in MP
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

			SetupListsOfUnbreakableTiles();

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

			Logger.InfoFormat("{0} Extra Explosives logger", Name);
			
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
			}
			
			// Code which adds Bullet Boom stuff
			// Note, this is the most dynamic i am currently able to make it
			// Adding modded ammo should be as easy as adding entries to the above arraylists tho
			mapItemToItemID = new Dictionary<int,int>();	// create the dictionary to map ids
			for (int i = 0; i < _bulletBoomItems.Length; i++)	// loop through array
			{
				// creates projectile and registers it with tml
				BulletBoomProjectile projectile = new BulletBoomProjectile(_bulletBoomProjectiles[i].projectileID, _bulletBoomProjectiles[i].projName);
				AddProjectile(_bulletBoomProjectiles[i].projName, projectile);
				//Creates items and register it
				BulletBoomItem item = new BulletBoomItem(_bulletBoomItems[i].displayName, projectile);
				AddItem(_bulletBoomItems[i].projName, item);
				// map the item to its new id and ammo to the item 
				mapItemToItemID.Add(_bulletBoomItems[i].itemID, item.item.type);
			}
		}
	}
}
