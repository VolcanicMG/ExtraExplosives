using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace ExtraExplosives.Sounds.Custom
{
    public class BigDynamite : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            soundInstance = sound.CreateInstance();
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            //soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
            return soundInstance;
        }
    }
}

