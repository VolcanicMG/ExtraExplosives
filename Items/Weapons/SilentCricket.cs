﻿using System.Collections.Generic;
using System.Linq;
 using ExtraExplosives.Projectiles;
 using ExtraExplosives.Projectiles.Weapons;
 using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
 using Terraria.DataStructures;
 using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class SilentCricket : ExplosiveWeapon
    {
        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/SilentCricket/SilentCricketShot";

        public override void SafeSetDefaults()
        {
            Item.damage = 100;
            Item.width = 52;
            Item.height = 28;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 11;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SilentCricketProjectile>();
            Item.UseSound = new SoundStyle(SoundLocation);
            Item.shootSpeed = 12;    // Instant (ID)
            Item.crit = 0;
            Item.useAmmo = AmmoID.Rocket;
            
            PrimarySounds = null;
            SecondarySounds = null;
        }
        
        // This should be done in the hjson ( i think )
        /*public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.text = damageValue + " explosive " + damageWord;
            }
        }*/
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, -3);
        }
        
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            
            SoundEngine.PlaySound(new SoundStyle(SoundLocation), player.position);
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(2));
            Projectile.NewProjectile(source,
                                     position,
                                     perturbedSpeed,
                                     ModContent.ProjectileType<SilentCricketProjectile>(),
                                     damage,
                                     knockback,
                                     player.whoAmI);
            
           
            return false;
        }*/
    }
}