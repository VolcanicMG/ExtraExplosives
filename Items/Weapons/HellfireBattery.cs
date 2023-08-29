using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            // DisplayName.SetDefault("Hellfire Battery");
            /* Tooltip.SetDefault("'The rocket minigun's older bigger brother'\n" +
                "Right click to swap between 3 different modes"); */
        }

        public override void SafeSetDefaults()
        {
            Item.useStyle = 5;
            Item.autoReuse = true;
            Item.useAnimation = fireSpeed;
            Item.useTime = fireSpeed;
            Item.useAmmo = AmmoID.Rocket;
            Item.width = 78;
            Item.crit = 35;
            Item.height = 42;
            Item.shoot = 134;
            //Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Hellfire");
            Item.channel = true;
            Item.damage = 250;
            Item.shootSpeed = 13f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(10, 1, 0, 50);
            Item.knockBack = 4f;
            Item.rare = 11;
            //Item.ranged = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.Mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.Text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.Text = damageValue + " explosive " + damageWord;

            }

            var fireModeUseTip = new TooltipLine(Mod, "Multiplier", $"Fire Mode: {firemode}");
            fireModeUseTip.OverrideColor = Color.Tan;
            tooltips.Add(fireModeUseTip);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float speedX = velocity.X;
            float speedY = velocity.Y;

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
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 20), velocity.RotatedByRandom(MathHelper.ToRadians(25)), type, (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockback, player.whoAmI);
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 10), velocity.RotatedByRandom(MathHelper.ToRadians(25)), type, (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockback, player.whoAmI);
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), velocity.RotatedByRandom(MathHelper.ToRadians(25)), type, (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockback, player.whoAmI);

            }
            else if (mode == 1) //precision
            {
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 10), velocity, ModContent.ProjectileType<FollowRocketProjectile>(), (int)((damage + 3000 + player.EE().DamageBonus) * player.EE().DamageMulti), knockback, player.whoAmI);
            }
            else if (mode == 2) //homing
            {
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y + Main.rand.NextFloat(10, -20)), velocity, ModContent.ProjectileType<HomingRocketProjectile>(), (int)((damage + 100 + player.EE().DamageBonus) * player.EE().DamageMulti), knockback, player.whoAmI);
            }

            return false;
        }

        public override void HoldItem(Player player)
        {
            if (Main.mouseRight && Main.mouseRightRelease)
            {
                ////SoundEngine.PlaySound(SoundID.MenuTick, (int)player.position.X, (int)player.position.Y);
                mode++;

                if (mode == 1)
                {
                    //Main.NewText("[c/AC7988:Precision Mode]");

                    Item.useAnimation = 50;
                    Item.useTime = 50;
                    firemode = "Precision";
                }

                if (mode == 3)
                {
                    //Main.NewText("[c/AC7988:Spread Mode]");

                    Item.useAnimation = 6;
                    Item.useTime = 6;
                    firemode = "Spread";
                    mode = 0;
                }

                if (mode == 2)
                {
                    //Main.NewText("[c/AC7988:Homing Mode]");

                    Item.useAnimation = 12;
                    Item.useTime = 12;
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RocketMinigun>(), 1);
            recipe.AddIngredient(ModContent.ItemType<NovaBuster>(), 1);
            recipe.AddIngredient(ItemID.FragmentSolar, 25);
            recipe.AddIngredient(ItemID.FragmentNebula, 25);
            recipe.AddIngredient(ItemID.FragmentStardust, 25);
            recipe.AddIngredient(ItemID.FragmentVortex, 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}