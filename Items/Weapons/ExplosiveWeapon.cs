﻿using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public abstract class ExplosiveWeapon : ExplosiveItem
    {
        // Class Variables

        protected abstract string SoundLocation { get; }

        protected LegacySoundStyle[] PrimarySounds;
        protected LegacySoundStyle[] SecondarySounds;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //Left blank since tooltip modification needs to be handled on a per item basis
        }

        // IGNORE //
        /*public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.summon = false;
            item.thrown = false;
            //item.useStyle = 5;    // Required for correct animations (override where needed)
            DangerousSetDefaults();
        }

        public virtual void DangerousSetDefaults()
        {
        }
        
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult = player.EE().DamageMulti;
            add += player.EE().DamageBonus;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback = (knockback + player.EE().KnockbackBonus) * player.EE().KnockbackMulti;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            if (Explosive) crit += player.EE().ExplosiveCrit;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine stats = tooltips.FirstOrDefault(t => t.Name == "Damage" && t.mod == "Terraria");
            if (stats != null)
            {
                string[] split = stats.text.Split(' ');
                string damageValue = split.First();
                string damageWord = split.Last();
                stats.text = damageValue + " explosive " + damageWord;
            }
        }*/
    }
}