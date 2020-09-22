using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class Blunderboom : ExplosiveWeapon
    {
        private int swapCooldown = 0;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blunderboom");
            Tooltip.SetDefault("Lead and explosions go well together");
        }

        protected override string SoundLocation { get; } = "Sounds/Item/Weapons/Blunderboom/Blunderboom";

        public override void SafeSetDefaults()
        {
            item.damage = 26;
            item.ranged = true;
            item.width = 78;
            item.height = 32;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 2.5f;
            item.value = 10000;
            item.rare = ItemRarityID.LightRed;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 11;
            item.useAmmo = AmmoID.Bullet;
            
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

        public override void UpdateInventory(Player player)
        {
            if (swapCooldown > 0)
                swapCooldown--;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string firemode = (item.useAmmo == AmmoID.Bullet ? "Shotgun" : "Explosive");
            var fireModeUseTip = new TooltipLine(mod, "Firemode", $"Fire Mode: {firemode}");
            fireModeUseTip.overrideColor = Color.Brown;
            tooltips.Add(fireModeUseTip);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
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
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            if (swapCooldown != 0) return false;
            swapCooldown = 60;
            if (item.useAmmo == AmmoID.Bullet)
            {
                item.shoot = 133;
                item.useAmmo = AmmoID.Rocket;
                item.useAnimation = 50;
                item.useTime = 50;
                item.shootSpeed = 5;
                item.damage = 40;
                item.knockBack = 7;
                Main.NewText("Loaded with gunpowder");
            }
            else
            {
                item.shoot = 10;
                item.useAmmo = AmmoID.Bullet;
                item.useTime = 45;
                item.useAnimation = 45;
                item.shootSpeed = 11;
                item.damage = 26;
                item.knockBack = 2.5f;
                Main.NewText("Loaded with shrapnel");
            }
            return false;
        }
    }
}