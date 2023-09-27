﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectileID = Terraria.ID.ProjectileID;

namespace ExtraExplosives.Items.Weapons
{
    public class TacticalBonerifle : ExplosiveWeapon
    {
        private int swapCooldown = 0;

        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/TacticalBonerifle/TacticalBonerifle";

        public override void SafeSetDefaults()
        {
            Item.autoReuse = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useAmmo = AmmoID.Bullet;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.crit = 15;
            Item.width = 66;
            Item.height = 36;
            Item.shoot = 10;
            //item.UseSound = SoundID.Item11;
            Item.damage = 33;
            Item.shootSpeed = 10f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 15, 0, 50);
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Yellow;

             PrimarySounds = new SoundStyle[4];
             SecondarySounds = new SoundStyle[4];

             for (int n = 1; n <= PrimarySounds.Length; n++)
             {
                 PrimarySounds[n - 1] = new SoundStyle(SoundLocation + "Primary" + n);
             }
             for (int n = 1; n <= SecondarySounds.Length; n++)
             {
                 SecondarySounds[n - 1] = new SoundStyle(SoundLocation + "Secondary" + n);
             }
        }

        public override void DangerousSetDefaults()
        {
           // Item.ranged = true;
        }

        public override void HoldItem(Player player)
        {

            base.HoldItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            if (swapCooldown > 0)
            {
                swapCooldown--;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string firemode = (Item.useAmmo == AmmoID.Bullet ? "Bone Rifle" : "Bone Launcher");
            var fireModeUseTip = new TooltipLine(Mod, "Multiplier", $"Fire Mode: {firemode}");
            fireModeUseTip.OverrideColor = Color.Tan;
            tooltips.Add(fireModeUseTip);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //Speed variables (Used because the code hasn't been refractored and should eventually be removed)
            float speedX = velocity.X;
            float speedY = velocity.Y;

            //Muzzle offset
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                position.Y -= 8;
            }

            //Spread
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.

            switch (Item.useAmmo)
            {
                case 97:    // Bullet
                    SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)], position);
                    Projectile.NewProjectile(source, new Vector2(position.X, position.Y), perturbedSpeed, type, damage, knockback, player.whoAmI);
                    break;
                case 771:    // Rocket
                    SoundEngine.PlaySound(SecondarySounds[Main.rand.Next(SecondarySounds.Length)], position);
                    Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.Grenade, damage, knockback, player.whoAmI);
                    break;
                default:
                    Mod.Logger.InfoFormat("Something went wrong {0}", Item.useAmmo);
                    break;
            }
            
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            // Might change the alt function to simply act as the grenade launcher
            //     instead of just hotswapping stats
            if (swapCooldown != 0) return false;
            swapCooldown = 60;
            if (Item.useAmmo == AmmoID.Bullet)
            {
                Item.shoot = 133;
                Item.useAmmo = AmmoID.Rocket;
                Item.useAnimation = 90;
                Item.useTime = 90;
                Item.shootSpeed = 10;
                Item.damage = 40;
                Item.knockBack = 7;
                //Main.NewText("Bone Launcher");
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                CombatText.NewText(player.getRect(), Microsoft.Xna.Framework.Color.BurlyWood, "Grenade");
            }
            else
            {
                Item.shoot = 10;
                Item.useAmmo = AmmoID.Bullet;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.shootSpeed = 13;
                Item.damage = 35;
                Item.knockBack = 3.5f;
                // Main.NewText("Bone Rifle");
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                CombatText.NewText(player.getRect(), Microsoft.Xna.Framework.Color.BurlyWood, "Semi-automatic");
            }

            return false;
        }
    }
}