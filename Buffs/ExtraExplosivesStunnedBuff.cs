using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExtraExplosives.Buffs
{
    public class ExtraExplosivesStunnedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stunned");
            // Description.SetDefault("You can't move, attack, or place blocks");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            //canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Vector2 NPCPos = npc.Center; //npc pos
            if (npc.boss)
            {
            }
            else
            {
                Dust dust1;
                //dust
                Vector2 position1 = new Vector2(NPCPos.X, NPCPos.Y - npc.height + 10);
                dust1 = Main.dust[Terraria.Dust.NewDust(position1, 10, 10, 106, 0f, 0f, 171, new Color(33, 0, 255), 2.0f)];
                dust1.noGravity = true;
                dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
                dust1.noLight = false;

                //stop the npc if not a boss
                npc.velocity.X = 0;
                npc.velocity.Y = npc.velocity.Y + 0.3f;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Vector2 PlayerPos = player.Center; //player pos

            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {
                Filters.Scene.Activate("Bang", player.Center).GetShader().UseColor(255, 255, 255).UseOpacity(0.1f);
                //float progress = 0f;
                //Filters.Scene["Bang"].GetShader().UseProgress(progress).UseOpacity(0);
            }

            //add lighting
            Lighting.AddLight(player.position, new Vector3(255f, 255f, 255f));
            //Lighting.maxX = 5;
            //Lighting.maxY = 5;

            //spawn dust
            Dust dust1;
            Vector2 position1 = new Vector2(PlayerPos.X, PlayerPos.Y - player.height + 10);
            dust1 = Main.dust[Terraria.Dust.NewDust(position1, 10, 10, 106, 0f, 0f, 171, new Color(33, 0, 255), 2.0f)];
            dust1.noGravity = true;
            dust1.shader = GameShaders.Armor.GetSecondaryShader(116, Main.LocalPlayer);
            dust1.noLight = false;

            //stop the player
            player.controlUseItem = false;
            player.controlUseTile = false;
            player.velocity.X = 0;
            player.velocity.Y = player.velocity.Y + 0.3f;
        }
    }
}