﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class MinerainLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Minerain-Launcher");
            Tooltip.SetDefault("'Today's Forcast; Cloudy with a chance of death from above'");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.width = 56;
            item.height = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 134; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 10;
            item.useAmmo = AmmoID.Rocket;
        }

        //public override void ModifyTooltips(List<TooltipLine> tooltips)
        //{
        //    TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
        //    if (stats != null)
        //    {
        //        string[] split = stats.text.Split(' ');
        //        string damageValue = split.First();
        //        string damageWord = split.Last();
        //        stats.text = damageValue + "x3 explosive " + damageWord;
        //    }
        //}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -4);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.ProximityMineI, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tmodloader to shoot projectile
        }
    }
}