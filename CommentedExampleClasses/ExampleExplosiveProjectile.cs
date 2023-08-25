using ExtraExplosives.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace ExtraExplosives.CommentedExampleClasses
{
    [Autoload(false)]
    public class ExampleExplosiveProjectile : ExplosiveProjectile
    {
        // This first line is only meant to stop tML from loading this class (and failing because no assets are connected to it
        // It can be ignored
        public bool Autoload(ref string _) => false;

        /// <summary>
        /// This represents the filepath of the sounds which should be played when the projectile explodes
        /// For a more detailed explanation see CommentedExplosiveWeapon or reference other ExplosiveWeapons or ExplosiveProjectiles
        /// </summary>
        protected override string explodeSoundsLoc => null;

        /// <summary>
        /// Same as above except for gore locations
        /// </summary>
        protected override string goreFileLoc => "Gores/Explosives/atom_gore";

        /// <summary>
        /// Replacement for SetDefaults, functions identically to other SafeSetDefaults
        /// See CommentedExplosiveItem for more info
        /// </summary>
        public override void SafeSetDefaults()
        {
        }

        /// <summary>
        /// Used to override sealed attributes
        /// See CommentedExplosiveItem for more info
        /// </summary>
        public override void DangerousSetDefaults()
        {
        }

        /*
         * The next three functions dictate the effects the projectiles have when they collide (hit) in various contexts
         * This does not change how they function when explode, see Explode() and ExplosionDamage() for those
         * These functions only change what the bomb does when it physically collides
         * Disabled in most cases to avoid projectile death after hitting an npc or player
         */

        /// <summary>
        /// How the projectile acts when it hits a player in a non-pvp context
        /// </summary>
        /// <param name="target">The player being hit</param>
        /// <param name="damage">The damage to be dealt</param>
        /// <param name="crit">If the hit was a crit</param>
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        }

        /// <summary>
        /// How the projectile acts when it hits a player in a pvp context
        /// </summary>
        /// <param name="target">The player being hit</param>
        /// <param name="damage">The damage to be dealt</param>
        /// <param name="crit">If the hit was a crit</param>
        public override void OnHitPvp(Player target, int damage, bool crit)/* tModPorter Note: Removed. Use OnHitPlayer and check info.PvP */
        {
        }

        /// <summary>
        /// How the projectile acts when it hits an npc (either hostile or friendly)
        /// Whether the hit is successful is usually dictated by the projectile.friendly and projectile.hostile attributes
        /// </summary>
        /// <param name="target">The npc being hit</param>
        /// <param name="damage">The damage to be dealt</param>
        /// <<param name="knockback">The knockback value</param>
        /// <param name="crit">If the hit was a crit</param>
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        }

        /// <summary>
        /// The Explosion function dictates the behavior of the projectiles explosion (not the damage however)
        /// Predefined in the base class is a basic explosion function (WIP) which causes a circular explosion
        /// The method used is optimized to avoid lag and so can be used with very large values
        /// If more specialized functionality is required or something entirely different this is the method to change
        /// Override it like any other function
        /// It is best practice to call the Explosion function in the Kill() function to avoid double calls
        /// This can be changed if you want some additional functionality (like multiple explosions)
        /// However the actual effects of the explosion (block breaking, sfx, spawning npcs, etc) should all be done here
        /// Damage should be done from ExplosionDamage and not here
        /// </summary>
        public override void Explosion()
        {
        }

        /// <summary>
        /// Used to inflict damage on players and npcs
        /// The current base implementation cycles through the player and npc arrays in Main and checks distance
        /// If the targeted player (without blast shielding) or npc is within range, it deals damage
        /// To change how damage is dealt, override and modify this function
        /// This damage function should be called from Kill() unless damage is meant to be dealt several times
        /// </summary>
        public override void ExplosionDamage()
        {
        }
    }
}