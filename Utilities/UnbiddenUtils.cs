using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using UnbiddenMod.Buffs.Cooldowns;

namespace UnbiddenMod
{
  public static class UnbiddenUtils
  {
    /// <summary>References the UnbiddenPlayer instance. Shorthand for ease of use.</summary>
    public static UnbiddenPlayer Unbidden(this Player player) => player.GetModPlayer<UnbiddenPlayer>();
    /// <summary>References the UnbiddenGlobalNPC instance. Shorthand for ease of use.</summary>
    public static UnbiddenGlobalNPC Unbidden(this NPC npc) => npc.GetGlobalNPC<UnbiddenGlobalNPC>();
    /// <summary>References the UnbiddenGlobalItem instance. Shorthand for ease of use.</summary>
    public static UnbiddenGlobalItem Unbidden(this Item item) => item.GetGlobalItem<UnbiddenGlobalItem>();
    /// <summary>References the UnbiddenGlobalProjectile instance. Shorthand for ease of use.</summary>
    public static UnbiddenGlobalProjectile Unbidden(this Projectile proj) => proj.GetGlobalProjectile<UnbiddenGlobalProjectile>();

    /// <summary>Shorthand for converting degrees of rotation into a radians equivalent.</summary>
    public static float InRadians(this float degrees) => MathHelper.ToRadians(degrees);
    /// <summary>Shorthand for converting radians of rotation into a degrees equivalent.</summary>
    public static float InDegrees(this float radians) => MathHelper.ToDegrees(radians);
    public static float[,] elemAffDef = new float[2, 15]
    {  // Defense score (middle), Damage mult (bottom)
      {     1,      2,      3,      5,      7,      9,     12,     15,     18,     22,     26,     30,     35,     45,     50},
      {1.010f, 1.022f, 1.037f, 1.056f, 1.080f, 1.110f, 5.000f, 1.192f, 1.246f, 1.310f, 1.397f, 1.497f, 1.611f, 1.740f, 1.885f}
    };

    /// <summary>Calculates the elemental defense of the player based on their affinities, and any accessories and armor providing such defense.</summary>
    public static void CalcElemDefense(this Player player)
    {
      UnbiddenPlayer unPlayer = player.GetModPlayer<UnbiddenPlayer>();
      for (int k = 0; k < 8; k++)
      {
        int index = unPlayer.affinities[k] - 1;
        if (index != -1)
          unPlayer.resists[k] += (int)elemAffDef[0, index];
      }
    }
    public static float[] GetAffinityBonuses(this Player player, int e)
    {
      return new float[2] { elemAffDef[0, player.Unbidden().affinities[e]], elemAffDef[1, player.Unbidden().affinities[e]] };
    }

