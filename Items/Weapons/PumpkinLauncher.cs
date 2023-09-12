using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class PumpkinLauncher : ExplosiveWeapon
    {
        protected override string SoundLocation { get; } = "";

        public override void SafeSetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useAnimation = 12;
            Item.useTime = 4;
            Item.reuseDelay = 14;
            Item.useAmmo = AmmoID.Rocket;
            Item.width = 66;
            Item.height = 34;
            Item.shoot = 134;
            Item.UseSound = SoundID.Item11;
            Item.channel = true;
            Item.damage = 30;
            Item.shootSpeed = 7;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 15, 0, 50);
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Yellow;
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
            return new Vector2(-10, -6);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float speedX = velocity.X;
            float speedY = velocity.Y;

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 2);
        }
    }
}