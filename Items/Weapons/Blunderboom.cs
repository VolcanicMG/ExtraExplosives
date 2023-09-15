using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class Blunderboom : ExplosiveWeapon
    {
        private int swapCooldown = 0;

        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/Blunderboom/Blunderboom";

        public override void SafeSetDefaults()
        {
            Item.damage = 26;
            Item.width = 78;
            Item.height = 32;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 2.5f;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 11;
            Item.useAmmo = AmmoID.Bullet;

            PrimarySounds = new SoundStyle[4];
            SecondarySounds = new SoundStyle[4];

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                PrimarySounds[n - 1] = new SoundStyle(SoundLocation + "Primary" + n);
            }
            for (int n = 1; n <= SecondarySounds.Length; n++)
            {
                SecondarySounds[n - 1] = new SoundStyle(SoundLocation + "Secondary" + n);
            }
        }

        public override void DangerousSetDefaults()
        {
            //Item.ranged = true;
            Explosive = false;
        }

        public override void UpdateInventory(Player player)
        {
            if (swapCooldown > 0)
                swapCooldown--;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            /*TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
            if (stats != null && !Explosive)
            {
                string[] split = stats.text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.text = damageValue + "x7 ranged " + damageWord;
            }*/

            string firemode = (Item.useAmmo == AmmoID.Bullet ? "Shotgun" : "Explosive");
            var fireModeUseTip = new TooltipLine(Mod, "Firemode", $"Fire Mode: {firemode}");
            fireModeUseTip.OverrideColor = Color.Tan;
            tooltips.Add(fireModeUseTip);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float speedX = velocity.X;
            float speedY = velocity.Y;
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            switch (Item.useAmmo)
            {
                case 97:    // Bullet
                    SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)], position);
                    int numberProjectiles = 7;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30    degree spread.
                        // If you want to randomize the speed to stagger the projectiles
                        float scale = 1f - (Main.rand.NextFloat() * .3f);
                        perturbedSpeed = perturbedSpeed * scale;
                        Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
                    }
                    return false; // return false because we don't want tmodloader to shoot projectile
                case 771:    // Rocket
                    //         SoundEngine.PlaySound(SecondarySounds[Main.rand.Next(SecondarySounds.Length)],
                    //(int)player.position.X, (int)player.position.Y);
                    Projectile.NewProjectile(source, position, velocity, ProjectileID.GrenadeIII, damage, knockback, player.whoAmI);
                    break;
            }
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            if (swapCooldown != 0) return false;
            swapCooldown = 60;
            if (Item.useAmmo == AmmoID.Bullet)
            {
                Item.shoot = ProjectileID.GrenadeIII;
                Item.useAmmo = AmmoID.Rocket;
                Item.useAnimation = 50;
                Item.useTime = 50;
                Item.shootSpeed = 5;
                Item.damage = 40;
                Item.knockBack = 7;
                //Item.ranged = false;
                Explosive = true;
                //Main.NewText("Loaded with gunpowder");
                ////SoundEngine.PlaySound(SoundID.MenuTick, (int)player.position.X, (int)player.position.Y);
                CombatText.NewText(player.getRect(), Microsoft.Xna.Framework.Color.BurlyWood, "Explosive");
            }
            else
            {
                Item.shoot = ProjectileID.Bullet;
                Item.useAmmo = AmmoID.Bullet;
                Item.useTime = 45;
                Item.useAnimation = 45;
                Item.shootSpeed = 11;
                Item.damage = 26;
                Item.knockBack = 2.5f;
                //Item.ranged = true;
                Explosive = false;
                // Main.NewText("Loaded with shrapnel");
                ////SoundEngine.PlaySound(SoundID.MenuTick, (int)player.position.X, (int)player.position.Y);

                CombatText.NewText(player.getRect(), Microsoft.Xna.Framework.Color.BurlyWood, "Shotgun");
            }
            return false;
        }
    }
}