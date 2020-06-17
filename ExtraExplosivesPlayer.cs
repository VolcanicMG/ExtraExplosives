using ExtraExplosives.Buffs;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using Terraria;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives
{
	public class ExtraExplosivesPlayer : ModPlayer
	{
		public int reforgeUIActive = 0;
		public bool detonate;

		//buffs
		public bool BombBuddy;

		public Vector2 BuddyPos;

		public bool RadiatedDebuff;

		//public static bool NukeActive;
		//public static Vector2 NukePos;
		//public static bool NukeHit;

		public List<Terraria.ModLoader.PlayerLayer> playerLayers = new List<Terraria.ModLoader.PlayerLayer>();

		public bool reforge = false;
		public static bool reforgePub;

		public override void ResetEffects()
		{
			RadiatedDebuff = false;
			BombBuddy = false;
		}

		public override void UpdateDead()
		{
			RadiatedDebuff = false;
		}

		public override void UpdateBadLifeRegen()
		{
			if (RadiatedDebuff)
			{
				if (player.lifeRegen > 0)
				{
					player.lifeRegen = 0;
				}
				player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
				player.lifeRegen -= 30;
			}
		}

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			reforgePub = reforge;
			//Player player = Main.player[Main.myPlayer];

			//Main.NewText(ExtraExplosives.TriggerUIReforge.GetAssignedKeys(InputMode.Keyboard)[0].ToString());
			if (reforge == true)
			{
				reforge = false;
			}

			if (ExtraExplosives.TriggerExplosion.JustReleased)
			{
				//ExtraExplosives.detonate = true;
				detonate = true;
				//Main.NewText("Detonate", (byte)30, (byte)255, (byte)10, false);
			}
			else
			{
				//ExtraExplosives.detonate = false;
				detonate = false;
			}

			if (ExtraExplosives.TriggerUIReforge.JustPressed) //check to see if the button was just pressed
			{
				reforgeUIActive++;

				if (reforgeUIActive == 5)
				{
					reforgeUIActive = 1;
				}
			}

			if (reforgeUIActive == 1) //check to see if the reforge bomb key was pressed
			{
				GetInstance<ExtraExplosives>().ExtraExplosivesReforgeBombInterface.SetState(new UI.ExtraExplosivesReforgeBombUI());
				reforgeUIActive++;
			}
			if (reforgeUIActive == 3)
			{
				GetInstance<ExtraExplosives>().ExtraExplosivesReforgeBombInterface.SetState(null);
				reforgeUIActive = 4;
			}
		}

		public override void PostUpdate()
		{
			//Player player = Main.player[Main.myPlayer];
			if (Main.netMode != NetmodeID.Server && Filters.Scene["Bang"].IsActive() && !player.HasBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>())) //destroy the filter once the buff has ended
			{
				Filters.Scene["Bang"].Deactivate();
			}

			if (Main.netMode != NetmodeID.Server && Filters.Scene["BigBang"].IsActive() && ExtraExplosives.NukeHit == false) //destroy the filter once the buff has ended
			{
				Filters.Scene["BigBang"].Deactivate();
			}
		}

		public override void ModifyDrawLayers(List<Terraria.ModLoader.PlayerLayer> layers) //Make the players invisable
		{
			//if (NukeActive == true)
			//{
			//	foreach (var layer in layers)
			//	{
			//		layer.visible = false;
			//	}
			//}
		}

		public override void ModifyScreenPosition()
		{
			if (ExtraExplosives.NukeActive == true)
			{
				//follow the projectiles
				Main.screenPosition = new Vector2(ExtraExplosives.NukePos.X - (Main.screenWidth / 2), ExtraExplosives.NukePos.Y - (Main.screenHeight / 2));
			}
			if (ExtraExplosives.NukeHit == true)
			{
				//shake
				Main.screenPosition += Utils.RandomVector2(Main.rand, Main.rand.Next(-100, 100), Main.rand.Next(-100, 100));

				//add lighting
				Lighting.AddLight(ExtraExplosives.NukePos, new Vector3(255f, 255f, 255f));
				Lighting.maxX = 400;
				Lighting.maxY = 400;
				//NukeHit = false;
			}
		}

		public override void OnEnterWorld(Player player) //might need to set to new netmode in case it dosnt work in MP
		{
			//NukeActive = false;
			//ExtraExplosives.NukeActivated = false;
			ExtraExplosives.NukeHit = false;
			//player.ResetEffects();
			player.ResetEffects();
			Main.screenPosition = player.Center;

			if(ExtraExplosives.CurrentVersion.Equals(""))
			{
				Main.NewText($"[c/FF0000:Mod browser is offline or there is no Internet connection.]");
			}
			else if(!ExtraExplosives.ModVersion.Equals(ExtraExplosives.CurrentVersion))
			{
				Main.NewText($"[c/AB40FF:The Extra Explosives Mod needs updated.]");
				Main.NewText($"[c/AB40FF:Current Version Installed: {ExtraExplosives.ModVersion}]");
				Main.NewText($"[c/AB40FF:Mod Browser Version: {ExtraExplosives.CurrentVersion}]");
				Main.NewText($"[c/AB40FF:You can find the latests version in the TML mod browser.]");
			}

			//Main.NewText($"Version: {ExtraExplosives.ModVersion}");
			//Main.NewText($"Current Version: |{currentVersion}|");
		}

		public override void SetControls() //when the nuke is active set the player to not build or use items
		{
			if (ExtraExplosives.NukeActive == true)
			{
				player.controlUseItem = false;
				player.noBuilding = true;
				player.controlUseTile = false;
				if (Main.playerInventory)
				{
					player.ToggleInv();
				}
				player.controlInv = false;
				player.controlMap = false;
			}
		}

		public override void clientClone(ModPlayer clientClone)
		{
			ExtraExplosivesPlayer clone = clientClone as ExtraExplosivesPlayer;
		}

		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
		}

		public override void SendClientChanges(ModPlayer clientPlayer)
		{
		}
	}
}