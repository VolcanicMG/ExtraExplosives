using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class CoralKrakSlinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coral Krak-Slinger");
            Tooltip.SetDefault("Improvised, but deadly");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 21;
            item.useTime = 21;
            item.useAmmo = AmmoID.Rocket;
            item.width = 26;
            item.height = 38;
            item.shoot = 133;
            item.UseSound = SoundID.Item11;
            item.damage = 12;
            item.shootSpeed = 5;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 15, 0, 50);
            item.knockBack = 4f;
            item.rare = ItemRarityID.Blue;
            item.ranged = true;
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -2);
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
    }
}