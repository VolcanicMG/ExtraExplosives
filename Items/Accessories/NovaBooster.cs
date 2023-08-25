using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class NovaBooster : ModItem
    {
        //public static LegacySoundStyle EngineSound;
        public static SoundEffectInstance EngineSoundInstance;

        //public static LegacySoundStyle EndSound;
        public static SoundEffectInstance EndSoundInstance;

        private bool justUsed = false;
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Nova Booster");
            Tooltip.SetDefault("'The power of a dying star strapped to your back'");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(15, ));
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine Info = tooltips.LastOrDefault(t => t.Mod == "Terraria");

            if (Info != null)
            {
                Info.Text += "[c/FF0000: Press " + ExtraExplosives.TriggerBoost.GetAssignedKeys(InputMode.Keyboard)[0] + " to boost. 5s cooldown]";
            }
        }

        public override void SetDefaults()
        {

            Item.width = 38;
            Item.height = 38;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;

            //EndSound = Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Novabooster/NovaboosterEnd");
            //EngineSound = Mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/Novabooster/Novabooster");
            //if (!Main.dedServ && EngineSound != null && EndSound != null)
            //{
            //    EngineSound = EngineSound.WithVolume(0.3f);
            //    EndSound = EndSound.WithVolume(0.4f);
            //}
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                //if (EngineSoundInstance == null)
                    //EngineSoundInstance = SoundEngine.PlaySound(EngineSound, (int)player.Center.X, (int)player.Center.Y);

                if (EngineSoundInstance.State != SoundState.Playing)
                    EngineSoundInstance.Play();
                justUsed = true;
                CreateEngineDust(player);

            }
            else
            {
                if (EngineSoundInstance != null && EngineSoundInstance.State == SoundState.Playing)
                    EngineSoundInstance.Stop();

                if (justUsed)
                {
                    //if (EndSoundInstance == null)
                        //EndSoundInstance = SoundEngine.PlaySound(EndSound, (int)player.Center.X, (int)player.Center.Y);

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

            if (acceleration >= 50)
            {
                acceleration = 50;
            }
        }

        public void CreateEngineDust(Player player)
        {
            //create dust for the bottom
            if (player.direction < 0)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = new Vector2(player.position.X + 35, player.position.Y + 30);
                dust = Main.dust[Terraria.Dust.NewDust(position, 15, 15, 6, 6.842105f, 0f, 0, new Color(255, 176, 0), Main.rand.NextFloat(1f, 3f))];

            }
            else
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = new Vector2(player.position.X - 35, player.position.Y + 30);
                dust = Main.dust[Terraria.Dust.NewDust(position, 15, 15, 6, -6.842105f, 0f, 0, new Color(255, 176, 0), Main.rand.NextFloat(1f, 3f))];
            }


            //set the top
            if (player.velocity.X < -5)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = new Vector2(player.position.X + 25, player.position.Y - 3);
                dust = Main.dust[Terraria.Dust.NewDust(position, 8, 8, 6, 10f, 0f, 0, new Color(255, 176, 0), Main.rand.NextFloat(.8f, 1.5f))];
                dust.noGravity = true;
            }
            else if (player.velocity.X > 5)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = new Vector2(player.position.X - 25, player.position.Y - 3);
                dust = Main.dust[Terraria.Dust.NewDust(position, 8, 8, 6, -10f, 0f, 0, new Color(255, 176, 0), Main.rand.NextFloat(.8f, 1.5f))];
                dust.noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 15);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 3);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(ItemID.WingsVortex, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

    }
}