    /// <summary>
    /// Elemental damage calculation for when the player hits an NPC with a melee weapon.
    /// </summary>
    public static int CalcEleDamage(this Item item, NPC npc, ref int damage)
    {
      int weapEl = item.Unbidden().element; // Determine the element (will always be between 0-6 for array purposes)
      if (weapEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = npc.Unbidden().resists[weapEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
        // UnbiddenPlayer modPlayer = Main.player[item.owner].Unbidden();
        // if (modPlayer.affExpCooldown <= 0)
        // {
        //   modPlayer.affExp[weapEl] += 1;
        //   modPlayer.affExpCooldown = 120;
        // }
      }
      return damage;
    }
    // 
    // Summary:
    // Calculates elemental projectile damage based on UnbiddenGlobalNPC resists.
    // UnbiddenUtils.CalcEleDamage(Projectile projectile, NPC npc, ref int damage);
    /// <summary>
    /// Elemental damage calculation for when the player hits an NPC with a projectile.
    /// </summary>
    public static int CalcEleDamage(this Projectile projectile, NPC npc, ref int damage)
    {
      int projEl = projectile.Unbidden().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = (float)damage, // And the damage we already have, converted to float
          resistMod = npc.Unbidden().resists[projEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
      }
      return damage;
    }

    /// <summary>
    /// Elemental damage calculation for when the player is hit by an NPC.
    /// </summary>
    public static int CalcEleDamageFromNPC(this Player player, NPC npc, ref int damage)
    {
      int npcEl = npc.Unbidden().contactDamageEl;
      if (npcEl != -1)
      {
        int resistMod = player.Unbidden().resists[npcEl];
        damage -= (int)(Main.expertMode ? resistMod * 0.75 : resistMod * 0.5);
      }
      return damage;
    }

    /// <summary>
    /// Elemental damage calculation for when the player is hit by a projectile.
    /// </summary>
    public static int CalcEleDamageFromProj(this Player player, Projectile proj, ref int damage)
    {
      int projEl = proj.Unbidden().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        int resistMod = player.Unbidden().resists[projEl];
        damage -= (int)(Main.expertMode ? resistMod * 0.75 : resistMod * 0.5); // set the damage to the int version of the new float, implicitly rounding down to the lower int
      }
      return damage;
    }
    /// <summary>
    /// Shorthanding for the conditional in the Parry methods.
    /// </summary>
    /// <param name="currProj">The projectile being tested.</param>
    /// <param name="player">The active player acting as the point of origin.</param>
    /// <param name="hitbox">The hitbox of the parry shield projectile.</param>
    /// <param name="parryShield">The ID in "Main.projectile" the parry shield projectile is.</param>
    public static bool IsParry(this Projectile currProj, Player player, Rectangle hitbox, ref int parryShield)
    {
      return currProj.active && currProj.whoAmI != parryShield && !player.HasBuff(BuffType<CantDeflect>()) && currProj.Unbidden().deflectable && currProj.hostile && hitbox.Intersects(currProj.Hitbox);
    }

