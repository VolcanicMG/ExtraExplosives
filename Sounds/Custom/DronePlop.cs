using IL.Terraria;
using Microsoft.Xna.Framework.Audio;
using System.Dynamic;
using Terraria.ModLoader;

<<<<<<< HEAD
namespace ExtraExplosives.Sounds.Custom
=======
namespace ExtraExplosives.Sounds.Item
>>>>>>> Charlie's-Uploads
{
	public class DronePlop : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
<<<<<<< HEAD
			soundInstance.Volume = volume * .4f;
=======
			soundInstance.Volume = volume * .8f;
>>>>>>> Charlie's-Uploads
			soundInstance.Pan = pan;
			//soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
			return soundInstance;
		}
	}
}

