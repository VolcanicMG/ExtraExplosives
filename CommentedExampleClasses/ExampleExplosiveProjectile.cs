using ExtraExplosives.Projectiles;

namespace ExtraExplosives.CommentedExampleClasses
{
    /// <summary>
    /// This class is an example to show how ModProjectiles work and how ExplosiveProjectiles differ from them
    /// Everything in this class should be commented thoroughly enough that you can understand and adapt it into your own code when needed
    /// Due to this being a ExplosiveProjectile and not a ModProjectile some parts of the mod projectile class wont work
    ///     These areas will be marked with a TODO (which your IDE should highlight in blue, just like this!)
    ///         If the above TO DO isnt highlighted, just ctrl-f and search TO DO without the space to find all the differences between the two classes
    ///
    /// The first important thing to note is how to inherit from a specific class
    /// C# doesnt use a keyword like java or python, instead using the : to signify inheritance
    /// 
    /// </summary>
    public class ExampleExplosiveProjectile : ExplosiveProjectile
    {
        public override bool Autoload(ref string name)
        {
            return false;    // This ensures this projectile isnt loaded by tML
        }

        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/atom_gore";
        public override void SafeSetDefaults()
        {
        }
    }
}