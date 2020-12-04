using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class HellfireBattery : ExplosiveWeapon
    {
        private int fireSpeed = 6;
        private int mode = 0;
        private string firemode = "Spread";
        protected override string SoundLocation { get; } = "";

        public static bool homing;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Battery");
            Tooltip.SetDefault("'The rocket minigun's older bigger brother'\n" +
                "Right click to swap between 3 different modes");
        }

        public override void SafeSetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = fireSpeed;
            item.useTime = fireSpeed;
            item.useAmmo = AmmoID.Rocket;
            item.width = 78;
            item.crit = 35;
            item.height = 42;
            item.shoot = 134;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Hellfire");
            item.channel = true;
            item.damage = 250;
            item.shootSpeed = 13f;
            item.noMelee = true;
            item.value = Item.buyPrice(10, 1, 0, 50);
            item.knockBack = 4f;
            item.rare = 11;
            item.ranged = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
            //if (stats != null)
            //{
            //	string[] split = stats.text.Split(' ');
            //	string damageValue = split.First();
            //	string damageWord = split.Last();
            //	stats.text = damageValue + " explosive " + damageWord;

            //}

            var fireModeUseTip = new TooltipLine(mod, "Multiplier", $"Fire Mode: {firemode}");
            fireModeUseTip.overrideColor = Color.Tan;
            tooltips.Add(fireModeUseTip);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 56f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            if (mode == 0) //spread
            {
                //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                //speedX = perturbedSpeed.X;
                //speedY = perturbedSpeed.Y;
                homing = false;
                Projectile.NewProjectile(new Vector2(position.X, position.Y - 20), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockBack, player.whoAmI);
                Projectile.NewProjectile(new Vector2(position.X, position.Y - 10), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockBack, player.whoAmI);
                Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)), type, (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockBack, player.whoAmI);

            }
            else if (mode == 1) //precision
            {
                Projectile.NewProjectile(new Vector2(position.X, position.Y - 10), new Vector2(speedX, speedY), ModContent.ProjectileType<FollowRocketProjectile>(), (int)((damage + 3000 + player.EE().DamageBonus) * player.EE().DamageMulti), knockBack, player.whoAmI);
            }
            else if (mode == 2) //homing
            {
                Projectile.NewProjectile(new Vector2(position.X, position.Y + Main.rand.NextFloat(10, -20)), new Vector2(speedX, speedY), ModContent.ProjectileType<HomingRocketProjectile>(), (int)((damage + 100 + player.EE().DamageBonus) * player.EE().DamageMulti), knockBack, player.whoAmI);
            }

            return false;
        }

        public override void HoldItem(Player player)
        {
            if (Main.mouseRight && Main.mouseRightRelease)
            {
                Main.PlaySound(SoundID.MenuTick, (int)player.position.X, (int)player.position.Y);
                mode++;

                if (mode == 1)
                {
                    //Main.NewText("[c/AC7988:Precision Mode]");

                    item.useAnimation = 50;
                    item.useTime = 50;
                    firemode = "Precision";
                }

                if (mode == 3)
                {
                    //Main.NewText("[c/AC7988:Spread Mode]");

                    item.useAnimation = 6;
                    item.useTime = 6;
                    firemode = "Spread";
                    mode = 0;
                }

                if (mode == 2)
                {
                    //Main.NewText("[c/AC7988:Homing Mode]");

                    item.useAnimation = 12;
                    item.useTime = 12;
                    firemode = "Homing";

                }

            }

        }

        public override bool CanUseItem(Player player)
        {
            if (mode == 1)
            {
                // Ensures no more than one rocket can fire during this mode
                return player.ownedProjectileCounts[ModContent.ProjectileType<FollowRocketProjectile>()] < 1;
            }
            else
            {
                return true;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -10);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<RocketMinigun>(), 1);
            recipe.AddIngredient(ModContent.ItemType<NovaBuster>(), 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 25);
            recipe.AddIngredient(ItemID.FragmentNebula, 25);
            recipe.AddIngredient(ItemID.FragmentStardust, 25);
            recipe.AddIngredient(ItemID.FragmentVortex, 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}