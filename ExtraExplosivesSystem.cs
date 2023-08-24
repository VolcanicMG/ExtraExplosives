using ExtraExplosives.Items;
using ExtraExplosives.Items.Accessories;
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
using Microsoft.Xna.Framework.Audio;
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
using ExtraExplosives;

namespace ExtraExplosives;
// TODO i created a new file to extend modsystem since that was the easiest way to get it to work
// IT is also undoubtedly the wrong way to do this and should be undone as soon as possible
public class ExtraExplosivesSystem : ModSystem
{
    //UI
    internal UserInterface ExtraExplosivesUserInterface;
    internal UserInterface ExtraExplosivesReforgeBombInterface;
    internal UserInterface CEBossInterface;
    internal UserInterface CEBossInterfaceNonOwner;

    public override void Load()
    {
        base.Load();
        //UI stuff
        ExtraExplosivesUserInterface = new UserInterface();
        ExtraExplosivesReforgeBombInterface = new UserInterface();
        CEBossInterface = new UserInterface();
        CEBossInterfaceNonOwner = new UserInterface();
    }

    public override void Unload()
    {
        base.Unload();
        ExtraExplosivesUserInterface = null;
    }

    public override void PostUpdateEverything()/* tModPorter Note: Removed. Use ModSystem.PostUpdateEverything */
    {
        /*if (boomBoxMusic)
        {
            boomBoxTimer++;
            if (boomBoxTimer > (1200 + Main.rand.Next(600)))
            {
                boomBoxMusic = false;
                boomBoxTimer = 0;
            }
        }*/

        //Still needs work and most likely reworked(MP issues)
        if (Main.LocalPlayer.dead)
        {
            if (NovaBooster.EngineSoundInstance != null && NovaBooster.EngineSoundInstance.State == SoundState.Playing)
                NovaBooster.EngineSoundInstance.Stop();
            if (NovaBooster.EndSoundInstance != null && NovaBooster.EndSoundInstance.State == SoundState.Playing)
                NovaBooster.EndSoundInstance.Stop();
        }
    }
    
    public override void UpdateUI(GameTime gameTime)/* tModPorter Note: Removed. Use ModSystem.UpdateUI */
    {
        ExtraExplosivesUserInterface?.Update(gameTime);
        CEBossInterface?.Update(gameTime);
        CEBossInterfaceNonOwner?.Update(gameTime);
        //ExtraExplosivesReforgeBombInterface?.Update(gameTime);

        /* TODO buttonInterface?.Update(gameTime);
        cookbookInterface?.Update(gameTime);*/
    }
    
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)/* tModPorter Note: Removed. Use ModSystem.ModifyInterfaceLayers */
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
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                "ExtraExplosives: CookbookButton",
                    delegate
                    {
                        /*if (Main.playerInventory)
                        {
                            buttonInterface.Draw(Main.spriteBatch, new GameTime());
                        }*/
                        return true;
                    },
                    InterfaceScaleType.UI));
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExtraExplosives: CookbookUI",
                    delegate
                    {

                        //cookbookInterface.Draw(Main.spriteBatch, new GameTime());

                        return true;

                    },
                    InterfaceScaleType.UI));
            }

            if (mouseTextIndex != -1)
            {

            }


        }
    
    public override void AddRecipes()/* tModPorter Note: Removed. Use ModSystem.AddRecipes */
    {
        Recipe recipe = Recipe.Create(ItemID.AvengerEmblem);
        recipe.AddIngredient(ModContent.ItemType<BombardierEmblem>(), 1);
        recipe.AddIngredient(ItemID.SoulofMight, 5);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddIngredient(ItemID.SoulofFright, 5);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
        base.AddRecipes();
    }
}