using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class SilentCricket : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silent Cricket");
            Tooltip.SetDefault("I feel like imma break this damn thing!");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 52;
            item.height = 28;
            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 11;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 133; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 12;    // Instant (ID)
            item.crit = 0;
            item.useAmmo = AmmoID.Rocket;
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, -3);
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