    /// <summary>
    /// Generates the mechanical effects of a parry. Called every tick while a player is parrying.
    /// <para>Standard Parry is exactly what you would expect from a parry: bounce projectiles back towards enemies, keeping the damage off of you and on them.</para>
    /// </summary>
    /// <param name="player">The active player acting as the point of origin.</param>
    /// <param name="hitbox">The hitbox of the parry shield projectile.</param>
    /// <param name="parryShield">The ID in "Main.projectile" the parry shield projectile is.</param>
    public static void StandardParry(Player player, Rectangle hitbox, ref int parryShield)
    {
      int affectedProjs = 0;
      foreach (Projectile currProj in Main.projectile)
      {
        if (currProj.IsParry(player, hitbox, ref parryShield))
        {
          // Add your melee damage multiplier to the damage so it has a little more oomph
          currProj.damage = (int)(currProj.damage * player.meleeDamageMult);

          // If Micit Bangle is equipped, add that multiplier.
          currProj.damage = player.Unbidden().micitBangle ? (int)(currProj.damage * 2.5) : currProj.damage;
          // Convert the proj so you own it and reverse its trajectory
          currProj.owner = player.whoAmI;
          currProj.hostile = false;
          currProj.friendly = true;
          currProj.Unbidden().deflected = true;
          currProj.velocity.X = -currProj.velocity.X;
          currProj.velocity.Y = -currProj.velocity.Y;
          affectedProjs++;
        }
      }
      player.Unbidden().parriedProjs += affectedProjs;
    }
    /// <summary>
    /// Generates the mechanical effects of a parry. Called every tick while a player is parrying.
    /// <para>Tank Parry absorbs projectiles, instead of deflecting them. Each projectile absorbed provides a Damage Reduction boost, based on the damage of the projectile, which it then returns.</para>
    /// </summary>
    /// <param name="player">The active player acting as the point of origin.</param>
    /// <param name="hitbox">The hitbox of the parry shield projectile.</param>
    /// <param name="parryShield">The ID in "Main.projectile" the parry shield projectile is.</param>
    public static int TankParry(Player player, Rectangle hitbox, ref int parryShield)
    {
      int affectedProjs = 0;
      int DRBoost = 0;
      foreach (Projectile currProj in Main.projectile)
      {
        if (currProj.IsParry(player, hitbox, ref parryShield))
        {
          DRBoost += currProj.damage / 10;
          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Unbidden().parriedProjs += affectedProjs;
      return DRBoost;
    }

    /// <summary>
    /// Generates the mechanical effects of a parry. Called every tick while a player is parrying.
    /// <para>DPS Parry turns all deflected projectiles into a chlorophyte bullet, with improved damage, almost always guaranteeing it will contact and deal substantial damage.</para>
    /// </summary>
    /// <param name="player">The active player acting as the point of origin.</param>
    /// <param name="hitbox">The hitbox of the parry shield projectile.</param>
    /// <param name="parryShield">The ID in "Main.projectile" the parry shield projectile is.</param>
    public static void DPSParry(Player player, Rectangle hitbox, ref int parryShield)
    {
      int affectedProjs = 0;
      foreach (Projectile currProj in Main.projectile)
      {
        if (currProj.IsParry(player, hitbox, ref parryShield))
        {
          _ = Projectile.NewProjectile(
            currProj.position,
            Vector2.Negate(currProj.velocity),
            ProjectileID.ChlorophyteBullet,
            (int)(player.Unbidden().micitBangle ? currProj.damage * 2 * 2.5 : currProj.damage * 5),
            currProj.knockBack,
            player.whoAmI
            );

          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Unbidden().parriedProjs += affectedProjs;
    }

    /// <summary>
    /// Generates the mechanical effects of a parry. Called every tick while a player is parrying.
    /// <para>Support Parry is currently incomplete. Its expected effect is to heal the user, and if the user is at max HP, the closest player gains the benefit.<para>
    /// </summary>
    /// <param name="player">The active player acting as the point of origin.</param>
    /// <param name="hitbox">The hitbox of the parry shield projectile.</param>
    /// <param name="parryShield">The ID in "Main.projectile" the parry shield projectile is.</param>
    public static void SupportParry(Player player, Rectangle hitbox, ref int parryShield)
    {
      int affectedProjs = 0;
      int HPBoost = 0;
      foreach (Projectile currProj in Main.projectile)
      {
        if (currProj.IsParry(player, hitbox, ref parryShield))
        {
          HPBoost += currProj.damage / 10;
          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Unbidden().parriedProjs += affectedProjs;
      player.statLife += HPBoost;
      if (HPBoost > 0)
      {
        player.HealEffect(HPBoost);
      }
    }

    /// <summary>Generates dust particles based on Aura size. Call when adding an Aura buff.</summary>
    /// <param name="player">The active player acting as the point of origin.</param>
    /// <param name="dust">The ID of the dust particle to use for the aura. By convention, this dust should have no gravity or light, and should have AI set to dissipate within 3-5 ticks.</param>
    /// <param name="radiusBoost">The distance modifier for the aura's effective range.</param>
    public static void GenerateAuraField(Player player, int dust, float radiusBoost)
    {
      UnbiddenPlayer mP = player.Unbidden();
      for (float rotation = 0f; rotation < 360f; rotation += 8f)
      {
        Vector2 spawnPosition = player.MountedCenter + new Vector2(0f, mP.clericAuraRadius + radiusBoost).RotatedBy(rotation.InRadians());
        Dust d = Dust.NewDustPerfect(spawnPosition, dust, null, 90, new Color(255, 255, 255), 1f);
        d.noLight = true;
        d.noGravity = true;
      }
    }
    /// <summary>Finds and returns the hostile NPC closest to the provided projectile. Actively disregards Target Dummies.</summary>
    public static NPC ClosestEnemyNPC(Projectile projectile)
    {
      float shortest = -1f;
      NPC chosenNPC = null;
      foreach (NPC npc in Main.npc)
      {
        if (npc.active && !npc.townNPC && !npc.friendly && npc.type != NPCID.TargetDummy)
        {
          float dist = Vector2.Distance(projectile.position, npc.position);
          if (dist < shortest || shortest == -1f)
          {
            shortest = dist;
            chosenNPC = npc;
          }
        }
      }
      return chosenNPC;
    }
    /// <summary>Finds and returns the player closest to the provided projectile.</summary>
    public static Player ClosestPlayer(Projectile projectile)
    {
      float shortest = -1f;
      Player chosenPC = null;
      foreach (Player player in Main.player)
      {
        if (player.active)
        {
          float dist = Vector2.Distance(projectile.position, player.position);
          if (dist < shortest || shortest == -1f)
          {
            shortest = dist;
            chosenPC = player;
          }
        }
      }
      return chosenPC;
    }
    /// <summary>Shorthanding of distance testing involving Vector2's, for readability.</summary>
    public static bool IsInRadius(this Vector2 targetPos, Vector2 center, float radius) => Vector2.Distance(center, targetPos) <= radius;

    /// <summary>Provides how many instances of the projectile type currently exist.</summary>
    public static int GrabProjCount(int type)
    {
      int count = 0;
      foreach (Projectile proj in Main.projectile)
      {
        if (proj.type == type)
          count++;
      }
      return count;
    }
    /// <summary>A simpler version of rotation control, just sets the rotation to the given value. This is deterministic, unlike "RotatedBy", which adds the rotation value to the current.</summary>
    public static Vector2 RotateTo(this Vector2 v, float rotation)
    {
      float oldVRotation = v.ToRotation();
      return v.RotatedBy(rotation - oldVRotation);
    }
    // 
    // Summary:
    // Add to a list if condition returns true.
    // UnbiddenUtils.AddWithCondition(List<T> list, T type, bool condition);
    public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
    {
      if (!condition)
        return;
      list.Add(type);
    }
    public static Color ColorShift(Color firstColor, Color secondColor, float seconds)
    {
      float amount = (float)((Math.Sin(Math.PI * Math.PI / seconds * Main.GlobalTime) + 1.0) * 0.5);
      return Color.Lerp(firstColor, secondColor, amount);
    }

    /// <summary>For use in setting defaults in items.</summary>
    /// <param name="item">The item being set.</param>
    /// <param name="elID">The element of a weapon's attacks, or additional elemental defense of accessories and armor.</param>
    /// <param name="elDef">The amount of elemental defense provided in the element given to "elID" Defaults to 0.</param>
    /// <param name="weakElID">The element of a weapon's weakness. Only really used for armor. Defaults to -1 (Typeless).</param>
    /// <param name="weakElDef">The amount of elemental defense lowered in the element given to "weakElID". Defaults to 0.</param>
    public static void SetElementalTraits(this Item item, int elID, int elDef = 0, int weakElID = -1, int weakElDef = 0)
    {
      item.Unbidden().element = elID;
      item.Unbidden().elementDef = elDef;
      if (weakElID != -1)
      {
        item.Unbidden().weakEl = weakElID;
        item.Unbidden().weakElDef = weakElDef;
      }
    }
    /// <summary>Returns if there is a boss, and if there is, their ID in "Main.npc".</summary>
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
    // Tuple in this order: Damage, DR, Regen, Speed
    /// <summary>Returns the player's bonuses originated from their focus. In a tuple for ease of access.</summary>
    public static Tuple<int, decimal, decimal, decimal> FocusBonuses(this Player player)
    {
      UnbiddenPlayer mP = player.Unbidden();
      int focusPercent = (int)(mP.focus * 100);
      int damageBoost = focusPercent / 5;
      decimal DR = (decimal)((mP.focus / 4 >= 0.25f ? 0.25f : mP.focus / 4) * 100);
      decimal regen = focusPercent;
      decimal moveSpeedBoost = focusPercent / 2;
      return new Tuple<int, decimal, decimal, decimal>(damageBoost, DR, regen, moveSpeedBoost);
    }

    /// <summary>
    /// A function that gives a sort of "gravity" effect, pulling the Vector2 "v" towards the angle with the given amount.
    /// </summary>
    /// <param name="v">The velocity being gravitized.</param>
    /// <param name="angleToTarget">The angle relative to v and the source of gravity.</param>
    /// <param name="turnAMT">How strong the gravity is, and how fast the turning effect is.</param>
    public static Vector2 TurnTowardsByX(this Vector2 v, float angleToTarget, float turnAMT)
    {
      Vector2 pull = new Vector2(turnAMT, 0f).RotateTo(angleToTarget);
      return Vector2.Add(v, pull);
    }

    /// <summary>
    /// A smart homing AI for all projectiles to use in their AIs. A good cover-all to allow homing without constant retyping.
    /// <para>This is a free-to-use code example for our open source, so adopt code as you need!</para>
    /// </summary>
    /// <param name="projectile">The projectile being worked with.</param>
    /// <param name="speedCap">How fast the projectile can go in a straight line. Defaults at 6f.</param>
    /// <param name="turnStrength">How much the projectile will change direction. Defaults at 0.1f.</param>
    /// <param name="trackingRadius">How far away from its target it can be and still chase after. Defaults at 200f.</param>
    /// <param name="overshotPrevention">Whether or not there should be a radius where it will guarantee its hit, even if hitboxes don't intersect. Defaults to false.</param>
    /// <param name="overshotThreshold">If overshotPrevention is true, provides the radius which will guarantee the hit. Defaults to 0f.</param>
    public static void Homing(this Projectile projectile, float speedCap = 6f, float turnStrength = 0.1f,
    float trackingRadius = 200f, bool overshotPrevention = false, float overshotThreshold = 0f)
    {
      // Slightly different tracking methods between hostile and friendly AIs. Not much, but enough.
      if (projectile.friendly)
      {
        // Potential owner requirements?
        Player owner = Main.player[projectile.owner];
        // Target the closest hostile NPC. If in range, turn the velocity towards target by turnStrength.
        NPC potTarget = ClosestEnemyNPC(projectile);
        if (potTarget?.position.IsInRadius(projectile.position, trackingRadius) == true)
          projectile.velocity = projectile.velocity.TurnTowardsByX(projectile.AngleTo(potTarget.position), turnStrength);

        // If overshotPrevention is on, force the projectile to beeline right for the target if it's within threshold distance.
        if (overshotPrevention && potTarget?.position.IsInRadius(projectile.position, overshotThreshold) == true)
          projectile.velocity = new Vector2(speedCap, 0f).RotateTo(projectile.AngleTo(potTarget.position));
      }
      else if (projectile.hostile)
      {
        // Same basic process as with friendly projs.
        NPC owner = Main.npc[projectile.owner];
        Player potTarget = ClosestPlayer(projectile);
        if (potTarget.active && potTarget.position.IsInRadius(projectile.position, trackingRadius))
          projectile.velocity = projectile.velocity.TurnTowardsByX(projectile.AngleTo(potTarget.position), turnStrength);

        // If overshotPrevention is on, force the projectile to beeline right for the target if it's within threshold distance.
        if (overshotPrevention && potTarget.position.IsInRadius(projectile.position, overshotThreshold))
          projectile.velocity = new Vector2(speedCap, 0f).RotateTo(projectile.AngleTo(potTarget.position));
      }

      // Force speed cap.
      if (projectile.velocity.Length() > speedCap)
        projectile.velocity = new Vector2(speedCap, 0f).RotateTo(projectile.velocity.ToRotation());
    }
  }

  public static class ParryTypeID
  {
    public const int Universal = 0;
    public const int Tank = 1;
    public const int DPS = 2;
    public const int Support = 3;
  }
  public static class ElementID
  {
    public const int Typeless = -1;
    public const int Fire = 0;
    public const int Ice = 1;
    public const int Lightning = 2;
    public const int Water = 3;
    public const int Earth = 4;
    public const int Air = 5;
    public const int Radiant = 6;
    public const int Necrotic = 7;
  }
}