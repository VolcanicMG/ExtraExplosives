using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ExtraExplosives.Projectiles
{
    public class TorchBombProjectile : ExplosiveProjectile
    {
        protected override string explodeSoundsLoc => "n/a";
        protected override string goreFileLoc => "Gores/Explosives/torch_gore";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torch Bomb");
            //Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            projectile.width = 10;   //This defines the hitbox width
            projectile.height = 32; //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 45; //The amount of time the projectile is alive for
            projectile.damage = 0;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);

            //Create Bomb Gore
            Vector2 gVel1 = new Vector2(0.0f, -2.0f);
            Vector2 gVel2 = new Vector2(-1.0f, 2.0f);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel1), gVel1.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "1"), projectile.scale);
            Gore.NewGore(projectile.position + Vector2.Normalize(gVel2), gVel2.RotatedBy(projectile.rotation), mod.GetGoreSlot(goreFileLoc + "2"), projectile.scale);

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