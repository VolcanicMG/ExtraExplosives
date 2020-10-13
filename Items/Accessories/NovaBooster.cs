using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class NovaBooster : ModItem
    {
        private LegacySoundStyle phaseSound; //don't @ me im too lazy...
        private SoundEffectInstance phaseSoundInstance;

        private LegacySoundStyle EndSound;
        private SoundEffectInstance EndSoundInstance;

        private bool justUsed = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nova Booster");
            Tooltip.SetDefault("The power of a dying star,\n" +
                               "strapped to your back");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(15, ));
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;

            EndSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Novabooster/NovaboosterEnd");
            phaseSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Novabooster/Novabooster");
            if (!Main.dedServ)
            {
                phaseSound = phaseSound.WithVolume(0.3f);
                EndSound = EndSound.WithVolume(0.4f);
            }
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            
            if(inUse)
            {
                if (phaseSoundInstance == null)
                    phaseSoundInstance = Main.PlaySound(phaseSound, (int)player.Center.X, (int)player.Center.Y);

                if (phaseSoundInstance.State != SoundState.Playing)
                    phaseSoundInstance.Play();
                justUsed = true;
            }
            else
            {
                if (phaseSoundInstance != null && phaseSoundInstance.State == SoundState.Playing)
                    phaseSoundInstance.Stop();

                if(justUsed)
                {
                    if (EndSoundInstance == null)
                        EndSoundInstance = Main.PlaySound(EndSound, (int)player.Center.X, (int)player.Center.Y);

                    if (EndSoundInstance.State != SoundState.Playing)
                        EndSoundInstance.Play();
                    justUsed = false;
                }
            }

            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //player.wings = item.type;
            player.EE().novaBooster = true;
            player.wingTimeMax = 300;

        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.7f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 2;
            maxAscentMultiplier = 2f;
            constantAscend = 1f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 12; //12 is max without messing with dashing
            acceleration *= 3f;

            if(acceleration >= 50)
            {
                acceleration = 50;
            }
        }

    }
}