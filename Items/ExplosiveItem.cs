using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExtraExplosives.Items
{
    public abstract class ExplosiveItem : ModItem
    {
        public override bool CloneNewInstances { get; } = true;

        /// <summary>
        /// If this is true then the mod will add this item to the disclaimer list. Returns false by default
        /// </summary>
        public bool toolTipDisclamer = false;

        /// <summary>
        /// If this is true then the mod will add this item to the Bombard tooltip list. Returns false by default
        /// </summary>
        public bool BombardTag = false;

        public ModItem ModItem
        {
            get;
            internal set;
        }

        public bool Explosive = true;

        public virtual void SafeSetDefaults()
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
        }

        public override void Load(TagCompound tag)
        {
            base.Load(tag);
            //Used to add the tooltip without manually adding it in the main mod class
            if (toolTipDisclamer) ExtraExplosives.disclaimerTooltip.Add(this.item.type);
            if (BombardTag) ExtraExplosives._tooltipWhitelist.Add(this.item.type);
        }
    }
}