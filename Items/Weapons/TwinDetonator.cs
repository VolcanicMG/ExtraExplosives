using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ExtraExplosives.Items.Weapons
{
    public class TwinDetonator : ExplosiveWeapon
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Twin Detonator");
            /* Tooltip.SetDefault("'Double the trouble.'\n" +
                               "Launches a pair of grenades\n" +
                               "Consumes one rocket per volley"); */
        }

        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/TwinDetonator/TwinDetonator";

        public override void SafeSetDefaults()
        {
            Item.damage = 15;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.shoot = 133; //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 8;
            Item.useAmmo = AmmoID.Rocket;

            /*PrimarySounds = new LegacySoundStyle[4];
            SecondarySounds = null;

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] =
                    Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + n);
            }*/
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 1);
        }

        //public override void ModifyTooltips(List<TooltipLine> tooltips)
        //{
        //    TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
        //    if (stats != null)
        //    {
        //        string[] split = stats.text.Split(' ');
        //        string damageValue = split.First();
        //        string damageWord = split.Last();
        //        stats.text = damageValue + "x2 explosive " + damageWord;
        //    }
        //}

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                (int)player.position.X, (int)player.position.Y);

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
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.GrenadeI, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tmodloader to shoot projectile
        }*/
    }
}