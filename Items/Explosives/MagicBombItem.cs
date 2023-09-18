using ExtraExplosives.Projectiles;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;
using Player = Terraria.Player;
using Projectile = Terraria.Projectile;

namespace ExtraExplosives.Items.Explosives
{
    public class MagicBombItem : ExplosiveItem
    {
        private int time = 0;
        private bool repeateOnce;
        private bool runOnce;

        protected override bool CloneNewInstances => true;

        public override void SafeSetDefaults()
        {
            Item.damage = 10;
            Item.knockBack = 25;
            Item.useTurn = true;
            Item.height = 20;
            Item.width = 20;
            Item.shoot = ModContent.ProjectileType<MagicBombProjectile>();
            Item.shootSpeed = 7f;
            Item.rare = ItemRarityID.Orange;
            Item.value = 1000;
            Item.maxStack = 99;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.channel = true;
            Item.autoReuse = true;
        }

        //Left in to block the use of the shoot hook
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

        //Used instead of the shoot hook
        private void ManaualShoot(Player player)
        {
            //Consume the item
            player.ConsumeItem(this.Item.type);

            Vector2 velocity = Item.shootSpeed * player.Center.DirectionTo(Main.MouseWorld);

            Projectile proj = Projectile.NewProjectileDirect(player.GetSource_ItemUse_WithPotentialAmmo(Item, AmmoID.None), player.Center, velocity,
                ModContent.ProjectileType<MagicBombProjectile>(), Item.damage, Item.knockBack);

            proj.rotation = proj.velocity.ToRotation();

            //Reset for the next time the item is used
            Item.damage = 10;
            time = 0;
        }

        public override void HoldItem(Player player)
        {
            //Check if the player is the local player
            if (player != Main.player[player.whoAmI]) return;

            //Fire the projectile if the player is not channeling and its time
            if (!player.channel && !repeateOnce && runOnce)
            {
                repeateOnce = true;
                ManaualShoot(player);
            }

            //Make sure we are holding left mouse down
            if (!player.channel) return;

            //Checking to make sure we can fire the projectile
            repeateOnce = false;
            runOnce = true; //Used so we dont fire one right of the bat

            //Reduced the time while holding the item
            time--;

            //Every 20 ticks (0.2 seconds) add 10 damage to the bomb
            if (time % 20 == 0 && player.statMana >= 10 && Item.damage < 500)
            {
                CombatText.NewText(player.getRect(), Microsoft.Xna.Framework.Color.Blue, "+10 Damage");
                Item.damage += 10;
                player.statMana -= 10;
                player.manaRegenDelay = 100;

                SoundEngine.PlaySound(SoundID.Item9, player.position);
            }

            base.HoldItem(player);
        }

        private void SpawnDust(Player player)
        {
            Dust dust;

            //Get the center of the player and rotate the dust around them
            Vector2 startpoint = player.Center;

            //Set the directon to the player
            Vector2 velocity = 2 * startpoint.DirectionTo(player.Center);

            dust = Dust.NewDustPerfect(startpoint, 6, velocity);

            dust.noGravity = true;
            dust.fadeIn = .1f;

            Main.NewText(dust.position);
        }
    }
}