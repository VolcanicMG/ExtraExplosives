using ExtraExplosives.Buffs;
using ExtraExplosives.NPCs.CaptainExplosiveBoss;
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
using System.Diagnostics;
using System.Drawing;
using ExtraExplosives.Items.Accessories;
using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Projectiles;
using ExtraExplosives.UI;
using ExtraExplosives.UI.AnarchistCookbookUI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Color = System.Drawing.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Terraria.DataStructures;

namespace ExtraExplosives
{
	public class ExtraExplosivesPlayer : ModPlayer
	{
		// Bombard Class stuff	(may need to make setting these on a per client basis)
		
		/// <summary>
		/// Additional explosive damage
		/// </summary>
		public float DamageBonus { get; set; }
		
		/// <summary>
		/// Additional explosive knockback
		/// </summary>
		public float KnockbackBonus { get; set; }
		
		/// <summary>
		/// Additional explosive radius
		/// </summary>
		public int RadiusBonus { get; set; }
		
		/// <summary>
		/// Explosive damage multiplier
		/// </summary>
		public float DamageMulti { get; set; }
		
		/// <summary>
		/// Explosive knockback multiplier
		/// </summary>
		public float KnockbackMulti { get; set; }
		
		/// <summary>
		/// Explosion radius multiplier
		/// </summary>
		public float RadiusMulti { get; set; }
		
		/// <summary>
		/// Explosive crit chance bonus
		/// </summary>
		public int ExplosiveCrit { get; set; }

		public int reforgeUIActive = 0;
		public bool detonate;

		//buffs
		public bool BombBuddy;

		public Vector2 BuddyPos;

		public bool RadiatedDebuff;

		private int tickCheck = 1;

		//public static bool NukeActive;
		//public static Vector2 NukePos;
		//public static bool NukeHit;

		public List<Terraria.ModLoader.PlayerLayer> playerLayers = new List<Terraria.ModLoader.PlayerLayer>();

		public bool reforge = false;
		public static bool reforgePub;

		//Anarchist Cookbook Stuff
		/// <summary>
		/// Blast Shielding equipped
		/// </summary>
		public bool BlastShielding { get; set; }
		/// <summary>
		/// Blast Shielding active in cookbook
		/// </summary>
		public bool BlastShieldingActive { get; set; }
		
		/// <summary>
		/// Bomb bag equipped
		/// </summary>
		public bool BombBag { get; set; }
		/// <summary>
		/// Bomb bag active in cookbook
		/// </summary>
		public bool BombBagActive { get; set; }
		
		/// <summary>
		/// Crossed wires equipped
		/// </summary>
		public bool CrossedWires { get; set; }
		/// <summary>
		/// Crossed wires active in cookbook
		/// </summary>
		public bool CrossedWiresActive { get; set; }
		
		/// <summary>
		/// Glowing compound equipped
		/// </summary>
		public bool GlowingCompound { get; set; }
		/// <summary>
		/// Glowing compound active in cookbook
		/// </summary>
		public bool GlowingCompoundActive { get; set; }
		
		/// <summary>
		/// Lightweight bombshells equipped
		/// </summary>
		public bool LightweightBombshells { get; set; }
		/// <summary>
		/// Lightweight bombshells active in cookbook
		/// </summary>
		public bool LightweightBombshellsActive { get; set; }
		/// <summary>
		/// Lightweight bombshells cookbook velocity
		/// </summary>
		public float LightweightBombshellVelocity { get; set; } = 1;
		
		/// <summary>
		/// Mystery bomb equipped
		/// </summary>
		public bool MysteryBomb { get; set; }
		/// <summary>
		/// Mystery bomb active in cookbook
		/// </summary>
		public bool MysteryBombActive { get; set; }
		
		/// <summary>
		/// Random fuel equipped
		/// </summary>
		public bool RandomFuel { get; set; }
		/// <summary>
		/// Random fuel active in cookbook
		/// </summary>
		public bool RandomFuelActive { get; set; }
		/// <summary>
		/// Depricated
		/// </summary>
		public bool RandomFuelOnFire { get; set; }
		/// <summary>
		/// Depricated
		/// </summary>
		public bool RandomFuelFrostburn { get; set; }
		/// <summary>
		/// Depricated
		/// </summary>
		public bool RandomFuelConfused { get; set; }
		
