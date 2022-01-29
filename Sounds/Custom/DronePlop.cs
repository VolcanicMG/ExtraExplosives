using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace ExtraExplosives.Sounds.Custom
{
    public class DronePlop : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            soundInstance = sound.CreateInstance();
            soundInstance.Volume = volume * .4f;
            soundInstance.Pan = pan;
            //soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
            return soundInstance;
        }
    }
}

