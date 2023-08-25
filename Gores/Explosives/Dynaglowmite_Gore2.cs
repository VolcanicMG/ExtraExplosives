using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ExtraExplosives.Gores.Explosives
{
    class Dynaglowmite_Gore2 : ModGore
    {
        public override void OnSpawn(Gore gore, IEntitySource source)
        {
            gore.sticky = true;
            gore.timeLeft = 60 * 8;
            gore.numFrames = 1;
            gore.frame = 0;
            gore.light = 0.4f;
        }
    }
}
