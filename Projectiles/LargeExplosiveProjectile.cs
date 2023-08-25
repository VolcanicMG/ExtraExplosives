using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using static ExtraExplosives.GlobalMethods;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class LargeExplosiveProjectile : ExplosiveProjectile
    {

        protected override string explodeSoundsLoc => "Sounds/Custom/Explosives/Large_Explosive_";
        protected override string goreFileLoc => "Gores/Explosives/basic-explosive_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LargeExplosive");
        }

        public override void SafeSetDefaults()
        {
            pickPower = 50;
            radius = 20;
            Projectile.tileCollide = true;
            Projectile.width = 32;
            Projectile.height = 38;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            explodeSounds = new SoundStyle[3];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            //Create Bomb Sound
            SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)]);

            //Create Bomb Dust
            DustEffects();

            Explosion();
            ExplosionDamage();

            //Create Bomb Damage
            //ExplosionDamage(20f * 2f, projectile.Center, 450, 40, projectile.owner);

            //Create Bomb Explosion
            //CreateExplosion(projectile.Center, 20);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(-2f, 0f);
            Vector2 gVel2 = new Vector2(0f, -2f);
            gVel1 = gVel1.RotatedBy(Projectile.rotation);
            gVel2 = gVel2.RotatedBy(Projectile.rotation);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1, Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale * 1.5f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2, Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale * 1.5f);
            gVel1 = gVel1.RotatedBy(Math.PI / 2);
            gVel2 = gVel2.RotatedBy(Math.PI / 2);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1, Mod.Find<ModGore>(goreFileLoc + "1").Type, Projectile.scale * 1.5f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2, Mod.Find<ModGore>(goreFileLoc + "2").Type, Projectile.scale * 1.5f);
        }

        /*private void CreateExplosion(Vector2 position, int radius)
		{
			for (int x = -radius; x <= radius; x++) //Starts on the X Axis on the left
			{
				for (int y = -radius; y <= radius; y++) //Starts on the Y Axis on the top
				{
					int xPosition = (int)(x + position.X / 16.0f);
					int yPosition = (int)(y + position.Y / 16.0f);

					if (Math.Sqrt(x * x + y * y) <= radius + 0.5 && (WorldGen.InWorld(xPosition, yPosition))) //Circle
					{
						ushort tile = Main.tile[xPosition, yPosition].type;
						if (!CanBreakTile(tile, PickPower)) //Unbreakable CheckForUnbreakableTiles(tile) ||
						{
						}
						else //Breakable
						{
							WorldGen.KillTile(xPosition, yPosition, false, false, false); //This destroys Tiles
							if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false); //This destroys Walls
						}
					}
				}
			}
		}*/
    }
}