using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using static ProvidenceMod.ProvidenceUtils;

namespace ProvidenceMod.UI
{
	internal class BossHealth : UIState
	{
		// This assigs the current scale value to a variable so that we can later reassign it and recalculate,
		// This allows us to properly scale our UI to fit with the inventory
		public float oldScale = Main.inventoryScale;
		//
		//
		// This allows us to turn our UI on and off as needed, as it is the variable that is checked before drawing is performed in ProvidenceMod.cs
		public static bool visible = true;
		//
		//
		// This is the UIElement that we will append our UI children to
		// You can alternatively use a UIPanel if you would like a panel background
		private BossHealthElement area;
		//
		//
		// This is the image for the frame around the health bar
		private UIImage frame;
		//
		//
		// This is the bloom on the tip of the current HP bar
		private UIImage bloom;
		//
		//
		// This is the health bar. It is a framed UIImage to allow us to crop it down to the correct size
		private UIImageFramed health;
		//
		//
		// This is the hit bar. It is a framed UIImage for the same reason.
		private UIImageFramed combo;
		private Rectangle healthRect;
		private Rectangle comboRect;
		//
		//
		// This is the cooldown timer (in frames) for how long to wait after the health changes before beginning to decrease
		private int cooldown = 75;
		//
		//
		// This is a life array, you can see how the life values are written to it in the Update method
		private readonly int[] lifeArray = new int[3] { 0, 0, 0 };
		//
		//
		// This is the variable that allows our boss health bar to run, but only if there is a boss
		private bool boss = false;
		//
		//
		// This lets us set the initial health value to the array only once, and then never again until the next boss
		private bool arraySet = false;
		//
		//
		// This is the NPC variable that contains the identity for the Boss
		private NPC bossNPC;
		private NPC head;
		private NPC hand;
		private NPC hand2;
		private int bossHealth;
		private int bossHealthMax;
		private int comboDMG;
		private int comboCounter;

