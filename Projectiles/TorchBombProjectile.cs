using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class TorchBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreName => "torch_gore";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Torch Bomb");
            //Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            Projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            Projectile.width = 10;   //This defines the hitbox width
            Projectile.height = 32; //This defines the hitbox height
            Projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 45; //The amount of time the projectile is alive for
            Projectile.damage = 0;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = Projectile.Center;
            //SoundEngine.PlaySound(SoundID.Item14, position);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(0.0f, -2.0f);
            Vector2 gVel2 = new Vector2(-1.0f, 2.0f);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}1").Type, Projectile.scale);
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(Projectile.rotation), Mod.Find<ModGore>($"{goreName}2").Type, Projectile.scale);

            float x = 0;
            float y = 0;
            float width = 20;
            float height = 20;

            int randomChance = 10;

            for (x = -width; x < width; x++)
            {
                for (y = -height; y < height; y++)
                {
                    if (Main.rand.Next(randomChance) == 1)
                        if (WorldGen.TileEmpty((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f)))
                            WorldGen.PlaceTile((int)(x + position.X / 16.0f), (int)(y + position.Y / 16.0f), TileID.Torches, false, false, -1, 0);
                }
                x = x + 2;
            }
        }
    }
}