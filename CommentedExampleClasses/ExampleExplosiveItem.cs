using ExtraExplosives.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.CommentedExampleClasses
{
    public class ExampleExplosiveItem : ExplosiveItem
    {
        public bool Autoload(ref string _) => false;    // Stops tML from auto loading this class

        /* This class is used to demonstrate the differences between Explosive and Mod Items,
        *     and the changes necessary to convert one to the other
        *  This class should also serve as reference for how the additions made by ExplosiveItem
        */

        /// <summary>
        /// This serves as a replacement for SetDefaults, which has been sealed with in ExplosiveItem to ensure basic functionality persists
        /// SafeSetDefaults is called before SetDefaults which safeguards against accidental changes to the require attributes
        ///     item.(melee/ranged/magic/summon/thrown) these five values are all set to false to allow explosive damage to work
        /// SafeSetDefaults should be used as the exclusive replacement to SetDefaults in 99% of cases
        /// </summary>
        public override void SafeSetDefaults()
        {
            //Put regular SetDefaults code here
        }

        /// <summary>
        /// DangerousSetDefaults should only be used to override the values shown below
        /// While other cases may exist which call for use other than editing these five values,
        ///     they should be avoided to ensure functionality of items, and readability and maintainability of code
        /// </summary>
        public override void DangerousSetDefaults()
        {
            //item.melee = false;
            //item.ranged = false;
            //item.magic = false;
            //item.summon = false;
            //item.thrown = false;
        }

        //If you for some reason need to interact with the underlying ModItem this ExplosiveItem is built on,
        //    and you cannot through this class directly
        // You can call this items ModItem similarly to how you would call an Item through a ModItem
        // this.ModItem will let you interact with this items ModItem


        // Most if not all functionality can be obtained with little changes to a traditional ModItem
        // Using any method besides SafeSetDefaults will be almost universally unnecessary
        // However the following methods are available for overriding if needed
        //    KEEP IN MIND HOWEVER, that changes the following methods may causes strange functionality and should be tested thoroughly



        // Functionality of damage, knockback, and crit chance is handled elsewhere so errors should only arise from balance issues
        // Changes are advised against in order to keep damage profiles consistent and aid in balancing

        /// <summary>
        /// Allows temporary changing of an items damage
        /// If changes are made remember to use += for add and flat and = for mult
        ///     using += on mult causes issues with the final damage values
        /// base functionality has the add modified by the players damage bonus and the mult set to the players damage multiplier
        /// </summary>
        /// <param name="player"></param>
        /// <param name="add"></param>
        /// <param name="mult"></param>
        /// <param name="flat"></param>
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
        }

        /// <summary>
        /// Allows temporary changing of an items knockback
        /// base functionality has the knockback modified by the players knockback modifier and then returns
        /// </summary>
        /// <param name="player"></param>
        /// <param name="knockback"></param>
        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
        }

        /// <summary>
        /// Allows temporary changing of the items crit chance
        /// base functionality has the crit chance modified by the players crit modifier and then returns
        /// </summary>
        /// <param name="player"></param>
        /// <param name="crit"></param>
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
        }

        /// <summary>
        /// This changes the Items tooltip to correctly show its damage, knockback, and crit chance
        /// Changing this method should be done only if specific formatting of the tooltip is required
        /// </summary>
        /// <param name="tooltips"></param>
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        }
    }
}