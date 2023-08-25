using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Player = Terraria.Player;
using Projectile = Terraria.Projectile;

namespace ExtraExplosives.Items.Explosives
{
    public class MagicBombItem : ExplosiveItem
    {
        private int _pickPower = 0;
        private int timeLeft = 0;

        protected override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magic Bomb");
            /* Tooltip.SetDefault("It can be imbued with mana\n" +    // Not all mages cast spells lol
                               "Right Click to increase its power"); */
        }

        public override void SafeSetDefaults()
        {
            Item.damage = 100;
            Item.knockBack = 25;
            Item.useTurn = true;
            Item.height = 20;
            Item.width = 20;
            Item.shoot = ModContent.ProjectileType<MagicBombProjectile>();
            Item.shootSpeed = 10f;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = 1000;
            Item.maxStack = 99;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.useStyle = 4;
            //item.mana = 10;
            Item.channel = true;
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY),
                ModContent.ProjectileType<SmallExplosiveProjectile>(), Item.damage, Item.knockBack);   //ModContent.ProjectileType<MagicBombProjectile>()
            Item.damage = 100;
            return false;
        }*/

        public override void HoldItem(Player player)
        {
            if (timeLeft != 0) timeLeft--;
            base.HoldItem(player);
        }


        public override bool AltFunctionUse(Player player)
        {
            if (player.statMana < 20 || timeLeft != 0 || Item.damage >= 1000) return false;
            Item.damage += 30;    // TODO add indicator showing failed or successful mana addition
            player.statMana -= 20;
            timeLeft = 30;
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var currentDamageTooltip = new TooltipLine(Mod, "CurrentDamage", $"Its damage has been increased by {Item.damage - 40}");
            currentDamageTooltip.OverrideColor = Color.Aquamarine;
            tooltips.Add(currentDamageTooltip);
        }
    }
}