using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public abstract class ExplosiveWeapon : ExplosiveItem
    {
        // Class Variables

        protected abstract string SoundLocation { get; }

        protected SoundStyle[] PrimarySounds;
        // Is there a way to make variables in an abstract optional?
        // TODO look into
        protected SoundStyle[] SecondarySounds;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //Left blank since tooltip modification needs to be handled on a per item basis
            // Do tooltips get modified in code anymore??? TODO Check to see if this can be removed
        }
    }
}