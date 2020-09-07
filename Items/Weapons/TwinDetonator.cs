using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class TwinDetonator : ExplosiveWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twin Detonator");
            Tooltip.SetDefault("Double the trouble.\n" +
                               "Launches two grenades per use\n" +
                               "Consumes one rocket per volley");
        }

        protected override string SoundLocation { get; } = "Sounds/Item/Weapons/TwinDetonator/TwinDetonator";

        public override void SafeSetDefaults()
        {
            item.damage = 15;
            item.ranged = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.shoot = 133; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 8;
            item.useAmmo = AmmoID.Rocket;
            
            PrimarySounds = new LegacySoundStyle[4];
            SecondarySounds = null;

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] =
                    mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + n);
            }
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 1);
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                (int) player.position.X, (int) player.position.Y);
            
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tmodloader to shoot projectile
        }
    }
}