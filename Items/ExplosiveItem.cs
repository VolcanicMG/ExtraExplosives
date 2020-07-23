using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static ExtraExplosives.GlobalMethods;

namespace ExtraExplosives.Items
{
    public abstract class ExplosiveItem : ModItem
    {
        public override bool CloneNewInstances { get; } = true;

        public ModItem ModItem
        {
            get;
            internal set;
        }
        
        public bool Explosive;

        public abstract void SafeSetDefaults();
        
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.summon = false;
            item.thrown = false;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            
            mult = player.EE().DamageMulti;
            add += player.EE().DamageBonus;// * player.EE().DamageMulti;
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
        }
    }
}