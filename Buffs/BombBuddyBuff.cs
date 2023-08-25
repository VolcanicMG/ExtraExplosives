using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives.Buffs
{
    public class BombBuddyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Buddy");
            Description.SetDefault("It's a walking bomb!!");
            
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<ExtraExplosivesPlayer>().BombBuddy = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Pets.BombBuddy>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                //Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Pets.BombBuddy>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}