using Microsoft.Xna.Framework;
using System.Collections.Generic;
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

        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/TacticalBonerifle/TacticalBonerifle";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tactical Bonerifle");
            Tooltip.SetDefault("Doot. Doot. Shoot.");
        }

        public override void SafeSetDefaults()
        {
            Item.autoReuse = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useAmmo = AmmoID.Bullet;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.crit = 15;
            Item.width = 66;
            Item.height = 36;
            Item.shoot = 10;
            //item.UseSound = SoundID.Item11;
            Item.damage = 33;
            Item.shootSpeed = 10f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 15, 0, 50);
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Yellow;

            /*PrimarySounds = new LegacySoundStyle[4];
            SecondarySounds = new LegacySoundStyle[4];

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] =
                    Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + "Primary" + n);
            }
            for (int n = 1; n <= SecondarySounds.Length; n++)
            {
                SecondarySounds[n - 1] =
                    Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + "Secondary" + n);
            }*/
        }

        public override void DangerousSetDefaults()
        {
           // Item.ranged = true;
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
            string firemode = (Item.useAmmo == AmmoID.Bullet ? "Bone Rifle" : "Bone Launcher");
            var fireModeUseTip = new TooltipLine(Mod, "Multiplier", $"Fire Mode: {firemode}");
            fireModeUseTip.OverrideColor = Color.Tan;
            tooltips.Add(fireModeUseTip);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            switch (Item.useAmmo)
            {
                case 97:    // Bullet
                    //SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                        (int)player.position.X, (int)player.position.Y);
                    Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
                    break;
                case 771:    // Rocket
                    //SoundEngine.PlaySound(SecondarySounds[Main.rand.Next(SecondarySounds.Length)],
                        (int)player.position.X, (int)player.position.Y);
                    Projectile.NewProjectile(position, new Vector2(speedX, speedY), ProjectileID.Grenade, damage, knockBack, player.whoAmI);
                    break;
                default:
                    Mod.Logger.InfoFormat("Something went wrong {0}", Item.useAmmo);
                    break;
            }
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                position.Y -= 6;
            }
            return false;
        }*/

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
            if (Item.useAmmo == AmmoID.Bullet)
            {
                Item.shoot = 133;
                Item.useAmmo = AmmoID.Rocket;
                Item.useAnimation = 90;
                Item.useTime = 90;
                Item.shootSpeed = 10;
                Item.damage = 40;
                Item.knockBack = 7;
                //Main.NewText("Bone Launcher");
                ////SoundEngine.PlaySound(SoundID.MenuTick, (int)player.position.X, (int)player.position.Y);
            }
            else
            {
                Item.shoot = 10;
                Item.useAmmo = AmmoID.Bullet;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.shootSpeed = 13;
                Item.damage = 35;
                Item.knockBack = 3.5f;
                // Main.NewText("Bone Rifle");
                ////SoundEngine.PlaySound(SoundID.MenuTick, (int)player.position.X, (int)player.position.Y);
            }

            return false;
        }
    }
}