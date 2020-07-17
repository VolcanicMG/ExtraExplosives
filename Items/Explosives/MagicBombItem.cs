using System.Collections.Generic;
using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using On.Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Main = Terraria.Main;
using Player = Terraria.Player;
using Projectile = Terraria.Projectile;

namespace ExtraExplosives.Items.Explosives
{
<<<<<<< HEAD
    public class MagicBombItem : ModItem
=======
    public class MagicBombItem : ExplosiveItem
>>>>>>> Charlie's-Uploads
    {
        private int _pickPower = 0;
        private int timeLeft = 0;

        public override bool CloneNewInstances => true;
<<<<<<< HEAD
        public override string Texture => "Terraria/Item_" + ItemID.StickyBomb;
=======
>>>>>>> Charlie's-Uploads
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Bomb");
            Tooltip.SetDefault("It can be imbued with mana\n" +    // Not all mages cast spells lol
                               "Right Click to increase its power");
        }

<<<<<<< HEAD
        public override void SetDefaults()
        {
=======
        public override void SafeSetDefaults()
        {
            item.damage = 100;
            item.knockBack = 25;
>>>>>>> Charlie's-Uploads
            item.useTurn = true;
            item.height = 20;
            item.width = 20;
            item.shoot = ModContent.ProjectileType<MagicBombProjectile>();
            item.shootSpeed = 10f;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.value = 1000;
            item.maxStack = 99;
            item.useAnimation = 40;
            item.useTime = 40;
            item.useStyle = 4;
            //item.mana = 10;
            item.channel = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY),
<<<<<<< HEAD
                ModContent.ProjectileType<SmallExplosiveProjectile>(), item.damage, 0);   //ModContent.ProjectileType<MagicBombProjectile>()
            item.damage = 40;
=======
                ModContent.ProjectileType<SmallExplosiveProjectile>(), item.damage, item.knockBack);   //ModContent.ProjectileType<MagicBombProjectile>()
            item.damage = 100;
>>>>>>> Charlie's-Uploads
            return false;
        }

        public override void HoldItem(Player player)
        {
            if (timeLeft != 0) timeLeft--;
            base.HoldItem(player);
        }


        public override bool AltFunctionUse(Player player)
        {
            if (player.statMana < 20 || timeLeft != 0 || item.damage >= 1000) return false;
<<<<<<< HEAD
            item.damage += 20;    // TODO add indicator showing failed or successful mana addition
=======
            item.damage += 30;    // TODO add indicator showing failed or successful mana addition
>>>>>>> Charlie's-Uploads
            player.statMana -= 20;
            timeLeft = 30;
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var currentDamageTooltip = new TooltipLine(mod, "CurrentDamage", $"Its damage has been increased by {item.damage - 40}");
            currentDamageTooltip.overrideColor = Color.Aquamarine;
            tooltips.Add(currentDamageTooltip);
        }
<<<<<<< HEAD
        
        
=======
>>>>>>> Charlie's-Uploads
    }
}