		/// <summary>
		/// Reactive Plating equipped
		/// </summary>
		public bool ReactivePlating { get; set; }
		/// <summary>
		/// Reactive Plating active in cookbook
		/// </summary>
		public bool ReactivePlatingActive { get; set; }
		
		/// <summary>
		/// Short Fuse equipped
		/// </summary>
		public bool ShortFuse { get; set; }
		/// <summary>
		/// Short Fuse active in cookbook
		/// </summary>
		public bool ShortFuseActive { get; set; }
		/// <summary>
		/// Short Fuse cookbook time
		/// </summary>
		public float ShortFuseTime { get; set; } = 1;
		
		/// <summary>
		/// Sticky gunpowder equipped
		/// </summary>
		public bool StickyGunpowder { get; set; }
		/// <summary>
		/// Sticky gunpowder active in cookbook
		/// </summary>
		public bool StickyGunpowderActive { get; set; }
		
		/// <summary>
		/// Anarchist Cookbook equipped
		/// </summary>
		public bool AnarchistCookbook { get; set; }

		// Chaos Bomb
		/// <summary>
		/// Alien Explosive equipped
		/// </summary>
		public bool AlienExplosive { get; set; }
		
		/// <summary>
		/// Bombshroom equipped
		/// </summary>
		public bool Bombshroom { get; set; }
		
		/// <summary>
		/// Chaos Bomb equipped
		/// </summary>
		public bool ChaosBomb { get; set; }
		
		/// <summary>
		/// Eclectic Bomb equipped
		/// </summary>
		public bool EclecticBomb { get; set; }
		
		/// <summary>
		/// Lihzahrd Fuzeset equipped
		/// </summary>
		public bool LihzahrdFuzeset { get; set; }
		
		/// <summary>
		/// Supernatural Bomb equipped
		/// </summary>
		public bool SupernaturalBomb { get; set; }
		
		/// <summary>
		/// Wyrd Bomb equipped
		/// </summary>
		public bool WyrdBomb { get; set; }

		/// <summary>
		/// Lihzahrd Fuzeset fuse time
		/// </summary>
		public int? FuseTime { get; set; }   // Later use with Anarchist Cookbook UI

		/// <summary>
		/// Anti gravity equipped
		/// </summary>
		public bool AntiGravity { get; set; }   // Later use with Anarchist Cookbook UI

		// Grenadier Class stuff (Bombard whatever)
		/// <summary>
		/// Bombardier emblem equipped
		/// </summary>
		public bool BombardEmblem { get; set; }
		
		/// <summary>
		/// Bomb Cloak equipped
		/// </summary>
		public bool BombCloak { get; set; }
		
		/// <summary>
		/// Certificate of Demolition equipped
		/// </summary>
		public bool CertificateOfDemolition { get; set; }
		
		/// <summary>
		/// Bombers Hat equipped
		/// </summary>
		public bool BombersHat { get; set; }
		
		/// <summary>
		/// Flesh Blasting Caps equipped
		/// </summary>
		public bool FleshBlastingCaps { get; set; }
		
		/// <summary>
		/// Bombards Laurels equipped
		/// </summary>
		public bool BombardsLaurels { get; set; }
		
		/// <summary>
		/// Bombers Pouch equipped
		/// </summary>
		public bool BombersPouch { get; set; }
		
		/// <summary>
		/// Ravenous Bomb equipped
		/// </summary>
		public bool RavenousBomb { get; set; }

		internal bool InventoryOpen { get; set; }
		private bool InvFlag { get; set; }

		public static bool BossCheckDead;

		public Vector2 cookbBookPos;

		// Nova Wing Draw Data
		internal int wingFrame = 0;
		internal int wingFrameCounter = 0;
		internal bool boosting = false;
		internal int boostTimer = 30;
		internal bool novaBooster = false;
		internal int novaBoostRechargeDelay = 0;
		
