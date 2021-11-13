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
using ReLogic.Graphics;
using ProvidenceMod.Particles;

namespace ProvidenceMod
{
	public static partial class ProvidenceUtils
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
		public static void UpdateLifeCache(this NPC npc)
		{
			for (int i = npc.Providence().oldLife.Length - 1; i > 0; i--)
			{
				npc.Providence().oldLife[i] = npc.Providence().oldLife[i - 1];
			}
			npc.Providence().oldLife[0] = npc.life;
		}
		public static bool IsWalking(this Player player) => player.velocity.X < -0.5f || player.velocity.X > 0.5f || player.velocity.Y < -0.5f || player.velocity.Y > 0.5f;
		public static void DrawHealthBarCustom(this NPC npc, ref int comboDMG, ref int comboBarCooldown, ref int comboDMGCooldown, ref int comboDMGCounter, ref float scale, ref Vector2 position, ref Rectangle comboRect)
		{
			float quotient = npc.life / (float)npc.lifeMax;
			if (quotient > 1f)
				quotient = 1f;

			int width = (int)(36f * quotient);
			float xPos = position.X - (24f * scale);
			float yPos = position.Y;
			if (Main.player[Main.myPlayer].gravDir == -1f)
			{
				yPos -= Main.screenPosition.Y;
				yPos = Main.screenPosition.Y + Main.screenHeight - yPos;
			}


			Rectangle healthRect = new Rectangle(0, 0, (int)(width * quotient) < 1 ? 1 : (int)(width * quotient), 8);

			if (comboRect.Width < healthRect.Width)
			{
				comboRect.Width = healthRect.Width;
			}

			if (npc.life < npc.Providence().oldLife[0])
			{
				comboBarCooldown = 120;
				comboDMGCounter = 120;
				npc.UpdateLifeCache();
				comboDMG += npc.Providence().oldLife[1] - npc.Providence().oldLife[0];
			}
			else if (npc.life == npc.Providence().oldLife[0])
			{
				if (comboBarCooldown > 0) comboBarCooldown--;
			}
			if (comboBarCooldown == 0 && comboRect.Width != healthRect.Width)
			{
				comboRect.Width--;
				//if ((comboRect.Width - healthRect.Width) * 0.05f < 1)
				//else
				//	comboRect.Width -= (int)((comboRect.Width - healthRect.Width) * 0.05f);
			}
			if ((comboBarCooldown == 0 && comboDMG != 0) || (healthRect.Width == comboRect.Width && comboDMG != 0))
			{
				comboDMG -= (int)(comboDMG * 0.05f);
				comboDMG--;
			}
			if (comboDMG == 0 && comboDMGCooldown != 120 && comboDMGCounter != 0)
			{
				comboDMGCounter--;
			}

			if (comboDMGCounter > 0)
			{
				float opacity = comboDMGCounter > 61 ? 1f : comboDMGCounter / 120f;
				SpriteBatch spriteBatch = new SpriteBatch(Main.graphics.GraphicsDevice);
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
				DrawBorderStringEightWay(spriteBatch, ProvidenceMod.bossHealthFont, $"{comboDMG}", new Vector2(xPos + (48f * scale) + 2f, yPos + 9f) - Main.screenPosition, new Color((int)(255 * opacity), (int)(255 * opacity), (int)(255 * opacity), (int)(255 * opacity)), new Color((int)(23 * opacity), (int)(23 * opacity), (int)(23 * opacity), (int)(255 * opacity)), 0.4f);
				spriteBatch.End();
			}
			Main.spriteBatch.Draw(GetTexture("ProvidenceMod/TexturePack/UI/HB1"), new Vector2(xPos, yPos) - Main.screenPosition, new Rectangle(0, 0, 48, 20), Color.White, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(GetTexture("ProvidenceMod/TexturePack/UI/HB3"), new Vector2(xPos + 6f * scale, yPos + 6f * scale) - Main.screenPosition, comboRect, Color.White, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(GetTexture("ProvidenceMod/TexturePack/UI/HB2"), new Vector2(xPos + 6f * scale, yPos + 6f * scale) - Main.screenPosition, healthRect, Color.White, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
		}
		public static void DrawBorderStringEightWay(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color color, Color border, float scale = 1f)
		{
			for (int x = -2; x <= 2; x++)
			{
				for (int y = -2; y <= 2; y++)
				{
					Vector2 vector2 = position + new Vector2(x, y);
					if (x != 0 || y != 0)
						spriteBatch.DrawString(font, text, vector2, border, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
				}
			}
			spriteBatch.DrawString(font, text, position, color, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
		}
		public static void Talk(string message, Color color, int npc = -1)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				if (npc != -1)
				{
					string text = Language.GetTextValue(message, Lang.GetNPCNameValue(npc), message);
					Main.NewText(text, color.R, color.G, color.B);
					return;
				}
				Main.NewText(message, color.R, color.G, color.B);
			}
			else
			{
				if (npc != -1)
				{
					NetworkText text = NetworkText.FromKey(message, npc, message);
					NetMessage.BroadcastChatMessage(text, new Color(color.R, color.G, color.B));
					return;
				}
				NetMessage.BroadcastChatMessage(NetworkText.FromKey(message), new Color(color.R, color.G, color.B));
			}
		}
		/// <summary>Finds and returns the closest player to the entity. <para>Actively disregards Target Dummies.</para></summary>
		public static Player ClosestPlayer(this Entity ent)
		{
			float shortest = -1f;
			Player chosenEntity = null;
			foreach (Player player in Main.player)
			{
				if (player.active)
				{
					float dist = Vector2.Distance(ent.Center, player.Center);
					if (dist < shortest || shortest == -1f)
					{
						shortest = dist;
						chosenEntity = player;
					}
				}
			}
			return chosenEntity;
		}
		/// <summary>Finds and returns the closest NPC to the entity. Optional parameter to decide to target hostile NPC or Town NPC.
		/// <para>Actively disregards Target Dummies.</para></summary>
		public static NPC ClosestNPC(this Entity ent, bool town = false)
		{
			float shortest = -1f;
			NPC chosenEntity = null;
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && npc.type != NPCID.TargetDummy)
				{
					// If we're looking for a town NPC and the current NPC is hostile (or vice versa if looking for a hostile mob),
					// Skip the distance checking; it's not something we're looking for
					if (town && (!npc.friendly || !npc.townNPC)) continue;
					if (!town && (npc.friendly || npc.townNPC)) continue;

					float dist = Vector2.Distance(ent.Center, npc.Center);
					if (dist < shortest || shortest == -1f)
					{
						shortest = dist;
						chosenEntity = npc;
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
			int type = NPC.NewNPC((int)pos.X, (int)pos.Y, Type, Start, ai0, ai1, ai2, ai3, Target);
			Main.npc[type].Providence().extraAI[0] = ai4;
			Main.npc[type].Providence().extraAI[0] = ai5;
			Main.npc[type].Providence().extraAI[0] = ai6;
			Main.npc[type].Providence().extraAI[0] = ai7;
		}
		public static void NewParticle(Vector2 Position, Vector2 Velocity, Particle Type, Color Color, float AI0 = 0, float AI1 = 0, float AI2 = 0, float AI3 = 0, float AI4 = 0, float AI5 = 0, float AI6 = 0, float AI7 = 0)
		{
			ProvidenceMod.particleManager.NewParticle(Position, Velocity, Type, Color, AI0, AI1, AI2, AI3, AI4, AI5, AI6, AI7);
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
		/// <param name="minDist">Radius min.</param>
		/// <param name="maxDist">Radius max + 1.</param>
		public static Vector2 RandomPointNearby(this Vector2 v, float minDist = 0f, float maxDist = 16f) => Vector2.Add(v, new Vector2(Main.rand.NextFloat(minDist, maxDist), 0).RotatedByRandom(MathHelper.TwoPi));
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
			return new Rectangle(0, overrideHeight != 0 ? overrideHeight * frame : entity.height * frame, entity.width, entity.height);
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
		public static bool DownedAnyBoss() => NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 ||
																					NPC.downedFishron || NPC.downedAncientCultist || NPC.downedGolemBoss ||
																					NPC.downedPlantBoss || NPC.downedMechBossAny || NPC.downedMoonlord ||
																					NPC.downedSlimeKing || NPC.downedQueenBee;
	}
}