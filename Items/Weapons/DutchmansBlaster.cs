using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class DutchmansBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dutchman's Blaster");
            Tooltip.SetDefault("This belongs on a ship.\n" +
                               "Sadly, pirates care little for rules.");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.ranged = true;
            item.width = 54;
            item.height = 28;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 155; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 12;
            item.useAmmo = AmmoID.Rocket;
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 4);
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true; // return false because we don't want tmodloader to shoot projectile
        }
    }
}