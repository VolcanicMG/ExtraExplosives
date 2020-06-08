using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;
using Terraria;

namespace ExtraExplosives.Sounds.Custom
{
	public class Plane : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			// By checking if the input soundInstance is playing, we can prevent the sound from firing while the sound is still playing, allowing the sound to play out completely. Non-ModSound behavior is to restart the sound, only permitting 1 instance.
			//if (soundInstance.State == SoundState.Stopped)
			//{
				
			//}

			soundInstance.Volume = volume * .4f;
			soundInstance.Pan = pan;
			soundInstance.IsLooped = true;
			//soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
			return soundInstance;
		}
	}
}
