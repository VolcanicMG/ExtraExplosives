using ExtraExplosives.Buffs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.Projectiles;

namespace ExtraExplosives
{
	public class ExtraExplosivesPlayer : ModPlayer
	{
		public int reforgeUIActive = 0;
		public bool detonate;

		public bool BombBuddy;
		public Vector2 BuddyPos;

		public static bool NukeActive;
		public static Vector2 NukePos;
		public static bool NukeHit;

		public List<Terraria.ModLoader.PlayerLayer> playerLayers = new List<Terraria.ModLoader.PlayerLayer>();

		public bool reforge = false;
		public static bool reforgePub;

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			reforgePub = reforge;
			//Player player = Main.player[Main.myPlayer];

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
			Player player = Main.player[Main.myPlayer];
			if (Main.netMode != NetmodeID.Server && Filters.Scene["Bang"].IsActive() && !player.HasBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>())) //destroy the filter once the buff has ended
			{
				Filters.Scene["Bang"].Deactivate();

			}
		
			if (Main.netMode != NetmodeID.Server && Filters.Scene["BigBang"].IsActive() && NukeHit == false) //destroy the filter once the buff has ended
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
			if (NukeActive == true)
			{
				//follow the projectiles
				Main.screenPosition = new Vector2(NukePos.X - (Main.screenWidth / 2), NukePos.Y - (Main.screenHeight / 2));

			}
			if (NukeHit == true)
			{
				//shake
				Main.screenPosition += Utils.RandomVector2(Main.rand, Main.rand.Next(-100, 100), Main.rand.Next(-100, 100));

				//NukeHit = false;
			}
		}

		public override void OnEnterWorld(Player player) //might need to set to new netmode in case it dosnt work in MP
		{
			NukeActive = false;
			ExtraExplosives.NukeActivated = false;
			NukeHit = false;
			//player.ResetEffects();
		}

		public override void SetControls() //when the nuke is active set the player to not build or use items
		{
			if (NukeActive == true)
			{
				player.controlUseItem = false;
				player.noBuilding = true;
				player.controlUseTile = false;
				if (Main.playerInventory)
				{
					player.ToggleInv();
				}
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