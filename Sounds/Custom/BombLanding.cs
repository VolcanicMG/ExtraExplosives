using IL.Terraria;
using Microsoft.Xna.Framework.Audio;
using System.Dynamic;
using Terraria.ModLoader;

namespace ExtraExplosives.Sounds.Item
{
	public class BombLanding : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * .8f;
			soundInstance.Pan = pan;
			//soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
			return soundInstance;
		}
	}
}

