// <image url="https://i.vgy.me/fiiTlx.png" scale="0.25" />
using System;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ProvidenceMod
{
	public static class ProvidenceUtils
	{
		/// <summary>References the ProvidencePlayer instance. Shorthand for ease of use.</summary>
		public static ProvidencePlayer Providence(this Player player) => player.GetModPlayer<ProvidencePlayer>();
		/// <summary>References the ProvidenceGlobalNPC instance. Shorthand for ease of use.</summary>
		public static ProvidenceGlobalNPC Providence(this NPC npc) => npc.GetGlobalNPC<ProvidenceGlobalNPC>();
		/// <summary>References the ProvidenceGlobalItem instance. Shorthand for ease of use.</summary>
		public static ProvidenceGlobalItem Providence(this Item item) => item.GetGlobalItem<ProvidenceGlobalItem>();
		/// <summary>References the ProvidenceGlobalProjectile instance. Shorthand for ease of use.</summary>
		public static ProvidenceGlobalProjectile Providence(this Projectile proj) => proj.GetGlobalProjectile<ProvidenceGlobalProjectile>();
		/// <summary>References the Player owner of a projectile instance. Shorthand for ease of use.</summary>
		public static Player OwnerPlayer(this Projectile projectile) => Main.player[projectile.owner];
		/// <summary>References the NPC owner of a projectile instance. Shorthand for ease of use.</summary>
		public static NPC OwnerNPC(this Projectile projectile) => Main.npc[projectile.owner];
		/// <summary>References the Main.localPlayer. Shorthand for ease of use.</summary>
		public static Player LocalPlayer() => Main.LocalPlayer;
		/// <summary>Shorthand for converting degrees of rotation into a radians equivalent.</summary>
		public static float InRadians(this float degrees) => MathHelper.ToRadians(degrees);
		/// <summary>Shorthand for converting radians of rotation into a degrees equivalent.</summary>
		public static float InDegrees(this float radians) => MathHelper.ToDegrees(radians);
		/// <summary>Automatically converts seconds into game ticks. 1 second is 60 ticks.</summary>
		public static int InTicks(this float seconds) => (int)(seconds * 60);
		public static decimal Round(this decimal dec, int points) => decimal.Round(dec, points);
		public static float Round(this float f, int points) => (float)Math.Round(f, points);
		public static double Round(this double d, int points) => Math.Round(d, points);
		public static Vector3 ColorRGBIntToFloat(this Vector3 vector3)
		{
			const double conversion = 1f / 255f;
			vector3.X = (float)(vector3.X * conversion);
			vector3.Y = (float)(vector3.Y * conversion);
			vector3.Z = (float)(vector3.Z * conversion);
			return vector3;
		}
		public static Color ColorRGBIntToFloat(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R * conversion);
			color.G = (byte)(color.G * conversion);
			color.B = (byte)(color.B * conversion);
			return color;
		}
		public static Vector4 ColorRGBAIntToFloat(this Vector4 vector4)
		{
			const double conversion = 1f / 255f;
			vector4.X = (float)(vector4.X * conversion);
			vector4.Y = (float)(vector4.Y * conversion);
			vector4.Z = (float)(vector4.Z * conversion);
			vector4.W = (float)(vector4.W * conversion);
			return vector4;
		}
		public static Color ColorRGBAIntToFloat(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R * conversion);
			color.G = (byte)(color.G * conversion);
			color.B = (byte)(color.B * conversion);
			color.A = (byte)(color.A * conversion);
			return color;
		}
		public static Vector3 ColorRGBFloatToInt(this Vector3 vector3)
		{
			const double conversion = 1f / 255f;
			vector3.X = (float)(vector3.X / conversion);
			vector3.Y = (float)(vector3.Y / conversion);
			vector3.Z = (float)(vector3.Z / conversion);
			return vector3;
		}
		public static Color ColorRGBFloatToInt(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R / conversion);
			color.G = (byte)(color.G / conversion);
			color.B = (byte)(color.B / conversion);
			return color;
		}
		public static Vector4 ColorRGBAFloatToInt(this Vector4 vector4)
		{
			const double conversion = 1f / 255f;
			vector4.X = (float)(vector4.X / conversion);
			vector4.Y = (float)(vector4.Y / conversion);
			vector4.Z = (float)(vector4.Z / conversion);
			vector4.W = (float)(vector4.W / conversion);
			return vector4;
		}
		public static Color ColorRGBAFloatToInt(this Color color)
		{
			const double conversion = 1f / 255f;
			color.R = (byte)(color.R / conversion);
			color.G = (byte)(color.G / conversion);
			color.B = (byte)(color.B / conversion);
			color.A = (byte)(color.A / conversion);
			return color;
		}
		public static void UpdatePositionCache(this Projectile projectile)
		{
			for (int i = projectile.oldPos.Length - 1; i > 0; i--)
			{
				projectile.oldPos[i] = projectile.oldPos[i - 1];
			}
			projectile.oldPos[0] = projectile.position;
		}
		public static void UpdatePositionCache(this NPC npc)
		{
			for (int i = npc.oldPos.Length - 1; i > 0; i--)
			{
				npc.oldPos[i] = npc.oldPos[i - 1];
			}
			npc.oldPos[0] = npc.position;
		}
		public static void UpdateRotationCache(this Projectile projectile)
		{
			for (int i = projectile.oldRot.Length - 1; i > 0; i--)
			{
				projectile.oldRot[i] = projectile.oldRot[i - 1];
			}
			projectile.oldRot[0] = projectile.rotation;
		}
		public static void UpdateRotationCache(this NPC npc)
		{
			for (int i = npc.oldRot.Length - 1; i > 0; i--)
			{
				npc.oldRot[i] = npc.oldRot[i - 1];
			}
			npc.oldRot[0] = npc.rotation;
		}
		public static void UpdateCenterCache(this Projectile projectile)
		{
			for (int i = projectile.Providence().oldCen.Length - 1; i > 0; i--)
			{
				projectile.Providence().oldCen[i] = projectile.Providence().oldCen[i - 1];
			}
			projectile.Providence().oldCen[0] = projectile.Center;
		}
		public static void UpdateCenterCache(this NPC npc)
		{
			for (int i = npc.Providence().oldCen.Length - 1; i > 0; i--)
			{
				npc.Providence().oldCen[i] = npc.Providence().oldCen[i - 1];
			}
			npc.Providence().oldCen[0] = npc.Center;
		}
		public static void Talk(string message, Color color, int npc)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				string text = Language.GetTextValue(message, Lang.GetNPCNameValue(npc), message);
				Main.NewText(text, color.R, color.G, color.B);
			}
			else
			{
				NetworkText text = NetworkText.FromKey(message, npc, message);
				NetMessage.BroadcastChatMessage(text, new Color(color.R, color.G, color.B));
			}
		}
		/// <summary>Finds and returns the closesnt entity to the provided entity. Actively disregards Target Dummies.</summary>
		public static Entity ClosestEntity(Entity entity, bool hostile)
		{
			float shortest = -1f;
			Entity chosenEntity = null;
			if (hostile)
			{
				foreach (NPC npc in Main.npc)
				{
					if (npc.active && !npc.townNPC && !npc.friendly && npc.type != NPCID.TargetDummy)
					{
						float dist = Vector2.Distance(entity.Center, npc.Center);
						if (dist < shortest || shortest == -1f)
						{
							shortest = dist;
							chosenEntity = npc;
						}
					}
				}
			}
			if (!hostile)
			{
				foreach (Player player in Main.player)
				{
					if (player.active)
					{
						float dist = Vector2.Distance(entity.Center, player.Center);
						if (dist < shortest || shortest == -1f)
						{
							shortest = dist;
							chosenEntity = player;
						}
					}
				}
			}
			return chosenEntity;
		}
		/// <summary>Vector distance shorthand.</summary>
		public static bool IsInRadiusOf(this Vector2 position, Vector2 target, float radius) => Vector2.Distance(target, position) <= radius;
		/// <summary>Rotates the given vector to the provided rotation.</summary>
		public static Vector2 RotateTo(this Vector2 v, float rotation)
		{
			float oldVRotation = v.ToRotation();
			return v.RotatedBy(rotation - oldVRotation);
		}
		/// <summary>Gradually shifts between two colors over time.</summary>
		public static Color ColorShift(Color firstColor, Color secondColor, float seconds)
		{
			float amount = (float)((Math.Sin(Math.PI * Math.PI / seconds * Main.GlobalTime) + 1.0) * 0.5);
			return Color.Lerp(firstColor, secondColor, amount);
		}
		/// <summary>
		/// <para>Gradually shifts between multiple colors over time.</para>
		/// <para>Remember to provide the middle colors in reverse order for correct shifting.</para>
		/// <param name="colors">The array of colors to shift between</param> 
		/// <param name="seconds">The time to shift colors color</param> 
		/// </summary>
		public static Color ColorShiftMultiple(Color[] colors, float seconds)
		{
			float fade = Main.GameUpdateCount % (int)(seconds * 60) / (seconds * 60f);
			int index = (int)(Main.GameUpdateCount / (seconds * 60f) % colors.Length);
			return Color.Lerp(colors[index], colors[(index + 1) % colors.Length], fade);
		}
		public static void NewProjectileExtraAI(Vector2 position, Vector2 velocity, int Type, int Damage, float KnockBack, int Owner = 255, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, float ai4 = 0, float ai5 = 0, float ai6 = 0, float ai7 = 0)
		{
			int type = Projectile.NewProjectile(position, velocity, Type, Damage, KnockBack, Owner, ai0, ai1);
			Main.projectile[type].Providence().extraAI[0] = ai2;
			Main.projectile[type].Providence().extraAI[1] = ai3;
			Main.projectile[type].Providence().extraAI[2] = ai4;
			Main.projectile[type].Providence().extraAI[3] = ai5;
			Main.projectile[type].Providence().extraAI[4] = ai6;
			Main.projectile[type].Providence().extraAI[5] = ai7;
		}
		public static void NewNPCExtraAI(int X, int Y, int Type, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, float ai4 = 0, float ai5 = 0, float ai6 = 0, float ai7 = 0, int Target = 255)
		{
			int type = NPC.NewNPC(X, Y, Type, Start, ai0, ai1, ai2, ai3, Target);
			Main.npc[type].Providence().extraAI[0] = ai4;
			Main.npc[type].Providence().extraAI[0] = ai5;
			Main.npc[type].Providence().extraAI[0] = ai6;
			Main.npc[type].Providence().extraAI[0] = ai7;
		}
		public static void NewNPCExtraAI(Vector2 Position, int Type, int Start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, float ai4 = 0, float ai5 = 0, float ai6 = 0, float ai7 = 0, int Target = 255)
		{
			Vector2 pos = new Vector2(Position.X, Position.Y);
			int type = NPC.NewNPC((int) pos.X, (int) pos.Y, Type, Start, ai0, ai1, ai2, ai3, Target);
			Main.npc[type].Providence().extraAI[0] = ai4;
			Main.npc[type].Providence().extraAI[0] = ai5;
			Main.npc[type].Providence().extraAI[0] = ai6;
			Main.npc[type].Providence().extraAI[0] = ai7;
		}
		/// <summary>Returns whether there is a boss, and if there is, their ID in "Main.npc".</summary>
		public static Tuple<bool, int> IsThereABoss()
		{
			bool bossExists = false;
			int bossID = -1;
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && npc.boss)
					bossExists = true;
				bossID = npc.type;
			}
			return Tuple.Create(bossExists, bossID);
		}
		/// <summary>Provides a random point near the provided position.</summary>
		/// <param name="v">The origin point.</param>
		/// <param name="maxDist">Radius max.</param>
		public static Vector2 RandomPointNearby(this Vector2 v, float maxDist = 16f)
		{
			return Vector2.Add(v, new Vector2(0, Main.rand.NextFloat(maxDist)).RotatedByRandom(180f.InRadians()));
		}
		public static Vector2 RandomPointInHitbox(this Rectangle hitbox)
		{
			float x = Main.rand.Next(hitbox.Left, hitbox.Right + 1);
			float y = Main.rand.Next(hitbox.Top, hitbox.Bottom + 1);
			return new Vector2(x, y);
		}
		/// <summary>Provides the animation frame for given parameters.</summary>
		/// <param name="frame">The frame that this item is currently on. Use "public int frame;" in your item file.</param>
		/// <param name="frameTick">The frame tick for this item. Use "public int frameTick;" in your item file.</param>
		/// <param name="frameTime">How many frames (ticks) you are spending on a single frame.</param>
		/// <param name="frameCount">How many frames this animation has.</param>
		public static Rectangle AnimationFrame(this Entity entity, ref int frame, ref int frameTick, int frameTime, int frameCount, bool frameTickIncrease, int overrideHeight = 0)
		{
			if (frameTick >= frameTime)
			{
				frameTick = -1;
				frame = frame == frameCount - 1 ? 0 : frame + 1;
			}
			if (frameTickIncrease)
				frameTick++;
			if (overrideHeight > 0 || overrideHeight < 0)
			{
				return new Rectangle(0, overrideHeight * frame, entity.width, entity.height);
			}
			else
			{
				return new Rectangle(0, entity.height * frame, entity.width, entity.height);
			}
		}
		public static void FindFrame(this Projectile projectile, int time, int number)
		{
			if (++projectile.frameCounter >= time) // Frame time
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= number) //Frame number
				{
					projectile.frame = 0;
				}
			}
		}
		/// <summary>Draws a glowMask for the given item.</summary>
		/// <param name="spriteBatch">The spriteBatch instance for this glowMask. Passed with "PostDraw" or "PreDraw" item methods.</param>
		/// <param name="rotation">The rotation for this item. Passed with "PostDraw" item methods.</param>
		/// <param name="glowMaskTexture">The texture to draw for this glowMask.</param>
		public static void GlowMask(this Item item, SpriteBatch spriteBatch, float rotation, Texture2D glowMaskTexture)
		{
			Vector2 origin = new Vector2(glowMaskTexture.Width / 2f, (float)((glowMaskTexture.Height / 2.0) - 2.0));
			spriteBatch.Draw(glowMaskTexture, item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
		}
		/// <summary>Draws a glowMask for the given item while the item is in use.</summary>
		/// <param name="info">The PlayerDrawInfo instance for this method. Use "PlayerDrawInfo info = new PlayerDrawInfo();" and pass it.</param>
		public static void DrawGlowMask(PlayerDrawInfo info)
		{
			Player player = info.drawPlayer;
			Texture2D glowMaskTexture = null;
			if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir)
			{
				if (player.HeldItem.Providence().glowMask)
					glowMaskTexture = player.HeldItem.Providence().glowMaskTexture;
				Main.playerDrawData.Add(
					new DrawData(
						glowMaskTexture,
						info.itemLocation - Main.screenPosition,
						glowMaskTexture.Frame(),
						Color.White,
						player.itemRotation,
						new Vector2(player.direction == 1 ? 0 : glowMaskTexture.Width, glowMaskTexture.Height),
						player.HeldItem.scale,
						info.spriteEffects,
						0
					)
				);
			}
		}
		/// <summary>Draws an animated glowMask for the given item while the item is in use.</summary>
		/// <param name="info">The PlayerDrawInfo instance for this method. Use "PlayerDrawInfo info = new PlayerDrawInfo();" and pass it.</param>
		public static void DrawAnimation(PlayerDrawInfo info)
		{
			Player player = info.drawPlayer;
			Texture2D animationTexture = null;
			Rectangle frame = default;
			if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir)
			{
				if (player.HeldItem.Providence().animated)
				{
					animationTexture = player.HeldItem.Providence().animationTexture;
					frame = Main.itemAnimations[player.HeldItem.type].GetFrame(animationTexture);
				}
				Main.playerDrawData.Add(
					new DrawData(
						animationTexture,
						info.itemLocation - Main.screenPosition,
						frame,
						Lighting.GetColor((int)player.Center.X / 16, (int)player.Center.Y / 16),
						player.itemRotation,
						new Vector2(player.direction == 1 ? 0 : frame.Width, frame.Height),
						player.HeldItem.scale,
						info.spriteEffects,
						0
					)
				);
			}
		}
		public static void DrawGlowMaskAnimation(PlayerDrawInfo info)
		{
			Player player = info.drawPlayer;
			Texture2D animatedGlowMaskTexture = null;
			Rectangle frame = default;
			if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir)
			{
				if (player.HeldItem.Providence().animatedGlowMask)
				{
					animatedGlowMaskTexture = player.HeldItem.Providence().animatedGlowMaskTexture;
					frame = Main.itemAnimations[player.HeldItem.type].GetFrame(animatedGlowMaskTexture);
				}
				Main.playerDrawData.Add(
					new DrawData(
						animatedGlowMaskTexture,
						info.itemLocation - Main.screenPosition,
						frame,
						Color.White,
						player.itemRotation,
						new Vector2(player.direction == 1 ? 0 : frame.Width, frame.Height) + new Vector2(-100f, -100f),
						player.HeldItem.scale,
						info.spriteEffects,
						0
					)
				);
			}
		}
		//TODO: Clarify this AI
		public static void SmoothHoming(Projectile projectile)
		{
			float speed = (float)Math.Sqrt((projectile.velocity.X * (double)projectile.velocity.X) + (projectile.velocity.Y * (double)projectile.velocity.Y));
			float localAI = projectile.localAI[0];
			if ((double)localAI == 0.0)
			{
				projectile.localAI[0] = speed;
				localAI = speed;
			}
			float posX = projectile.position.X;
			float posY = projectile.position.Y;
			float range = 1000f;
			bool tracking = false;
			int ai = 0;
			if (projectile.ai[1] == 0.0)
			{
				for (int index = 0; index < 200; ++index)
				{
					if (Main.npc[index].CanBeChasedBy(projectile) && (projectile.ai[1] == 0.0 || projectile.ai[1] == (double)(index + 1)))
					{
						float npcCenterX = Main.npc[index].position.X + (Main.npc[index].width / 2);
						float npcCenterY = Main.npc[index].position.Y + (Main.npc[index].height / 2);
						float totalOffset = Math.Abs(projectile.position.X + (projectile.width / 2) - npcCenterX) + Math.Abs(projectile.position.Y + (projectile.height / 2) - npcCenterY);
						if (totalOffset < range && Collision.CanHit(new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2)), 1, 1, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height))
						{
							range = totalOffset;
							posX = npcCenterX;
							posY = npcCenterY;
							tracking = true;
							ai = index;
						}
					}
				}
				if (tracking)
					projectile.ai[1] = ai + 1;
				tracking = false;
			}
			if (projectile.ai[1] > 0.0)
			{
				int index = (int)(projectile.ai[1] - 1.0);
				if (Main.npc[index].active && Main.npc[index].CanBeChasedBy(projectile, true) && !Main.npc[index].dontTakeDamage)
				{
					float npcCenterX = Main.npc[index].position.X + (Main.npc[index].width / 2);
					float npcCenterY = Main.npc[index].position.Y + (Main.npc[index].height / 2);
					if (Math.Abs(projectile.position.X + (projectile.width / 2) - npcCenterX) + (double)Math.Abs(projectile.position.Y + (projectile.height / 2) - npcCenterY) < 1000.0)
					{
						tracking = true;
						posX = Main.npc[index].position.X + (Main.npc[index].width / 2);
						posY = Main.npc[index].position.Y + (Main.npc[index].height / 2);
					}
				}
				else
				{
					projectile.ai[1] = 0.0f;
				}
			}
			if (!projectile.friendly)
				tracking = false;
			if (tracking)
			{
				double npcCenterX = localAI;
				Vector2 projCenter = new Vector2(projectile.position.X + (projectile.width * 0.5f), projectile.position.Y + (projectile.height * 0.5f));
				float npcCenterY = posX - projCenter.X;
				float totalOffset = posY - projCenter.Y;
				double offset2 = Math.Sqrt((npcCenterY * (double)npcCenterY) + (totalOffset * (double)totalOffset));
				float num11 = (float)(npcCenterX / offset2);
				float num12 = npcCenterY * num11;
				float num13 = totalOffset * num11;
				int num14 = 8;
				if (projectile.type == 837)
					num14 = 32;
				projectile.velocity.X = ((projectile.velocity.X * (num14 - 1)) + num12) / num14;
				projectile.velocity.Y = ((projectile.velocity.Y * (num14 - 1)) + num13) / num14;
			}
		}
		public static void NaturalHoming(Projectile projectile, Entity entity, float turn, float speed)
		{
			Vector2 dir = projectile.DirectionTo(entity.Center);
			projectile.velocity = ((projectile.velocity * turn) + (dir * speed)) / (turn + 1f);
		}
		//TODO: Implement this properly using StrikeNPC and StrikePlayer
		// public static int ArmorCalculation(NPC npc, ref int damage, ref bool crit)
		// {
		//   int armorDamage = 0;
		//   // If the NPC is armored
		//   if (npc.Providence().Armored)
		//   {
		//     crit = false;
		//     // Deduct the damage from the armor
		//     armorDamage += (int)(damage * npc.Providence().armorEfficiency);
		//     npc.Providence().armor -= armorDamage;
		//     damage = (int)(damage * (1f - npc.Providence().armorEfficiency));
		//     // If the armor ends up being less than 0
		//     if (npc.Providence().armor < 0)
		//     {
		//       // Signify that as "overflow damage" by deducting the abs of the remainder from the NPC's life
		//       armorDamage += npc.Providence().armor;
		//       // Subtracting negative == adding positive
		//       npc.life += npc.Providence().armor;
		//       damage -= npc.Providence().armor;
		//       npc.Providence().armor = 0;
		//     }
		//     CombatText.NewText(npc.Hitbox, Color.White, armorDamage);
		//   }
		//   return damage;
		// }
		public static LegacySoundStyle AsLegacy(this int id, int style = 1) => new LegacySoundStyle(id, style);
		public static LegacySoundStyle AsLegacy(this string filename, Mod mod, Terraria.ModLoader.SoundType soundType = Terraria.ModLoader.SoundType.Item)
		=> mod.GetLegacySoundSlot(soundType, filename);
		public static int AsMusicSlot(this string filename, Mod mod) => mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, filename);
		public enum ElementID
		{
			Typeless = -1,
			Fire = 0,
			Ice = 1,
			Lightning = 2,
			Water = 3,
			Earth = 4,
			Air = 5,
			Radiant = 6,
			Shadow = 7
		}
		//public static void RepeatXTimes(int x, Action del)
		//{
		//	for (int i = 0; i < x; i++)
		//	{
		//		del();
		//	}
		//}
		//public static void RepeatXTimes(int x, Action<int> del)
		//{
		//	for (int i = 0; i < x; i++)
		//	{
		//		del(i);
		//	}
		//}
		public static bool PercentChance(this int num) => Main.rand.Next(0, 100) >= (100 - num);
		public static bool DownedAnyBoss() => NPC.downedBoss1     || NPC.downedBoss2          || NPC.downedBoss3     ||
																					NPC.downedFishron   || NPC.downedAncientCultist || NPC.downedGolemBoss ||
																					NPC.downedPlantBoss || NPC.downedMechBossAny    || NPC.downedMoonlord  ||
																					NPC.downedSlimeKing || NPC.downedQueenBee;
	}
}