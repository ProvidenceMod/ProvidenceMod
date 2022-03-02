using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Providence.Projectiles;
using static Providence.ProvidenceUtils;
using Providence.TexturePack;

namespace Providence
{
	public class ProvidenceGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		public float[] extraAI = new float[6] { 0, 0, 0, 0, 0, 0 };

		public int element = -1;

		public Vector2[] oldCen = new Vector2[10] { new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f) };

		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			for (int combatIndex2 = 99; combatIndex2 >= 0; --combatIndex2)
			{
				CombatText combatText = Main.combatText[combatIndex2];
				if ((combatText.lifeTime == 60 || combatText.lifeTime == 120) && combatText.alpha == 1.0)
				{
					if (combatText.color == CombatText.DamagedHostile || combatText.color == CombatText.DamagedHostileCrit)
					{
						switch (projectile.Providence().element)
						{
							case 0:
								Main.combatText[combatIndex2].color = new Color(238, 74, 89);
								break;
							case 1:
								Main.combatText[combatIndex2].color = new Color(238, 74, 204);
								break;
							case 2:
								Main.combatText[combatIndex2].color = new Color(238, 226, 74);
								break;
							case 3:
								Main.combatText[combatIndex2].color = new Color(74, 95, 238);
								break;
							case 4:
								Main.combatText[combatIndex2].color = new Color(74, 238, 137);
								break;
							case 5:
								Main.combatText[combatIndex2].color = new Color(145, 74, 238);
								break;
							case 6:
								Main.combatText[combatIndex2].color = new Color(255, 216, 117);
								break;
							case 7:
								Main.combatText[combatIndex2].color = new Color(96, 0, 188);
								break;
						}
					}
				}
			}
		}
		public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
		{
			for (int combatIndex2 = 99; combatIndex2 >= 0; --combatIndex2)
			{
				CombatText combatText = Main.combatText[combatIndex2];
				if ((combatText.lifeTime == 60 || combatText.lifeTime == 120) && combatText.alpha == 1.0)
				{
					if (combatText.color == CombatText.DamagedFriendly || combatText.color == CombatText.DamagedFriendlyCrit)
					{
						switch (projectile.Providence().element)
						{
							case 0:
								Main.combatText[combatIndex2].color = new Color(238, 74, 89);
								break;
							case 1:
								Main.combatText[combatIndex2].color = new Color(238, 74, 204);
								break;
							case 2:
								Main.combatText[combatIndex2].color = new Color(238, 226, 74);
								break;
							case 3:
								Main.combatText[combatIndex2].color = new Color(74, 95, 238);
								break;
							case 4:
								Main.combatText[combatIndex2].color = new Color(74, 238, 137);
								break;
							case 5:
								Main.combatText[combatIndex2].color = new Color(145, 74, 238);
								break;
							case 6:
								Main.combatText[combatIndex2].color = new Color(255, 216, 117);
								break;
							case 7:
								Main.combatText[combatIndex2].color = new Color(96, 0, 188);
								break;
						}
					}
				}
			}
		}
		public override void SetDefaults(Projectile projectile)
		{
			switch (projectile.type)
			{
				case ProjectileID.Flames:
					projectile.Providence().element = 0; // Fire
					break;
			}
		}
	}
}