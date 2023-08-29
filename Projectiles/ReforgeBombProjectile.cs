using ExtraExplosives.Dusts;
using ExtraExplosives.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    public class ReforgeBombProjectile : ExplosiveProjectile
    {
        //Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        //Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");
        protected override string explodeSoundsLoc => "ExtraExplosives/Assets/Sounds/Custom/Explosives/Reforge_Bomb_";
        protected override string goreName => "n/a";
        internal static bool CanBreakWalls;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ReforgeBomb");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SafeSetDefaults()
        {
            IgnoreTrinkets = true;
            Projectile.tileCollide = true; //checks to see if the projectile can go through tiles
            Projectile.width = 22;   //This defines the hitbox width
            Projectile.height = 22; //This defines the hitbox height
            Projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            Projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            Projectile.timeLeft = 100; //The amount of time the projectile is alive for
            explodeSounds = new SoundStyle[2];
            for (int num = 1; num <= explodeSounds.Length; num++)
            {
                explodeSounds[num - 1] = new SoundStyle(explodeSoundsLoc + num);
            }
        }

        public override void Kill(int timeLeft)
        {
            var player = Main.player[Projectile.owner].GetModPlayer<ExtraExplosivesPlayer>();
            Vector2 position = Projectile.Center;

            if (Projectile.owner == Main.myPlayer)
            {
                player.reforge = true;
            }

            if (ExtraExplosivesReforgeBombUI.IsVisible == false)
            {
                if (player.reforge == true)
                {
                    Main.NewText("Nothing to reforge, press " + "'" + ExtraExplosives.TriggerUIReforge.GetAssignedKeys(InputMode.Keyboard)[0].ToString() + "'" + " to toggle reforge UI, or set it in settings");
                }
                player.reforge = false;
            }
            //Item.NewItem(position, new Vector2(20, 20), ItemID.GoldAxe, 1, false, -2);

            //SoundEngine.PlaySound(explodeSounds[Main.rand.Next(explodeSounds.Length)], new Vector2(position.X, position.Y));

            for (int i = 0; i < 100; i++) //spawn dust
            {
                if (Main.rand.NextFloat() < ExtraExplosives.dustAmount)
                {
                    if (Main.rand.NextFloat() < 0.3f)
                    {
                        Vector2 position1 = new Vector2(position.X - 26 / 2, position.Y - 24 / 2);
                        Dust.NewDust(position1, 26, 24, ModContent.DustType<ReforgeBombDust>());

                        Dust dust;
                        Vector2 position2 = new Vector2(position.X - 105 / 2, position.Y - 105 / 2);
                        dust = Main.dust[Terraria.Dust.NewDust(position2, 105, 105, 1, 0f, 0f, 0, new Color(255, 255, 255), 1.4f)];
                        if (Vector2.Distance(dust.position, Projectile.Center) > 52) dust.active = false;
                        else
                        {
                            dust.noGravity = true;
                            dust.fadeIn = 1f;
                        }

                        Dust dust2;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position3 = new Vector2(position.X - 131 / 2, position.Y - 131 / 2);
                        dust2 = Main.dust[Terraria.Dust.NewDust(position3, 131, 131, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
                        if (Vector2.Distance(dust2.position, Projectile.Center) > 115) dust2.active = false;
                        else
                        {
                            dust2.noGravity = true;
                            dust2.position += dust2.velocity;
                        }
                    }
                }
            }
        }
    }
}