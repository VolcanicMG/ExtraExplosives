using ExtraExplosives.Projectiles.Weapons.TrashCannon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class RocketMinigun : ModItem
    {
        private int fireSpeed = 15;

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useAnimation = fireSpeed;
            Item.useTime = fireSpeed;
            Item.useAmmo = AmmoID.Rocket;
            Item.crit = 15;
            Item.width = 66;
            Item.height = 36;
            Item.shoot = 134;
            Item.UseSound = SoundID.Item11;
            Item.channel = true;
            Item.damage = 200;
            Item.shootSpeed = 10f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 15, 0, 50);
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Red;
            //Item.ranged = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float speedX = velocity.X;
            float speedY = velocity.Y;

            if (fireSpeed > 5) //change the firespeed
            {
                fireSpeed -= 1;
            }

            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 56f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);

            return false;
        }

        public override void HoldItem(Player player)
        {
            if (player.channel == true)
            {
                Item.useTime = fireSpeed;
                Item.useAnimation = fireSpeed;
            }
            else if (player.channel == false)
            {
                fireSpeed = 15;
                Item.useTime = fireSpeed;
                Item.useAnimation = fireSpeed;
            }

            base.HoldItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -10);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddIngredient(ItemID.ChainGun, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}