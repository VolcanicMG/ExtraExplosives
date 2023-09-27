using ExtraExplosives.Projectiles;
using ExtraExplosives.Projectiles.Weapons.DutchmansBlaster;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class DutchmansBlaster : ExplosiveWeapon
    {
        protected override string SoundLocation { get; } = "ExtraExplosives/Assets/Sounds/Item/Weapons/DutchmansBlaster/DutchmansBlaster";

        public override void SafeSetDefaults()
        {
            Item.damage = 40;
            //Item.ranged = true;
            Item.width = 54;
            Item.height = 28;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.shoot = 134; //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 24;
            Item.useAmmo = AmmoID.Rocket;
            Item.ArmorPenetration = 5;

             PrimarySounds = new SoundStyle[4];
             SecondarySounds = null;

             for (int n = 1; n <= PrimarySounds.Length; n++)
             {
                 PrimarySounds[n - 1] = new SoundStyle(SoundLocation + n);
             }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.Mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.Text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.Text = damageValue + " explosive " + damageWord;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 8);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)], position);

            float speedX = velocity.X;
            float speedY = velocity.Y;

            //Spread
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));

            //Muzzle offset
            position += Vector2.Normalize(velocity) * 35f; 

            Projectile.NewProjectile(source, new Vector2(position.X, position.Y), perturbedSpeed, ModContent.ProjectileType<DutchmansBlasterProjectile>(), (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockback, player.whoAmI);

            return false; // return false because we don't want tmodloader to shoot projectile
        }
    }
}