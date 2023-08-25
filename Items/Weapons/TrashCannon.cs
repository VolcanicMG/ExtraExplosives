using ExtraExplosives.Projectiles.Weapons.TrashCannon;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class TrashCannon : ExplosiveWeapon
    {
        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/TrashCannon/TrashCannon";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trash Cannon");
            Tooltip.SetDefault("'One man's trash is another's lethal projectile'");
        }

        public override void SafeSetDefaults()
        {
            Item.damage = 18;
            Item.width = 46;
            Item.height = 36;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 7;
            Item.crit = 11;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 15;
            Item.useAmmo = AmmoID.Rocket;

            /*PrimarySounds = new LegacySoundStyle[4];
            SecondarySounds = null;

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] =
                    Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + n);
            }*/
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
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 1);
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                (int)player.position.X, (int)player.position.Y);

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int numberProjectiles = 5 + Main.rand.Next(5); // 5 - 9 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<TrashCannonProjectile>(), damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tmodloader to shoot projectile
        }*/
    }
}