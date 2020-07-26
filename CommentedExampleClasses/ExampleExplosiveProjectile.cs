using ExtraExplosives.Projectiles;

namespace ExtraExplosives.CommentedExampleClasses
{
    /// <summary>
    /// This class is an example to show how ModProjectiles work and how ExplosiveProjectiles differ from them
    /// Everything in this class should be commented thoroughly enough that you can understand and adapt it into your own code when needed
    /// Due to this being a ExplosiveProjectile and not a ModProjectile some parts of the mod projectile class wont work
    ///     If you are unsure if a piece of code will work in a regular mod projectile, look at the comment or just try to use it
    ///         If it errors out it probably needs to be used in an explosive projectile
    ///
    /// While this file will show how to use the additions ExplosiveProjectile adds, it isnt meant to replace knowledge of C#
    /// If you are unsure how to code, look up a tutorial first
    ///
    /// For the important stuff, look for the TODO tag, everything else is just supplemental knowledge
    /// </summary>
    public class ExampleExplosiveProjectile : ExplosiveProjectile
    {
        // This first line is only meant to stop tML from loading this class (and failing because no assets are connected to it
        // It can be ignored
        public override bool Autoload(ref string name) => false;

        /// <summary>
        /// The first lines here are the sound and gore locations,
        /// These point to the 
        /// </summary>
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/atom_gore";
        
        public override void SafeSetDefaults()
        {
        }
    }
}