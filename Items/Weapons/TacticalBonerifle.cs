using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectileID = Terraria.ID.ProjectileID;

namespace ExtraExplosives.Items.Weapons
{
    public class TacticalBonerifle : ModItem
    {
        private int swapCooldown = 0;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tactical Bonerifle");
            Tooltip.SetDefault("Doot. Doot. Shoot.");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useAmmo = AmmoID.Bullet;
            item.crit = 15;
            item.width = 66;
            item.height = 36;
            item.shoot = 10;
            item.UseSound = SoundID.Item11;
            item.channel = true;
            item.damage = 33;
            item.shootSpeed = 10f;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 15, 0, 50);
            item.knockBack = 4f;
            item.rare = ItemRarityID.Yellow;
            item.ranged = true;
        }

        public override void HoldItem(Player player)
        {
            
            base.HoldItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            if (swapCooldown > 0)
            {
                Main.NewText(swapCooldown);
                swapCooldown--;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string firemode = (item.useAmmo == AmmoID.Bullet ? "Bone Rifle" : "Bone Launcher");
            var fireModeUseTip = new TooltipLine(mod, "Multiplier", $"Fire Mode: {firemode}");
            fireModeUseTip.overrideColor = Color.Tan;
            tooltips.Add(fireModeUseTip);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-18, 4);
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

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            if (swapCooldown != 0) return false;
            swapCooldown = 60;
            if (item.useAmmo == AmmoID.Bullet)
            {
                item.shoot = 133;
                item.useAmmo = AmmoID.Rocket;
                item.useAnimation = 90;
                item.useTime = 90;
                item.shootSpeed = 5;
                item.damage = 40;
                item.knockBack = 7;
                Main.NewText("Bone Launcher");
            }
            else
            {
                item.shoot = 10;
                item.useAmmo = AmmoID.Bullet;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootSpeed = 13;
                item.damage = 35;
                item.knockBack = 3.5f;
                Main.NewText("Bone Rifle");
            }

            return false;
        }
    }
}