using Terraria;
using Terraria.ModLoader;
using UnbiddenMod.UI;
using static Terraria.ModLoader.Mod;
using static UnbiddenMod.UnbiddenNPC;
using static UnbiddenMod.UnbiddenPlayer;
using static UnbiddenMod.UnbiddenProjectile;

namespace UnbiddenMod
{
	public class UnbiddenMod : Mod
	{
		public override void Load()
        {
            // this makes sure that the UI doesn't get opened on the server
            // the server can't see UI, can it? it's just a command prompt
            /*if (!Main.dedServ)
            {
                HealthUI = new HealthUI();
                somethingUI.Initialize();
                somethingInterface = new UserInterface();
                somethingInterface.SetState(somethingUI);
            }*/
        }
	}
}