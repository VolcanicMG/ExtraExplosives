using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ExtraExplosives
{
	public class ExtraExplosivesPlayer : ModPlayer
	{
		public int reforgeUIActive = 0;
		public bool detonate;

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			//Player player = Main.player[Main.myPlayer];

			if (ExtraExplosives.TriggerExplosion.JustReleased)
			{
				//ExtraExplosives.detonate = true;
				detonate = true;
				//Main.NewText("Detonate", (byte)30, (byte)255, (byte)10, false);
			}
			else
			{
				//ExtraExplosives.detonate = false;
				detonate = false;
			}

			if (ExtraExplosives.TriggerUIReforge.JustPressed) //check to see if the button was just pressed
			{

				reforgeUIActive++;
				
				if(reforgeUIActive == 5)
				{
					reforgeUIActive = 1;
				}
			}
			

			if (reforgeUIActive == 1) //check to see if the reforge bomb key was pressed
			{
				GetInstance<ExtraExplosives>().ExtraExplosivesReforgeBombInterface.SetState(new UI.ExtraExplosivesReforgeBombUI());
				reforgeUIActive++;

			}
			if (reforgeUIActive == 3)
			{
				GetInstance<ExtraExplosives>().ExtraExplosivesReforgeBombInterface.SetState(null);
				reforgeUIActive = 4;
			}

		}


	}
}