		public override void ResetEffects()
		{
			RadiatedDebuff = false;
			BombBuddy = false;
			novaBooster = false;

			// Anarchist Cookbook Resets
			BlastShielding = false;
			BombBag = false;
			CrossedWires = false;
			GlowingCompound = false;
			LightweightBombshells = false;
			MysteryBomb = false;
			RandomFuel = false;
			ReactivePlating = false;
			ShortFuse = false;
			StickyGunpowder = false;
			AnarchistCookbook = false;

			// Generic class stuff
			BombardEmblem = false;
			BombCloak = false;
			CertificateOfDemolition = false;
			RavenousBomb = false;

			// Chaos bomb
			AlienExplosive = false;
			Bombshroom = false;
			ChaosBomb = false;
			EclecticBomb = false;
			LihzahrdFuzeset = false;
			SupernaturalBomb = false;
			WyrdBomb = false;
			AntiGravity = false;

			// Class Bonus and Multiplier Set and Reset
			DamageBonus = 0;
			DamageMulti = 1;
			KnockbackBonus = 0;
			KnockbackMulti = 1;
			RadiusBonus = 0;
			RadiusMulti = 1;
			ExplosiveCrit = 0;
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

			//cookbook stuff
			if (ExtraExplosives.ToggleCookbookUI.JustPressed && Main.LocalPlayer.EE().AnarchistCookbook)
			{
				if (Main.playerInventory)
				{
					Main.playerInventory = false;
					ButtonUI.Visible = false;
				}

				if (CookbookUI.Visible)
				{
					ButtonUI.Visible = true;
				}

				CookbookUI.Visible = !CookbookUI.Visible;
			}

			if (novaBoostRechargeDelay > 0)
			{
				novaBoostRechargeDelay--; // if it needs to recharge, let it recharge
				if (player.wingTime > 0 || boosting)    // Can only boost if there is 'fuel' (wingTime) left in the novaBooster	(ignore wingtime if already boosting)
				{
					switch (novaBoostRechargeDelay)
					{
						case 299:
						case 250:
						case 200:
							Projectile projectile = Projectile.NewProjectileDirect(
								new Vector2(player.Center.X - 256, player.Center.Y + 128), Vector2.Zero,
								ModContent.ProjectileType<NovaBoosterProjectile>(), 200, 20, player.whoAmI);
							projectile.timeLeft = 0;
							projectile.friendly = true;
							projectile.Kill();
							break;
						default:
							Vector2 dustPos = new Vector2(player.Center.X - 100, player.Center.Y - 100);
							Dust dust = Dust.NewDustDirect(dustPos, 200, 200, 57);
							dust.noGravity = true;
							dust.fadeIn = 1.2f;
							break;
					}
				}
			}
			else if (Main.LocalPlayer.EE().novaBooster &&
				ExtraExplosives.TriggerBoost.JustPressed &&
				(int)Main.LocalPlayer.velocity.Y != 0)
			{
				novaBoostRechargeDelay = 300;
				player.velocity *= 3;
				boosting = true;
			}
		}

		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage,
			ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			Projectile projectile = new Projectile();
			projectile.CloneDefaults(damageSource.SourceProjectileType);
			if (projectile.type == ModContent.ProjectileType<BombCloakProjectile>()) return false;  // If the bomb cloak caused the explosion, do nothing

			if (projectile.aiStyle == 16)
			{
				//Main.NewText(damage);
				if (BlastShielding) // Blast Shielding (working)
				{
					return false;
				}
				else if (ReactivePlating) damage = (int)(damage * 0.9);
				//Main.NewText(damage);
			}

			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
		}

