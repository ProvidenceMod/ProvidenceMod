using Microsoft.Xna.Framework;
using Providence.Content.Buffs.StatDebuffs;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Providence.ProvidenceUtils;
using static Terraria.ModLoader.ModContent;

namespace Providence
{
	public class ProvidencePlayer : ModPlayer
	{
		public Vector2[] oldPos = new Vector2[10];

		public bool dashing;
		public int dashMod;
		public int dashTimeMod;
		public int dashModDelay = 60;

		public bool abilityPing;

		public float DR;

		public bool starreaverArmor;

		public bool heartOfReality;

		// Buffs
		public bool pressureSpike;
		public bool intimidated;

		public override void ResetEffects()
		{
			DR = 0f;
			dashMod = 0;
			dashTimeMod = 0;

			// Debuffs.
			intimidated = false;

			// Armor
			starreaverArmor = false;

			// Accessories.
			heartOfReality = false;
		}
		public override void PreUpdate()
		{
			if (intimidated = IsThereABoss().bossExists)
				Player.AddBuff(BuffType<Intimidated>(), 2);
			else
				Player.ClearBuff(BuffType<Intimidated>());
		}
		public override void PostUpdate()
		{
			Player.UpdatePositionCache();
		}
		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			damage = (int)(damage * ProvidenceMath.DiminishingDRFormula(DR));
		}
		public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
		{
			damage = (int)(damage * ProvidenceMath.DiminishingDRFormula(DR));
		}
		public override void PostUpdateRunSpeeds()
		{
			if (dashMod > 0 && dashModDelay <= 0)
				ModDashMovement();
			else
				dashModDelay--;
		}
		public void ModDashMovement()
		{
			string dashDir = string.Empty;
			if (dashMod == 1 && Player.dash == 0)
			{
				Player.eocDash = Player.dashTime;
				Player.armorEffectDrawShadowEOCShield = true;
				const float dashStrength = 12f;
				const int delayTime = 60;
				if (!dashing && Player.dashTime == 0)
					dashDir = "";
				if (Player.dashTime > 0)
					Player.dashTime--;
				if (Player.controlRight && Player.releaseRight)
				{
					if (Player.dashTime > 0 && dashDir == "right")
					{
						Player.dashTime = 0;
						dashModDelay = delayTime;
						dashing = true;
					}
					else
					{
						dashDir = "right";
						Player.dashTime = 15;
					}
				}
				else if (Player.controlLeft && Player.releaseLeft)
				{
					if (Player.dashTime > 0 && dashDir == "left")
					{
						Player.dashTime = 0;
						dashModDelay = delayTime;
						dashing = true;
					}
					else
					{
						dashDir = "left";
						Player.dashTime = 15;
					}
				}
				//else if (player.controlUp && player.releaseUp)
				//{
				//  if (player.dashTime > 0 && dashDir == "up")
				//  {
				//    player.dashTime = 0;
				//    dashModDelay = delayTime;
				//    dashing = true;
				//  }
				//  else
				//  {
				//    dashDir = "up";
				//    player.dashTime = 15;
				//  }
				//}
				//else if (player.controlDown && player.releaseDown)
				//{
				//  if (player.dashTime > 0 && dashDir == "down")
				//  {
				//    player.dashTime = 0;
				//    dashModDelay = delayTime;
				//    dashing = true;
				//  }
				//  else
				//  {
				//    dashDir = "down";
				//    player.dashTime = 15;
				//  }
				//}
				if (dashing)
				{
					switch (dashDir)
					{
						case "left":
							Player.velocity.X = -dashStrength;
							break;
						case "right":
							Player.velocity.X = dashStrength;
							break;
							//case "up":
							//  player.velocity.Y = -dashStrength;
							//  break;
							//case "down":
							//  player.velocity.Y = dashStrength;
							//  break;
					}
					Player.dashDelay = 60;
				}
				else
				{
					return;
				}
				int dashDirInt = dashDir == "left" || dashDir == "right" ? 1 : 0;
				Point tileCoordinates1 = (Player.Center + new Vector2((dashDirInt * Player.width / 2) + 2, (float)(((double)Player.gravDir * (double)-Player.height / 2.0) + ((double)Player.gravDir * 2.0)))).ToTileCoordinates();
				Point tileCoordinates2 = (Player.Center + new Vector2((dashDirInt * Player.width / 2) + 2, 0.0f)).ToTileCoordinates();
				if (WorldGen.SolidOrSlopedTile(tileCoordinates1.X, tileCoordinates1.Y) || WorldGen.SolidOrSlopedTile(tileCoordinates2.X, tileCoordinates2.Y))
					Player.velocity.X /= 2f;
			}
		}
		public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
		{
			//createItem(ItemType<StarterBag>())

			Item createItem(int type)
			{
				Item obj = new Item();
				obj.SetDefaults(type, false);
				return obj;
			}
		}
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)Player.whoAmI);
			packet.Send(toWho, fromWho);
		}
	}
}