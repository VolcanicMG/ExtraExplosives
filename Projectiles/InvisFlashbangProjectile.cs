using ExtraExplosives.Buffs;
using ExtraExplosives.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Projectiles
{
    class InvisFlashbangProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InvisFlashbangProjectile");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override void SetDefaults()
        {
            projectile.tileCollide = false;
            projectile.width = 10;   //This defines the hitbox width
            projectile.height = 20;    //This defines the hitbox height
            projectile.aiStyle = 0;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.hostile = true;
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 10; //The amount of time the projectile is alive for
            projectile.Opacity = 0f;
            projectile.scale = 45 * 2; //DamageRadiu
        }

        public override string Texture => "ExtraExplosives/Projectiles/ExplosionDamageProjectile";

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.Confused, 300);
            target.AddBuff(BuffID.Confused, 300);
            target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);

            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            //Main.NewText("Knockback " + projectile.knockBack);
            //Main.NewText("Direction " + FlashbangItem.Direction);
            //Main.NewText("PlayerDirection " + target.direction);
            if (target.direction == 1 && FlashbangItem.Direction == 1 && projectile.knockBack == 0) //left side
            {
                target.AddBuff(BuffID.Confused, 300);
                target.AddBuff(BuffID.Dazed, 300);
                target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
                //Main.NewText("Hit on the left");
            }

            if (target.direction == 1 && FlashbangItem.Direction == -1 && projectile.knockBack == 0) //left side
            {
                target.AddBuff(BuffID.Confused, 300);
                target.AddBuff(BuffID.Dazed, 300);
                target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
                //Main.NewText("Hit on the left");
            }

            if (target.direction == -1 && FlashbangItem.Direction == -1 && projectile.knockBack >= 1) //right side
            {
                target.AddBuff(BuffID.Confused, 300);
                target.AddBuff(BuffID.Dazed, 300);
                target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
               //Main.NewText("Hit on the right");
            }

            if (target.direction == -1 && FlashbangItem.Direction == 1 && projectile.knockBack >= 1) //right side
            {
                target.AddBuff(BuffID.Confused, 300);
                target.AddBuff(BuffID.Dazed, 300);
                target.AddBuff(ModContent.BuffType<ExtraExplosivesStunnedBuff>(), 90);
                //Main.NewText("Hit on the right");
            }


            base.OnHitPlayer(target, damage, crit);
        }

        public override void Kill(int timeLeft)
        {

            

        }


    }
}