		public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			if (BombCloak)
			{
				Projectile.NewProjectileDirect(player.position, Vector2.Zero, ProjectileType<BombCloakProjectile>(), (int)((100 + DamageBonus) * DamageMulti), 10, player.whoAmI).timeLeft = 1;
			}
			base.Hurt(pvp, quiet, damage, hitDirection, crit);
		}

		public override void PostUpdate()
		{
			
			if (Main.netMode != NetmodeID.Server && Filters.Scene["Bang"].IsActive() && !player.HasBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>())) //destroy the filter once the buff has ended
			{
				Filters.Scene["Bang"].Deactivate();
			}

			if (Main.netMode != NetmodeID.Server && Filters.Scene["BigBang"].IsActive() && ExtraExplosives.NukeHit == false) //destroy the filter once the buff has ended
			{
				Filters.Scene["BigBang"].Deactivate();
			}

			if (ExtraExplosives.CheckUIBoss == 1 && tickCheck == 1)
			{
				Player playerCheck = Main.player[Main.myPlayer];
				if (playerCheck.whoAmI == 0)
				{
					GetInstance<ExtraExplosives>().CEBossInterface.SetState(new UI.CEBossUI()); //get the UI
				}
				else if (playerCheck.whoAmI == 255)
				{

				}
				else
				{
					GetInstance<ExtraExplosives>().CEBossInterfaceNonOwner.SetState(new UI.CEBossUINonOwner()); //get the UI
				}

				tickCheck = 2;

				//Main.NewText(player.whoAmI);
			}

			//disable the looping
			if (ExtraExplosives.CheckUIBoss != 1)
			{
				tickCheck = 1;
			}
		}

		public override void PostUpdateMiscEffects()    // Put updates to damage, knockback, crit, and radius here
		{
			if (CrossedWires)
			{
				DamageMulti += 0.15f;
				ExplosiveCrit += 10;
			}
			if (ReactivePlating) DamageBonus += .1f;
			if (BombardEmblem) DamageMulti += 0.15f;
			if (BombersHat) RadiusMulti += 0.3f;
			if (CertificateOfDemolition) RadiusMulti += 0.5f;

		}

		private SpriteEffects effect;
		private int offset;
		public static readonly PlayerLayer Wings = new PlayerLayer("ExtraExplosives", "Wings", PlayerLayer.Wings,
			delegate (PlayerDrawInfo info)
			{
				//Main.NewText($"{GetInstance<ExtraExplosivesPlayer>().wingFrameCounter} {GetInstance<ExtraExplosivesPlayer>().wingFrame}");


				Player drawPlayer = info.drawPlayer;

				Mod mod = ModLoader.GetMod("ExtraExplosives");
				ExtraExplosivesPlayer mp = drawPlayer.EE();
				Texture2D Booster = GetTexture("ExtraExplosives/Items/Accessories/NovaBoosterLow_Wings");
				Texture2D BoosterHigh = GetTexture("ExtraExplosives/Items/Accessories/NovaBoosterHigh_Wings");

				if (Main.LocalPlayer.direction < 0)
				{
					mp.offset = 24;
					mp.effect = SpriteEffects.FlipHorizontally;
				}
				else
				{
					mp.offset = -24;
					mp.effect = SpriteEffects.None;
				}

				mp.wingFrameCounter++;  // This deals with the current frame
				if (mp.wingFrameCounter > 8)
				{
					mp.wingFrameCounter = 0;
					mp.wingFrame++;
					if (mp.wingFrame > 2 && mp.player.velocity.Y > 0)
					{
						mp.wingFrame = 0;
					}
					else if (mp.wingFrame > 5 && mp.player.velocity.Y < 0)
					{
						mp.wingFrame = 0;
					}
				}

				int drawX = (int)(info.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(info.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
				
				if (mp.boosting)
				{
					mp.boostTimer--;
					if (mp.boostTimer <= 0)
					{
						mp.boostTimer = 120;
						mp.boosting = false;
					}
				}
				DrawData data = new DrawData((mp.boosting ? BoosterHigh : Booster), new Vector2(drawX + mp.offset, drawY), new Rectangle(0, (mp.player.velocity.Y == 0 ? 6 * 44 : 44 * mp.wingFrame), 46, 44), new Microsoft.Xna.Framework.Color(255, 255, 255), 0f, new Vector2(Booster.Width / 2f, Booster.Height / 4f - 60), 1f, mp.effect, 0);
				Main.playerDrawData.Add(data);
			});
		public override void ModifyDrawLayers(List<Terraria.ModLoader.PlayerLayer> layers) //Make the players invisable
		{
			if (novaBooster && !player.dead)
			{
				//layers.RemoveAt(5);
				layers.Insert(5, Wings);
			}
		}

		public override void ModifyScreenPosition()
		{
			if (ExtraExplosives.NukeActive == true)
			{
				//Main.NewText("Nuke active");
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
			//StaticMethods.BuildDoNotHomeList();
			InventoryOpen = false;
			//NukeActive = false;
			//ExtraExplosives.NukeActivated = false;
			ExtraExplosives.NukeHit = false;
			//player.ResetEffects();
			player.ResetEffects();
			Main.screenPosition = player.Center;

			if(ExtraExplosives.CurrentVersion.Equals(""))
			{
				Main.NewText($"[c/FF0000:They're is no Internet connection.]");
			}
			else if(!ExtraExplosives.ModVersion.Equals(ExtraExplosives.CurrentVersion))
			{
				Main.NewText($"[c/AB40FF:The Extra Explosives Mod had an update available.]");
				Main.NewText($"[c/AB40FF:Current Version Installed: {ExtraExplosives.ModVersion}]");
				Main.NewText($"[c/AB40FF:Mod Browser Version: {ExtraExplosives.CurrentVersion}]");
				Main.NewText($"[c/AB40FF:You can find the latest version in the TML mod browser.]");
			}

			//Main.NewText($"Version: {ExtraExplosives.ModVersion}");
			//Main.NewText($"Current Version: |{currentVersion}|");
		}

		public override void SetControls() //when the nuke is active set the player to not build or use items
		{
			if (ExtraExplosives.NukeActive == true)
			{
				// Removed so i dont have to close the game each time i test the nuke
				/*player.controlUseItem = false;
				player.noBuilding = true;
				player.controlUseTile = false;
				if (Main.playerInventory)
				{
					player.ToggleInv();
				}
				player.controlInv = false;
				player.controlMap = false;*/
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



		public override TagCompound Save()
		{
			return new TagCompound  // save tag, leave whats here add more as needed
			{
				// Main Cookbook Integration DO NOT REMOVE
				[nameof(BlastShieldingActive)] = BlastShieldingActive,
				[nameof(BombBagActive)] = BombBagActive,
				[nameof(CrossedWiresActive)] = CrossedWiresActive,
				[nameof(GlowingCompoundActive)] = GlowingCompoundActive,
				[nameof(LightweightBombshellsActive)] = LightweightBombshellsActive,
				[nameof(MysteryBombActive)] = MysteryBombActive,
				[nameof(RandomFuelActive)] = RandomFuelActive,
				[nameof(ReactivePlatingActive)] = ReactivePlatingActive,
				[nameof(ShortFuseActive)] = ShortFuseActive,
				[nameof(StickyGunpowderActive)] = StickyGunpowderActive,

				// Lesser tags
				[nameof(LightweightBombshellVelocity)] = LightweightBombshellVelocity,
				[nameof(ShortFuseTime)] = ShortFuseTime,

				//Boss
				[nameof(BossCheckDead)] = BossCheckDead
			};
		}

		public override void Load(TagCompound tag)
		{
			// Main tag loading
			BlastShieldingActive = tag.GetBool(nameof(BlastShieldingActive));
			BombBagActive = tag.GetBool(nameof(BombBagActive));
			CrossedWiresActive = tag.GetBool(nameof(CrossedWiresActive));
			GlowingCompoundActive = tag.GetBool(nameof(GlowingCompoundActive));
			LightweightBombshellsActive = tag.GetBool(nameof(LightweightBombshellsActive));
			MysteryBombActive = tag.GetBool(nameof(MysteryBombActive));
			RandomFuelActive = tag.GetBool(nameof(RandomFuelActive));
			ReactivePlatingActive = tag.GetBool(nameof(ReactivePlatingActive));
			ShortFuseActive = tag.GetBool(nameof(ShortFuseActive));
			StickyGunpowderActive = tag.GetBool(nameof(StickyGunpowderActive));

			// Lesser tag loading
			LightweightBombshellVelocity = tag.GetFloat(nameof(LightweightBombshellVelocity));
			ShortFuseTime = tag.GetFloat(nameof(ShortFuseTime));

			//Boss tag loading
			BossCheckDead = tag.GetBool(nameof(BossCheckDead));
		}
	}
}