		public override void OnInitialize()
		{
			// Here we initialize the area and set our variables
			area = new BossHealthElement();
			// The first valoe is the pixel count fron the left or top, the second value is the percentage in case that's easier to use
			area.Left.Set((Main.screenWidth - 1000f) / 2, 0f);
			area.Top.Set(Main.screenHeight - 100, 0f);
			area.Width.Set(1000f, 0f);
			area.Height.Set(46f, 0f);

			frame = new UIImage(GetTexture("ProvidenceMod/ExtraTextures/UI/BossFrame"));
			frame.Top.Set(0, 0f);
			frame.Left.Set(0, 0f);
			frame.Width.Set(1000f, 0f);
			frame.Height.Set(46f, 0f);

			bloom = new UIImage(GetTexture("ProvidenceMod/ExtraTextures/UI/BossBloom"));
			bloom.Top.Set(19f, 0f);
			bloom.Left.Set(31f, 0f);
			bloom.Width.Set(7f, 0f);
			bloom.Height.Set(12f, 0f);

			// We give the rectangle the same dimensions as our health bar so that it always draws all of it unless told otherwise
			healthRect = new Rectangle(0, 0, 924, 6);
			health = new UIImageFramed(GetTexture("ProvidenceMod/ExtraTextures/UI/BossHP"), healthRect);
			health.Top.Set(22f, 0f);
			health.Left.Set(34f, 0f);
			health.Width.Set(924f, 0f);
			health.Height.Set(6f, 0f);

			comboRect = new Rectangle(0, 0, 924, 6);
			combo = new UIImageFramed(GetTexture("ProvidenceMod/ExtraTextures/UI/BossCombo"), comboRect);
			combo.Top.Set(22f, 0f);
			combo.Left.Set(34f, 0f);
			combo.Width.Set(924f, 0f);
			combo.Height.Set(6f, 0f);

			// Don't forget to append the area, otherwise your UI wont draw
			// You should also append any type that starts with UI (Like UIImage or UIImageFramed)
			// You can hold off on this if you only want it to be displayed when a certain condition is fulfilled
			Append(area);
		}
		public override void Update(GameTime gameTime)
		{
			// Always add this to your UI
			base.Update(gameTime);
			// Here we call our utility method to check if there is a boss, and we ask for Item1 which is the bossExists boolean
			// This is a Tuple, which is basically like a normal method that can return a value, 
			// but you can ask for a certain value to be returned instead of being limited to one
			if (IsThereABoss().Item1)
			{
				// If there is a boss, we append our UI children so that it displays the boss health bar 
				area.Append(frame);
				area.Append(combo);
				area.Append(health);
				area.Append(bloom);
			}
			else if (!IsThereABoss().Item1)
			{
				// This lets us remove all of the UI children in the case that the boss is dead or that there is no boss
				area.RemoveAllChildren();
			}
			// This method lets us scale the boss health bar accordingly.
			// We set the initial scale value earlier, but if the current one doesnt match the one we set earlier, 
			// then we reset it and recalculate our scaling
			if (oldScale != Main.inventoryScale)
			{
				oldScale = Main.inventoryScale;
				Recalculate();
			}
			var parentSpace = area.Parent.GetDimensions().ToRectangle();
			if (!GetDimensions().ToRectangle().Intersects(parentSpace))
			{
				Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
				Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
				// Recalculate forces the UI system to do the positioning math again.
				Recalculate();
			}
			// Here we check the entire NPC array, this lets us set our boss variables from earlier
			foreach (NPC npc in Main.npc)
			{
				if (npc.type == NPCID.MoonLordCore)
				{
					bossNPC = npc;
					bossHealth = npc.life;
					bossHealthMax = npc.lifeMax;
					boss = true;
					area.boss = npc;
				}
				if (npc.type == NPCID.MoonLordHead)
				{
					head = npc;
				}
				if (npc.type == NPCID.MoonLordHand)
				{
					hand = npc;
				}
				if (npc.type == NPCID.MoonLordHand && npc != hand)
				{
					hand2 = npc;
				}
				else if (npc.active && npc.boss && npc.type != NPCID.MoonLordCore && npc.type != NPCID.MoonLordHead && npc.type != NPCID.MoonLordHand && npc.type != NPCID.MoonLordFreeEye && npc.type != NPCID.MoonLordLeechBlob)
				{
					bossHealth = npc.life;
					bossHealthMax = npc.lifeMax;
					bossNPC = npc;
					boss = true;
					area.boss = npc;
				}
			}
			// This only runs if there is a boss
			if (boss)
			{
				if (bossNPC.type == NPCID.MoonLordCore || bossNPC.type == NPCID.MoonLordHead || bossNPC.type == NPCID.MoonLordHand)
				{
					if (bossNPC != null && head != null && hand != null && hand2 != null)
					{
						bossHealth = bossNPC.life + hand.life + hand2.life + head.life;
						bossHealthMax = bossNPC.lifeMax + hand.lifeMax + hand2.life + head.lifeMax;
					}
				}
				else
				{
					bossHealth = bossNPC.life;
				}
				area.quotient = ((float)bossHealth) / bossHealthMax;
				bloom.Left.Set(31f + healthRect.Width, 0f);
				if (Main.playerInventory)
				{ }
				if (!arraySet)
				{
					lifeArray[2] = lifeArray[1];
					lifeArray[1] = lifeArray[0];
					lifeArray[0] = bossHealth;
					arraySet = true;
				}
				if (comboRect.Width < healthRect.Width)
				{
					comboRect.Width = healthRect.Width;
				}
				if (comboDMG >= 0)
				{
					area.comboDMG = comboDMG;
					area.comboPos = new Vector2(area.Left.Pixels + 34 + healthRect.Width + ((comboRect.Width - healthRect.Width) / 2), area.Top.Pixels + 7);
				}
				// This checks if the current boss life is less than the previous recorded value
				// If it is, it resets the cooldown for the hit bar movement
				if (bossHealth < lifeArray[0])
				{
					lifeArray[2] = lifeArray[1];
					lifeArray[1] = lifeArray[0];
					lifeArray[0] = bossHealth;
					cooldown = 75;
					comboDMG += lifeArray[1] - lifeArray[0];
					area.comboVisible = true;
					comboCounter = 0;
				}
				else if (bossHealth == lifeArray[0])
				{
					if (cooldown > 0) cooldown--;
				}
				if (cooldown == 0 && comboRect.Width != healthRect.Width)
				{
					if ((comboRect.Width - healthRect.Width) * 0.05f < 1)
					{
						comboRect.Width--;
					}
					else
					{
						comboRect.Width -= (int)((comboRect.Width - healthRect.Width) * 0.05f);
					}
					combo.SetFrame(comboRect);
				}
				if ((cooldown == 0 && comboDMG != 0) || (healthRect.Width == comboRect.Width && comboDMG != 0))
				{
					comboDMG -= (int)(comboDMG * 0.05f);
					comboDMG--;
				}
				if (comboDMG == 0 && comboCounter != 120)
				{
					comboCounter++;
				}
				if (comboCounter == 120)
				{
					area.comboVisible = false;
				}
				// Resetting everything after the boss is dead, make sure to do this if your UI is dependent on variables and changing things around
				if (bossHealth <= 0 || bossNPC == null)
				{
					boss = false;
					bossNPC = null;
					area.boss = null;
					bossHealth = 0;
					bossHealthMax = 0;
					comboRect.Width = 924;
					combo.SetFrame(comboRect);
					cooldown = 75;
					lifeArray[0] = 0;
					lifeArray[1] = 0;
					lifeArray[2] = 0;
					arraySet = false;
					comboDMG = 0;
					comboCounter = 0;
					area.comboVisible = false;
					area.comboPos.X = area.Left.Pixels + 34;
				}
			}
			// Main Bar
			area.quotient = Utils.Clamp(area.quotient, 0f, 1f);
			healthRect.Width = (int)(924 * area.quotient);
			health.SetFrame(healthRect);
		}
	}
}