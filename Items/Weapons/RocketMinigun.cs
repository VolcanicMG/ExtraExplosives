using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class RocketMinigun : ModItem
    {
        private int fireSpeed = 15;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket Minigun");
            Tooltip.SetDefault("Uses up a lot of rockets");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 5;
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
            Item.rare = 10;
            //Item.ranged = true;
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (fireSpeed > 5) //change the firespeed
            {
                fireSpeed -= 1;
            }

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 56f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;

            return true;
        }*/

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