using ExtraExplosives.Projectiles.Weapons.DutchmansBlaster;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public class DutchmansBlaster : ExplosiveWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dutchman's Blaster");
            Tooltip.SetDefault("'This belongs on a ship.\n" +
                               "Sadly, pirates care little for rules.'");
        }

        protected override string SoundLocation { get; } = "Sounds/Item/Weapons/DutchmansBlaster/DutchmansBlaster";

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

            //PrimarySounds = new LegacySoundStyle[4];
            /*SecondarySounds = null;

            for (int n = 1; n <= PrimarySounds.Length; n++)
            {
                //PrimarySounds[n - 1] =
                    //Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + n);
            }*/
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
            return new Vector2(-6, 4);
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            SoundEngine.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                (int)player.position.X, (int)player.position.Y);

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Projectile.NewProjectile(new Vector2(position.X, position.Y), new Vector2(speedX, speedY), ModContent.ProjectileType<DutchmansBlasterProjectile>(), (int)((damage + player.EE().DamageBonus) * player.EE().DamageMulti), knockBack, player.whoAmI);

            return false; // return false because we don't want tmodloader to shoot projectile
        }*/
    }
}