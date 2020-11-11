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
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace ExtraExplosives
{
    public class ExtraExplosivesGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances { get; } = true;

        private Item instancedItem;
        private bool defaultConsume;

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            instancedItem = item;
            defaultConsume = item.consumable;
        }

        public void ConsumeOnUse()
        {

        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type,
            ref int damage, ref float knockBack)
        {
            if (item.type == ModContent.ItemType<NukeItem>()) return true;
            Projectile projectile = new Projectile();
            projectile.CloneDefaults(item.shoot);
            bool explosive = projectile.aiStyle == 16;

            item.consumable = defaultConsume;    // Needed to revert the item so it consumes anything
            if (explosive)    // Anarchist Cookbook stuff
            {
                ExtraExplosivesPlayer mp = player.GetModPlayer<ExtraExplosivesPlayer>();
                if (mp.CrossedWires ||    // Crit chance and damage increase
                    mp.BombBag ||            // Throw 2 bombs for the price of 1
                    mp.MysteryBomb ||        // No consume chance
                    mp.ReactivePlating ||
                    mp.BombersPouch)    // Damage Increase
                {
                    if (mp.CrossedWires)    // probably doesnt work, will require il editing
                    {
                        item.crit = (int)(item.crit * 1.25f);
                        damage = (int)(damage * 1.25f);
                    }

                    if (mp.MysteryBomb)    // Mystery Bomb (working)
                    {
                        if (Main.rand.Next(5) == 0)
                        {
                            item.consumable = false;
                        }
                    }

                    if (mp.ReactivePlating)
                    {
                        damage = (int)(damage * 1.25f);    // Probably doesnt work, will probably require il editing
                    }

                    if (mp.BombBag &&
                        Array.IndexOf(ExtraExplosives._doNotDuplicate, item.shoot) == -1)    // Bomb bag (working)
                    {                // Some bones shouldnt be duplicated, this does that, for list of bombs check AddRecipes()
                        if (Main.rand.NextBool())
                        {
                            Projectile proj = Projectile.NewProjectileDirect(position,
                                new Vector2(speedX + 0.1f, speedY + 0.1f), item.shoot, damage,
                                knockBack, item.owner);
                            proj.position.X += 5;
                            proj.position.Y += 5;
                        }
                    }

                    if (mp.BombersPouch)
                    {
                        if (Main.rand.Next(10) < 3)
                        {
                            item.consumable = false;
                        }
                    }

                }
            }
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine Info = tooltips.FirstOrDefault(t => t.mod == "Terraria");
            TooltipLine Disclaimer = tooltips.LastOrDefault(t => t.mod == "Terraria");

            if (Info != null && ExtraExplosives._tooltipWhitelist.Contains<int>(item.type))
            {
                //Info.text += "[c/AB40FF: (Bombard Item)]";
                var Dis = new TooltipLine(mod, "ItemName", "(Bombard Item)");
                Dis.overrideColor = Color.Purple;
                //tooltips.Add(Dis);
                tooltips.Insert(1, Dis);

            }
            else if(Disclaimer != null && ExtraExplosives.disclaimerTooltip.Contains<int>(item.type))
            {
                var Dis = new TooltipLine(mod, "", "(Doesn't work with Extra Explosive trinkets)");
                Dis.overrideColor = Color.Red;
                tooltips.Add(Dis);
            }
            
        }

        public override void AddRecipes()
        {

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 3);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Dynamite);
            recipe.AddRecipe();
            base.AddRecipes();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 1);
            recipe2.AddIngredient(ItemID.Grenade, 1);
            recipe2.AddIngredient(ItemID.Gel, 5);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(ItemID.Bomb);
            recipe2.AddRecipe();
            base.AddRecipes();

        }
    }
}