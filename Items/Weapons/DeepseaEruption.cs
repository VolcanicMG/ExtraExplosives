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
    public class DeepseaEruption : ExplosiveWeapon
    {
        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/DeepseaEruption/DeepseaEruption";

        public override void SafeSetDefaults()
        {
            Item.damage = 65;
            //Item.ranged = true;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.GrenadeI;
            Item.shootSpeed = 10;
            Item.useAmmo = AmmoID.Rocket;

            PrimarySounds = new SoundStyle[4];
            SecondarySounds = null;

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] = new SoundStyle(SoundLocation + n);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.Mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.Text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.Text = damageValue + " Explosive " + damageWord;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)], position);

            float speedX = velocity.X;
            float speedY = velocity.Y;

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int numberProjectiles = 7;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.GrenadeII, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}