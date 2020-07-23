using System;
using ExtraExplosives.Items;
using ExtraExplosives.Items.Accessories.AnarchistCookbook;
using ExtraExplosives.Items.Explosives;
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
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
        
        private static int cooldown = 0;

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            instancedItem = item;
            defaultConsume = item.consumable;
        }

        public void ConsumeOnUse()
        {
            
        }

        private int[] _doNotDuplicate;
        
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
                        damage = (int) (damage * 1.25f);
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
                        damage = (int) (damage * 1.25f);    // Probably doesnt work, will probably require il editing
                    }
                    
                    if (mp.BombBag &&
                        Array.IndexOf(_doNotDuplicate, item.shoot) == -1)    // Bomb bag (working)
                    {                // Some bones shouldnt be duplicated, this does that, for list of bombs check AddRecipes()
                        if(Main.rand.NextBool())
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
            
            _doNotDuplicate = new int[]    // Added here because the compile order is annoying, and i hate it
            {
                ModContent.ProjectileType<HouseBombProjectile>(),
                ModContent.ProjectileType<TheLevelerProjectile>(),
                ModContent.ProjectileType<ArenaBuilderProjectile>(),
                ModContent.ProjectileType<ReforgeBombProjectile>(),
                ModContent.ProjectileType<HellavatorProjectile>(),
                ModContent.ProjectileType<RainboomProjectile>(),
                ModContent.ProjectileType<BulletBoomProjectile>(),
                ModContent.ProjectileType<AtomBombProjectile>()
            };
        }
    }
}