using ExtraExplosives.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;

namespace ExtraExplosives.CommentedExampleClasses
{
    // ExplosiveWeapon extends ExplosiveItem, and adds functionality for sound
    // Additional functionality will be added as needed
    // It is recommended to look through the CommentedExplosiveItem class before this to ensure you understand what ExplosiveWeapon is built on
    // The additional subclass is mostly here to allow for future functionality and compatibility
    // Alongside that it also stops code repetition without having to null values in every ExplosiveItem
    public class ExampleExplosiveWeapon : ExplosiveWeapon
    {
        // This first line is only meant to stop tML from loading this class (and failing because no assets are connected to it
        // It can be ignored
        public override bool Autoload(ref string _) => false;

        /// <summary>
        /// The location of the sound this weapon uses when fired
        /// Should be a filepath (expressed as a string) relative to the main project directory
        ///     see other ExplosiveWeapons and ExplosiveProjectiles for examples
        /// Filepath should include the file name but leave off the numbers associated with it,
        ///     E.G. if a weapon has 5 sounds they should be identically named with a number appended to the end of each
        ///         the numbers need to be in sequential order unless an alternate method of populating the sound arrays is used
        /// Should be set to null if no value is needed (may be made non-abstract in the future)
        ///     ignore instances with their value set to "n/a", using null is more efficient
        /// 
        /// </summary>
        protected override string SoundLocation { get; } = null;

        /// <summary>
        /// Here to illustrate PrimarySounds and SecondarySounds
        /// </summary>
        public override void SafeSetDefaults()
        {
            /*
             * These two arrays hold the sounds which will be used by the weapon
             * The names (Primary and Secondary) are all but semantics and do not indicate the function of the sounds
             * In most cases the names do indicate the use of the sounds but this can be subverted if needed
             * The length of the arrays should be equal to the number of unique sounds used by the weapon
             * For readability, PrimarySounds should be used before SecondarySounds and SecondarySounds should be nullified if PrimarySounds is not used
             */
            PrimarySounds = new LegacySoundStyle[1];    // How to initialize the array when used
            SecondarySounds = null;                     // How to nullify the array when not used

            // This for loop shows the most common way of populating the sound arrays
            for (int n = 1; n <= PrimarySounds.Length; n++)    // n is initialized to 1 instead of 0 to allow for proper name spacing and readability
            {
                PrimarySounds[n - 1] =
                    // When populating the array it is important to remember the file path the sounds are located in
                    // The Terraria.ModLoader.SoundType.Type must be the same as the sounds main folder (clarification in a moment)
                    // Failure to do so will causes the sounds to not get loaded and the arrays will be filled with nulls
                    // To avoid this, ensure the sounds type folder to be the same as the SoundType
                    //    The possible sound types are: Custom, Item, Misc, NPCHit, NPCKilled
                    // The sounds filepath should have the same type as its original subfolder (the folder under Sounds)
                    // If additional clarification is needed either reference existing ExplosiveWeapons and ExplosivesProjectiles (and their sound locations)
                    // Or ask in the discord\
                    mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Item, SoundLocation + n);
            }
            // If SecondarySounds is also used, a second for loop will be needed
        }

        /// <summary>
        /// Here to illustrate using PrimarySounds and SecondarySounds
        /// </summary>
        /// <param name="player"></param>
        /// <param name="position"></param>
        /// <param name="speedX"></param>
        /// <param name="speedY"></param>
        /// <param name="type"></param>
        /// <param name="damage"></param>
        /// <param name="knockBack"></param>
        /// <returns></returns>
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            // To call a sound you do the following
            Main.PlaySound(PrimarySounds[Main.rand.Next(PrimarySounds.Length)],
                (int)player.position.X, (int)player.position.Y);
            // Unless specific sounds should be more common than others, this exact line can be used for all PrimarySounds calls
            //     and changing PrimarySounds to SecondarySounds allows it to be used with SecondarySounds as well


            return true;    // Ignore
        }
    }
}