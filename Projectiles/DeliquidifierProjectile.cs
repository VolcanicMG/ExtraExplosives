using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using static ExtraExplosives.GlobalMethods;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class DeliquidifierProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Deliquidefier_";
        protected override string goreFileLoc => "Gores/Explosives/deliquifyer_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deliquidifier");
        }

        public override void SafeSetDefaults()
        {
            pickPower = -2; // Override for nondestruction
            radius = 10;
            Projectile.tileCollide = true;
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            explodeSounds = new SoundStyle[4];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            //SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

            //Create Bomb Damage
            //ExplosionDamage(5f, projectile.Center, 70, 20, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 10);

            Explosion();

            //Create Bomb Dust
            DustEffects(type: 3, shake: false, dustType: 160, shader: GameShaders.Armor.GetSecondaryShader(39, Main.LocalPlayer));

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(2f, 0f);
            Vector2 gVel2 = new Vector2(-2f, -2f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale);
        }

        public override void Explosion()
        {
            Vector2 position = Projectile.Center;
            for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
            {
                for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
                    {
                        Main.tile[xPosition, yPosition].LiquidAmount = LiquidID.Water; //Removes Liquid
                        WorldGen.SquareTileFrame(xPosition, yPosition, true); //Updates Area
                    }
                }
            }
        }
    }
}

//Main.tile[xPosition, yPosition].liquid = Tile.Liquid_Water; //Breaks water instead of creating it
//Main.tile[(int)((position.X + i) / 16), (int)((position.Y + j) / 16)].liquid = 1;