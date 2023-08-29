using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Weapons
{
    public abstract class ExplosiveWeapon : ExplosiveItem
    {
        // Class Variables

        protected abstract string SoundLocation { get; }

        //protected LegacySoundStyle[] PrimarySounds;
        //protected LegacySoundStyle[] SecondarySounds;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //Left blank since tooltip modification needs to be handled on a per item basis
        }


        //Cleaned up class since we have a lot of repetitive code
    }
}