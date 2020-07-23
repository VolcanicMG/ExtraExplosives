using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class PumpkinLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpkin Launcher");
            Tooltip.SetDefault("These pumpkins are NOT safe around open flames\n" +
                               "Launches three rockets in quick succession\n" +
                               "Consumes one rocket per burst");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 33;
            item.useTime = 33;
            item.useAmmo = AmmoID.Rocket;
            item.width = 66;
            item.height = 34;
            item.shoot = 134;
            item.UseSound = SoundID.Item11;
            item.channel = true;
            item.damage = 30;
            item.shootSpeed = 7;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 15, 0, 50);
            item.knockBack = 4f;
            item.rare = ItemRarityID.Yellow;
            item.ranged = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 4);
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                // If you want to randomize the speed to stagger the projectiles
                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false; // return false because we don't want tmodloader to shoot projectile
        }
    }
}