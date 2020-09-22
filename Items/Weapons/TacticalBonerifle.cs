using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectileID = Terraria.ID.ProjectileID;

namespace ExtraExplosives.Items.Weapons
{
    public class TacticalBonerifle : ExplosiveWeapon
    {
        private int swapCooldown = 0;

        protected override string SoundLocation { get; } = "Sounds/Item/Weapons/TacticalBonerifle/TacticalBonerifle";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tactical Bonerifle");
            Tooltip.SetDefault("Doot. Doot. Shoot.");
        }

        public override void SafeSetDefaults()
        {
            item.autoReuse = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useAmmo = AmmoID.Bullet;
            item.crit = 15;
            item.width = 66;
            item.height = 36;
            item.shoot = 10;
            //item.UseSound = SoundID.Item11;
            item.channel = true;
            item.damage = 33;
            item.shootSpeed = 10f;
            item.noMelee = true;
            item.value = Item.buyPrice(0, 15, 0, 50);
            item.knockBack = 4f;
            item.rare = ItemRarityID.Yellow;
            item.ranged = true;
            
            PrimarySounds = new LegacySoundStyle[4];
            SecondarySounds = new LegacySoundStyle[4];

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] =
                    mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + "Primary" + n);
            }
            for (int n = 1; n <= SecondarySounds.Length; n++)
            {
                SecondarySounds[n - 1] =
                    mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + "Secondary" + n);
            }
        }

        public override void HoldItem(Player player)
        {
            
            base.HoldItem(player);
        }

        public override void UpdateInventory(Player player)
        {
            if (swapCooldown > 0)
            {
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
            return new Vector2(-14, -7);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            switch (item.useAmmo)
            {
                case 97:    // Bullet
                    Main.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                        (int) player.position.X, (int) player.position.Y);
                    break;
                case 771:    // Rocket
                    Main.PlaySound(SecondarySounds[Main.rand.Next(SecondarySounds.Length)],
                        (int)player.position.X, (int) player.position.Y);
                    break;
            }
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                position.Y -= 6;
            }
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            // Might change the alt function to simply act as the grenade launcher
            //     instead of just hotswapping stats
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