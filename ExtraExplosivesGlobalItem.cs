using ExtraExplosives.Items;
using ExtraExplosives.Items.Explosives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace ExtraExplosives
{
    public class ExtraExplosivesGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances { get; } = true;

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

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
                            
                            Projectile proj = Projectile.NewProjectileDirect(player.GetSource_FromThis(), position,
                                new Vector2(position.X + 0.1f, position.Y + 0.1f), item.shoot, damage,
                                knockback, item.playerIndexTheItemIsReservedFor);
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
            return base.Shoot(item, player, source, position, velocity, type,  damage,  knockback);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine Info = tooltips.FirstOrDefault(t => t.Mod == "Terraria");
            TooltipLine Disclaimer = tooltips.LastOrDefault(t => t.Mod == "Terraria");

            if (Info != null && ExtraExplosives._tooltipWhitelist.Contains<int>(item.type))
            {
                //Info.text += "[c/AB40FF: (Bombard Item)]";
                var Dis = new TooltipLine(Mod, "ItemName", "(Bombard Item)");
                Dis.OverrideColor = Color.Purple;
                //tooltips.Add(Dis);
                tooltips.Insert(1, Dis);

            }
            else if (Disclaimer != null && ExtraExplosives.disclaimerTooltip.Contains<int>(item.type))
            {
                var Dis = new TooltipLine(Mod, "", "(Doesn't work with Extra Explosive trinkets)");
                Dis.OverrideColor = Color.Red;
                tooltips.Add(Dis);
            }

        }

        public override void AddRecipes()
        {

            Recipe recipe = Recipe.Create(ItemID.Dynamite);
            recipe.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 3);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            base.AddRecipes();

            Recipe recipe2 = Recipe.Create(ItemID.Bomb);
            recipe2.AddIngredient(ModContent.ItemType<BasicExplosiveItem>(), 1);
            recipe2.AddIngredient(ItemID.Grenade, 1);
            recipe2.AddIngredient(ItemID.Gel, 5);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();
            base.AddRecipes();

        }
    }
}