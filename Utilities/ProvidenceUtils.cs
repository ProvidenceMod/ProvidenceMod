using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProvidenceMod.Dusts;
using ProvidenceMod.Buffs.Cooldowns;
using ProvidenceMod.Projectiles.Healing;

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
    /// <summary>References the ProvidenceGlobalWall instance. Shorthand for ease of use.</summary>
    public static ProvidenceWall ProvidenceWall(this Mod wall) => (ProvidenceWall)wall.GetGlobalWall("ProvidenceGlobalWall");
    /// <summary>References the ProvidenceWorld instance. Shorthand for ease of use.</summary>
    public static ProvidenceWorld ProvidenceWorld(this Mod world) => (ProvidenceWorld)world.GetModWorld("ProvidenceWorld");
    /// <summary>References the ProvidenceGlobalBuff instance. Shorthand for ease of use.</summary>
    public static ProvidenceBuff ProvidenceBuff(this Mod buff) => (ProvidenceBuff)buff.GetGlobalBuff("ProvidenceBuff");
    /// <summary>References the ProvidenceGlobalTile instance. Shorthand for ease of use.</summary>
    public static ProvidenceTile ProvidenceTile(this Mod tile) => (ProvidenceTile)tile.GetGlobalTile("ProvidenceTile");
    /// <summary>References the ProvidenceGlobalBigStyle instance. Shorthand for ease of use.</summary>
    public static ProvidenceBigStyle ProvidenceStyle(this Mod style) => (ProvidenceBigStyle)style.GetGlobalBgStyle("ProvidenceBigStyle");
    /// <summary>References the ProvidenceGlobalRecipes instance. Shorthand for ease of use.</summary>
    public static ProvidenceRecipes ProvidenceRecipes(this Mod recipe) => (ProvidenceRecipes)recipe.GetGlobalRecipe("ProvidenceRecipe");
    /// <summary>References the Player owner of a projectile instance. Shorthand for ease of use.</summary>
    public static Player OwnerPlayer(this Projectile projectile) => Main.player[projectile.owner];
    /// <summary>References the NPC owner of a projectile instance. Shorthand for ease of use.</summary>
    public static NPC OwnerNPC(this Projectile projectile) => Main.npc[projectile.owner];
    /// <summary>References the Main.localPlayer. Shorthand for ease of use.</summary>
    public static Player LocalPlayer() => Main.LocalPlayer;
    /// <summary>Returns the position for an Entity. Shorthand for ease of use.</summary>
    public static Vector2 EntityPosition(Entity entity) => entity.Center - Main.screenPosition;
    /// <summary>Shorthand for converting degrees of rotation into a radians equivalent.</summary>
    public static float InRadians(this float degrees) => MathHelper.ToRadians(degrees);
    /// <summary>Shorthand for converting radians of rotation into a degrees equivalent.</summary>
    public static float InDegrees(this float radians) => MathHelper.ToDegrees(radians);

    /// <summary>Automatically converts seconds into game ticks. 1 second is 60 ticks.</summary>
    public static int InTicks(this float seconds) => (int)(seconds * 60);
    /// <summary>Automatically converts seconds into game ticks. 1 second is 60 ticks.</summary>
    public static int InTicks(this int seconds) => seconds * 60;

    public static decimal Round(this decimal dec, int points) => decimal.Round(dec, points);
    public static float Round(this float f, int points) => (float)Math.Round(f, points);
    public static double Round(this double d, int points) => Math.Round(d, points);
    public static float[,] elememtalAffinityDefense = new float[2, 15]
    {  // Defense score (middle), Damage mult (bottom)
      {     1,      2,      3,      5,      7,      9,     12,     15,     18,     22,     26,     30,     35,     45,     50},
      {1.010f, 1.022f, 1.037f, 1.056f, 1.080f, 1.110f, 5.000f, 1.192f, 1.246f, 1.310f, 1.397f, 1.497f, 1.611f, 1.740f, 1.885f}
    };
    /// <summary>Calculates the elemental defense of the player based on their affinities, and any accessories and armor providing such defense.</summary>
    public static void CalcElemDefense(this Player player)
    {
      ProvidencePlayer proPlayer = player.Providence();
      for (int k = 0; k < 8; k++)
      {
        int index = proPlayer.affinities[k] - 1;
        if (index != -1)
          proPlayer.resists[k] += (int)elememtalAffinityDefense[0, index];
      }
    }
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
    public static float[] GetAffinityBonuses(this Player player, int e)
    {
      return new float[2] { elememtalAffinityDefense[0, player.Providence().affinities[e]], elememtalAffinityDefense[1, player.Providence().affinities[e]] };
    }

    /// <summary>
    /// Elemental damage calculation for when the player hits an NPC with a melee weapon.
    /// </summary>
    public static int CalcEleDamage(this Item item, NPC npc, ref int damage)
    {
      int weapEl = item.Providence().element; // Determine the element (will always be between 0-6 for array purposes)
      if (weapEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = damage, // And the damage we already have, converted to float
          resistMod = npc.Providence().resists[weapEl];
        if (resistMod > 0f)
        {
          damageFloat *= resistMod; // Multiply by the relevant resistance, divided by 100 (this is why we needed floats)
          damage = (int)damageFloat; // set the damage to the int version of the new float, implicitly rounding down to the lower int
        }
        else
        {
          damage = 1;
        }
        // ProvidencePlayer modPlayer = Main.player[item.owner].Providence();
        // if (modPlayer.affExpCooldown <= 0)
        // {
        //   modPlayer.affExp[weapEl] += 1;
        //   modPlayer.affExpCooldown = 120;
        // }
      }
      return damage;
    }
    /// <summary>
    /// Elemental damage calculation for when the player hits an NPC with a projectile.
    /// </summary>
    public static int CalcEleDamage(this Projectile projectile, NPC npc, ref int damage)
    {
      int projEl = projectile.Providence().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        float damageFloat = damage, // And the damage we already have, converted to float
          resistMod = npc.Providence().resists[projEl];
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
      int npcEl = npc.Providence().contactDamageEl;
      if (npcEl != -1)
      {
        int resistMod = player.Providence().resists[npcEl];
        damage -= (int)(Main.expertMode ? resistMod * 0.75 : resistMod * 0.5);
      }
      return damage;
    }

    /// <summary>
    /// Elemental damage calculation for when the player is hit by a projectile.
    /// </summary>
    public static int CalcEleDamageFromProj(this Player player, Projectile proj, ref int damage)
    {
      int projEl = proj.Providence().element; // Determine the element (will always be between 0-6 for array purposes)
      if (projEl != -1) // if not typeless (and implicitly within 0-6)
      {
        int resistMod = player.Providence().resists[projEl];
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
      return currProj.active && currProj.whoAmI != parryShield && !player.HasBuff(BuffType<CantDeflect>()) && currProj.Providence().Deflectable && currProj.hostile && hitbox.Intersects(currProj.Hitbox);
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
          for (float i = 0; i < 10; i++)
            _ = Dust.NewDustPerfect(currProj.position, DustType<MoonBlastDust>(), new Vector2(4f, 0f).RotatedBy((i * 36).InRadians()), 0, Color.White, 5f);
          // Add your melee damage multiplier to the damage so it has a little more oomph
          currProj.damage = (int)(currProj.damage * player.meleeDamageMult);

          // If Micit Bangle is equipped, add that multiplier.
          currProj.damage = player.Providence().micitBangle ? (int)(currProj.damage * 2.5) : currProj.damage;
          // Convert the proj so you own it and reverse its trajectory
          currProj.owner = player.whoAmI;
          currProj.hostile = false;
          currProj.friendly = true;
          currProj.Providence().deflected = true;
          currProj.velocity.X = -currProj.velocity.X;
          currProj.velocity.Y = -currProj.velocity.Y;
          affectedProjs++;
        }
      }
      player.Providence().parriedProjs += affectedProjs;
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
          for (float i = 0; i < 10; i++)
            _ = Dust.NewDustPerfect(currProj.position, DustType<MoonBlastDust>(), new Vector2(4f, 0f).RotatedBy((i * 36).InRadians()), 0, Color.White, 5f);

          DRBoost += currProj.damage / 10;
          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Providence().parriedProjs += affectedProjs;
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
          for (float i = 0; i < 10; i++)
            _ = Dust.NewDustPerfect(currProj.position, DustType<MoonBlastDust>(), new Vector2(4f, 0f).RotatedBy((i * 36).InRadians()), 0, Color.White, 5f);
          _ = Projectile.NewProjectile(
            currProj.position,
            Vector2.Negate(currProj.velocity),
            ProjectileID.ChlorophyteBullet,
            (int)(player.Providence().micitBangle ? currProj.damage * 2 * 2.5 : currProj.damage * 2),
            currProj.knockBack,
            player.whoAmI
            );

          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Providence().parriedProjs += affectedProjs;
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
          for (float i = 0; i < 10; i++)
            _ = Dust.NewDustPerfect(currProj.position, DustType<MoonBlastDust>(), new Vector2(4f, 0f).RotatedBy((i * 36).InRadians()), 0, Color.White, 5f);
          _ = Projectile.NewProjectile(new Vector2(player.position.X + 35, 0), new Vector2(0, 0), ProjectileType<HealProjectile>(), 0, 0);
          HPBoost += currProj.damage / 10;
          currProj.active = false;
          affectedProjs++;
        }
      }
      player.Providence().parriedProjs += affectedProjs;
      // player.statLife += HPBoost;
      // if (HPBoost > 0)
      // {
      //   player.HealEffect(HPBoost);
      // }
    }

    /// <summary>Generates dust particles based on Aura size. Call when adding an Aura buff.</summary>
    /// <param name="player">The active player acting as the point of origin.</param>
    /// <param name="style">The visual style that the aura will use.</param>
    /// <param name="radiusBoost">The distance modifier for the aura's effective range.</param>
    /// <param name="dust">The ID of the dust particle to use for the aura. By convention, this dust should have no gravity or light, and should have AI set to dissipate within 3-5 ticks.</param>
    /// <param name="custom">Whether or not to use a custom aura.</param>
    public static void GenerateAuraField(Player player, int style, float radiusBoost, int dust = 0, bool custom = false)
    {
      ProvidencePlayer proPlayer = player.Providence();
      if (custom)
      {
        for (float rotation = 0f; rotation < 360f; rotation += 8f)
        {
          Vector2 spawnPosition = player.MountedCenter + new Vector2(0f, proPlayer.clericAuraRadius + radiusBoost).RotatedBy(rotation.InRadians());
          Dust d = Dust.NewDustPerfect(spawnPosition, dust, null, 90, new Color(255, 255, 255), 1f);
          d.noLight = true;
          d.noGravity = true;
        }
      }
      else
      {
        switch (style)
        {
          case 0:
            break;
          case 1:
            if (proPlayer.hasClericSet)
            {
              const float burnRadiusBoost = -100f;
              GenerateAuraField(player, DustType<BurnDust>(), burnRadiusBoost);
              foreach (NPC npc in Main.npc)
              {
                if (!npc.townNPC && !npc.friendly && npc.position.IsInRadiusOf(player.MountedCenter, proPlayer.clericAuraRadius + burnRadiusBoost))
                {
                  npc.AddBuff(BuffID.OnFire, 1);
                }
              }
            }
            break;
          case 2:
            if (proPlayer.hasClericSet)
            {
              const float cFRadiusBoost = -150f;
              GenerateAuraField(player, DustType<AuraDust>(), cFRadiusBoost);
              foreach (NPC npc in Main.npc)
              {
                if (!npc.townNPC && !npc.friendly && npc.position.IsInRadiusOf(player.MountedCenter, proPlayer.clericAuraRadius + cFRadiusBoost))
                {
                  npc.AddBuff(BuffID.CursedInferno, 180);
                }
              }
            }
            break;
          case 3:
            const float ampRadiusBoost = 0;
            GenerateAuraField(player, DustType<ParryShieldDust>(), ampRadiusBoost);
            foreach (Projectile projectile in Main.projectile)
            {
              if (projectile.position.IsInRadiusOf(player.MountedCenter, proPlayer.clericAuraRadius + ampRadiusBoost) && !projectile.Providence().amped)
              {
                if (projectile.friendly)
                {
                  projectile.damage = (int)(projectile.damage * 1.15);
                  projectile.velocity *= 1.15f;
                  projectile.Providence().amped = true;
                }
                else if (projectile.hostile)
                {
                  projectile.damage = (int)(projectile.damage * 0.85);
                  projectile.velocity *= 0.85f;
                  projectile.Providence().amped = true;
                }
              }
            }
            break;
        }
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
    public static bool IsInRadiusOf(this Vector2 targetPos, Vector2 center, float radius) => Vector2.Distance(center, targetPos) <= radius;
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
    /// <summary>Gradually shifts between two colors over ingame time.</summary>
    public static Color ColorShift(Color firstColor, Color secondColor, float seconds)
    {
      float amount = (float)((Math.Sin(Math.PI * Math.PI / seconds * Main.GlobalTime) + 1.0) * 0.5);
      return Color.Lerp(firstColor, secondColor, amount);
    }
    // public static Color ColorShiftThree(Color firstColor, Color secondColor, Color thirdColor, float seconds, int phase)
    // {
    //   if(phase == 0)
    //   {
    //     return ColorShift(firstColor, secondColor, seconds);
    //   }
    //   if(phase == 1)
    //   {
    //     return ColorShift(secondColor, thirdColor, seconds);
    //   }
    //   if(phase == 2)
    //   {
    //     return ColorShift(thirdColor, firstColor, seconds);
    //   }
    //   else
    //   {
    //     return ColorShift(firstColor, secondColor, seconds);
    //   }
    // }
    /// <summary>For use in setting defaults in items.</summary>
    /// <param name="item">The item being set.</param>
    /// <param name="elementID">The element of a weapon's attacks, or additional elemental defense of accessories and armor.</param>
    /// <param name="elementDefense">The amount of elemental defense provided in the element given to "elementID" Defaults to 0.</param>
    /// <param name="weakElementID">The element of a weapon's weakness. Only really used for armor. Defaults to -1 (Typeless).</param>
    /// <param name="weakElementDefense">The amount of elemental defense lowered in the element given to "weakElementID". Defaults to 0.</param>
    public static void SetElementalTraits(this Item item, int elementID, int elementDefense = 0, int weakElementID = -1, int weakElementDefense = 0)
    {
      item.Providence().element = elementID;
      item.Providence().elementDefense = elementDefense;
      if (weakElementID != -1)
      {
        item.Providence().weakElement = weakElementID;
        item.Providence().weakElementDefense = weakElementDefense;
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
    /// <summary>Returns the player's bonuses originated from their focus. In a tuple for ease of access.</summary>
    public static Tuple<int, decimal, decimal, decimal> FocusBonuses(this Player player)
    {
      ProvidencePlayer mP = player.Providence();
      int focusPercent = (int)(mP.focus * 100);
      int damageBoost = focusPercent / 5;
      decimal DR = (decimal)((mP.focus / 4 >= 0.25f ? 0.25f : mP.focus / 4) * 100);
      decimal regen = focusPercent;
      decimal moveSpeedBoost = focusPercent / 2;
      return new Tuple<int, decimal, decimal, decimal>(damageBoost, DR.Round(2), regen.Round(2), moveSpeedBoost.Round(2));
    }
    /// <summary>Provides a random point near the Vector2 you call this on.</summary>
    /// <param name="v">The origin point.</param>
    /// <param name="maxDist">The distance in pixels away from the origin to move. Defaults at 16f, or 1 tile.</param>
    public static Vector2 RandomPointNearby(this Vector2 v, float maxDist = 16f)
    {
      return Vector2.Add(v, new Vector2(0, Main.rand.NextFloat(maxDist)).RotatedByRandom(180f.InRadians()));
    }
    public static Vector2 RandomPointInHitbox(this Rectangle hitbox)
    {
      int hBounds = Main.rand.Next(hitbox.Left, hitbox.Right),
          vBounds = Main.rand.Next(hitbox.Top, hitbox.Bottom);
      return Vector2.Add(hitbox.Center.ToVector2(), new Vector2(hBounds, vBounds));
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
    /// <summary>Draws a glowmask for the given item.</summary>
    /// <param name="spriteBatch">The spriteBatch instance for this glowmask. Passed with "PostDraw" or "PreDraw" item methods.</param>
    /// <param name="rotation">The rotation for this item. Passed with "PostDraw" item methods.</param>
    /// <param name="glowmaskTexture">The texture to draw for this glowmask.</param>
    public static void Glowmask(this Item item, SpriteBatch spriteBatch, float rotation, Texture2D glowmaskTexture)
    {
      Vector2 origin = new Vector2(glowmaskTexture.Width / 2f, (float)((glowmaskTexture.Height / 2.0) - 2.0));
      spriteBatch.Draw(glowmaskTexture, item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
    }
    /// <summary>Draws a glowmask for the given item while the item is in use.</summary>
    /// <param name="info">The PlayerDrawInfo instance for this method. Use "PlayerDrawInfo info = new PlayerDrawInfo();" and pass it.</param>
    public static void DrawGlowmask(PlayerDrawInfo info)
    {
      Player player = info.drawPlayer;
      Texture2D glowmaskTexture = null;
      if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir)
      {
        if (player.HeldItem.Providence().glowmask)
          glowmaskTexture = player.HeldItem.Providence().glowmaskTexture;
        Main.playerDrawData.Add(
          new DrawData(
            glowmaskTexture,
            info.itemLocation - Main.screenPosition,
            glowmaskTexture.Frame(),
            Color.White,
            player.itemRotation,
            new Vector2(player.direction == 1 ? 0 : glowmaskTexture.Width, glowmaskTexture.Height),
            player.HeldItem.scale,
            info.spriteEffects,
            0
          )
        );
      }
    }
    /// <summary>Draws an animated glowmask for the given item while the item is in use.</summary>
    /// <param name="info">The PlayerDrawInfo instance for this method. Use "PlayerDrawInfo info = new PlayerDrawInfo();" and pass it.</param>
    public static void DrawGlowmaskAnimation(PlayerDrawInfo info)
    {
      Player player = info.drawPlayer;
      Texture2D animationTexture = null;
      Rectangle frame = default;
      if (player != null && player.itemAnimation != 0 && !player.HeldItem.IsAir)
      {
        if (player.HeldItem.Providence().glowmask)
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

    /// <summary>
    /// A smart homing AI for all projectiles to use in their AIs. A good cover-all to allow homing without constant retyping.
    /// <para>This is a free-to-use code example for our open source, so adopt code as you need!</para>
    /// </summary>
    /// <param name="projectile">The projectile being worked with.</param>
    /// <param name="speed">How fast the projectile can go in a straight line. Defaults at 6f.</param>
    /// <param name="gain">How quickly the projectile will gain speed. Defaults at 0.1f </param>
    /// <param name="slow">How quickly the projectile will slow down. Defaults at 0.1f.</param>
    /// <param name="trackingRadius">How far away from its target it can be and still chase after. Defaults at 200f.</param>
    /// <param name="overshotPrevention">Whether or not there should be a radius where it will guarantee its hit, even if hitboxes don't intersect. Defaults to false.</param>
    /// <param name="overshotThreshold">If overshotPrevention is true, provides the radius which will guarantee the hit. Defaults to 0f.</param>
    /// <param name="courseAdjust">Whether or not the projectile should never overshoot the axis. Defaults to false.</param>
    /// <param name="courseRange">If courseAdjust is true, provides the range which will activate course adjustment. The range is centered around the axis of the target. Defaults to 5f.</param>
    public static void Homing(this Projectile projectile, Entity entity, float speed = 8f, float gain = 0.1f, float slow = 0.1f, float turn = 1f, float trackingRadius = 200f, bool overshotPrevention = false, float overshotThreshold = 5f, bool courseAdjust = false, float courseRange = 5f)
    {
      Vector2 target = entity == null ? default : entity.Hitbox.Center.ToVector2() - projectile.position;
      switch (projectile.Providence().homingID)
      {
        case HomingID.Smart:
          if (entity?.active == true && entity.position.IsInRadiusOf(projectile.position, trackingRadius))
            projectile.velocity = projectile.velocity.SmartHoming(projectile, entity, target, gain, slow, courseAdjust, courseRange, overshotPrevention, overshotThreshold, speed);
          break;
        case HomingID.Gravity:
          if (entity?.active == true && entity.position.IsInRadiusOf(projectile.position, trackingRadius))
            GravityHoming(projectile, entity, speed, turn, trackingRadius, overshotPrevention, overshotThreshold);
          break;
        case HomingID.Sine:
          break;
        case HomingID.Linear:
          break;
        case HomingID.Natural:
          if (entity?.active == true && entity.position.IsInRadiusOf(projectile.position, trackingRadius))
            NaturalHoming(projectile, entity, turn, speed);
          break;
        case HomingID.Smooth:
          if (entity?.active == true && entity.position.IsInRadiusOf(projectile.position, trackingRadius))
            SmoothHoming(projectile);
          break;
        case HomingID.Complex:
          break;
      }
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
    /// A smart homing AI.
    /// <para>This is a free-to-use code example for our open source, so adopt code as you need!</para>
    /// </summary>
    /// <param name="velocity">The velocity of the projectile.</param>
    /// <param name="offset">The offset from the projectile position to the target position.</param>
    /// <param name="gain">How fast the projectile gains speed.</param>
    /// <param name="slow">How fast the projectile loses speed</param>
    /// <param name="courseAdjust">Whether or not the projectile will prevent overshooting the axes of the target.</param>
    /// <param name="courseRange">The range that courseAdjust will activate within.</param>
    /// <param name="overshotPrevention">Whether or not the projectile will guarentee a hit within a certain distnace.</param>
    /// <param name="overshotThreshold">The range that overshotPrevention will guarantee a hit within.</param>
    /// <param name="speedCap">The max speed this projectile can reach.</param>
    public static Vector2 SmartHoming(this Vector2 velocity, Projectile projectile, Entity target, Vector2 offset, float gain = 0.1f, float slow = 0.1f, bool courseAdjust = true, float courseRange = 5f, bool overshotPrevention = false, float overshotThreshold = 10f, float speedCap = 8f)
    {
      if (offset.X > 0)
      {
        if (velocity.X < 0)
          velocity.X /= slow;
        if (velocity.X < speedCap)
          velocity.X += gain;
        if (velocity.X > speedCap)
          velocity.X = speedCap;
      }
      if (offset.X < 0)
      {
        if (velocity.X > 0)
          velocity.X /= slow;
        if (velocity.X > -speedCap)
          velocity.X -= gain;
        if (velocity.X < -speedCap)
          velocity.X = -speedCap;
      }
      if (offset.Y > 0)
      {
        if (velocity.Y < 0)
          velocity.Y /= slow;
        if (velocity.Y < speedCap)
          velocity.Y += gain;
        if (velocity.Y > speedCap)
          velocity.Y = speedCap;
      }
      if (offset.Y < 0)
      {
        if (velocity.Y > 0)
          velocity.Y /= slow;
        if (velocity.Y > -speedCap)
          velocity.Y -= gain;
        if (velocity.Y < -speedCap)
          velocity.Y = -speedCap;
      }
      if (courseAdjust)
      {
        if (offset.X <= courseRange && !(offset.X < 0))
        {
          if (!(offset.X < 1))
            velocity.X /= slow;
          if (offset.X < 1)
            velocity.X = 0f;
        }
        if (offset.X >= -courseRange && !(offset.X > 0))
        {
          if (!(offset.X > -1))
            velocity.X /= slow;
          if (offset.X > -1)
            velocity.X = 0f;
        }
        if (offset.Y <= courseRange && !(offset.Y < 0))
        {
          if (!(offset.Y < 1))
            velocity.Y /= slow;
          if (offset.Y < 1)
            velocity.Y = 0f;
        }
        if (offset.Y >= -courseRange && !(offset.Y > 0))
        {
          if (!(offset.Y > -1))
            velocity.Y /= slow;
          if (offset.Y > -1)
            velocity.Y = 0f;
        }
      }
      if (overshotPrevention && target.position.IsInRadiusOf(projectile.position, overshotThreshold))
      {
        velocity.RotateTo(projectile.AngleTo(target.position));
      }
      return velocity;
    }

    /// <summary>
    /// A smart homing AI for all projectiles to use in their AIs. A good cover-all to allow homing without constant retyping.
    /// <para>This is a free-to-use code example for our open source, so adopt code as you need!</para>
    /// </summary>
    /// <param name="projectile">The projectile being worked with.</param>
    /// <param name="speedCap">How fast the projectile can go in a straight line. Defaults at 6f.</param>
    /// <param name="turnStrength">How quickly the projectile will gain turn. Defaults at 0.1f </param>
    /// <param name="trackingRadius">How far away from its target it can be and still chase after. Defaults at 200f.</param>
    /// <param name="overshotPrevention">Whether or not there should be a radius where it will guarantee its hit, even if hitboxes don't intersect. Defaults to false.</param>
    /// <param name="overshotThreshold">If overshotPrevention is true, provides the radius which will guarantee the hit. Defaults to 0f.</param>
    public static void GravityHoming(this Projectile projectile, Entity entity, float speedCap = 6f, float turnStrength = 0.1f, float trackingRadius = 200f, bool overshotPrevention = false, float overshotThreshold = 0f)
    {
      // Slightly different tracking methods between hostile and friendly AIs. Not much, but enough.
      if (projectile.friendly)
      {
        if (entity?.position.IsInRadiusOf(projectile.position, trackingRadius) == true)
          projectile.velocity = projectile.velocity.TurnTowardsByX(projectile.AngleTo(entity.position), turnStrength);
        // If overshotPrevention is on, force the projectile to beeline right for the target if it's within threshold distance.
        if (overshotPrevention && entity?.position.IsInRadiusOf(projectile.position, overshotThreshold) == true)
          projectile.velocity = new Vector2(speedCap, 0f).RotateTo(projectile.AngleTo(entity.position));
        // Force speed cap.
        if (projectile.velocity.Length() > speedCap)
          projectile.velocity = new Vector2(speedCap, 0f).RotateTo(projectile.velocity.ToRotation());
      }
    }
    //TODO: Code this AI
    public static void SineHoming()
    { }
    //TODO: Code this AI
    public static void LinearHoming()
    { }
    //TODO: Clarify this AI
    public static void SmoothHoming(Projectile projectile)
    {
      float velocityTriangle = (float)Math.Sqrt((projectile.velocity.X * (double)projectile.velocity.X) + (projectile.velocity.Y * (double)projectile.velocity.Y));
      float localAI = projectile.localAI[0];
      if ((double)localAI == 0.0)
      {
        projectile.localAI[0] = velocityTriangle;
        localAI = velocityTriangle;
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
    public static void NaturalHoming(Projectile projectile, Entity entity, float turn, float homing)
    {
      Vector2 unitY = projectile.DirectionTo(entity.Center);
      projectile.velocity = ((projectile.velocity * turn) + (unitY * homing)) / (turn + 1f);
    }
    //TODO: Code this AI
    public static void ComplexHoming()
    {
    }
    public static int ArmorCalculation(NPC npc, ref int damage, ref bool crit)
    {
      int armorDamage = 0;
      // If the NPC is armored
      if (npc.Providence().Armored)
      {
        crit = false;
        // Deduct the damage from the armor
        armorDamage += (int)(damage * npc.Providence().armorEfficiency);
        npc.Providence().armor -= armorDamage;
        damage = (int)(damage * (1f - npc.Providence().armorEfficiency));
        // If the armor ends up being less than 0
        if (npc.Providence().armor < 0)
        {
          // Signify that as "overflow damage" by deducting the abs of the remainder from the NPC's life
          armorDamage += npc.Providence().armor;
          // Subtracting negative == adding positive
          npc.life += npc.Providence().armor;
          damage -= npc.Providence().armor;
          npc.Providence().armor = 0;
        }
        CombatText.NewText(npc.Hitbox, Color.White, armorDamage);
      }
      return damage;
    }
    public static void DrawEnemyDebuffs(this NPC nPC)
    {
      nPC.Providence().buffCount = 0;
      Vector2 drawPos = new Vector2(0f, nPC.position.Y + 10f);
      int[] buffTypeArray = new int[5] { 0, 0, 0, 0, 0 };
      foreach (int num in nPC.buffType)
      {
        buffTypeArray[nPC.Providence().buffCount] = num;
        nPC.Providence().buffCount++;
      }
      for (int i = 0; i < nPC.Providence().buffCount; i++)
      {
        drawPos.X = nPC.position.X - 10f + (i * 5f);
        Texture2D texture = GetTexture("Terraria/Buff_" + buffTypeArray[i].ToString());
        Main.spriteBatch.Draw(texture, drawPos, Color.White);
      }
    }
    /// <summary>A slightly modified Vector2 based on the Center of the Entity given. There's some weirdness to what is considered the center in the computer's eyes; this fixes that.</summary>
    public static Vector2 TrueCenter(this Entity ent) => new Vector2(ent.Center.X - 3, ent.Center.Y - 3);

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
    public static class HomingID
    {
      public const int Smart = 0;
      public const int Gravity = 1;
      public const int Sine = 2;
      public const int Linear = 3;
      public const int Natural = 4;
      public const int Smooth = 5;
      public const int Complex = 6;
    }
    public static class AuraStyle
    {
      public const int CerberusStyle = 0;
      public const int BurnStyle = 1;
      public const int CFlameStyle = 2;
      public const int AmpStyle = 3;
    }
    public static class AuraType
    {
      public const int CerberusAura = 0;
      public const int BurnAura = 1;
      public const int CFlameAura = 2;
      public const int AmpCapacitorAura = 3;
    }
    public static class EntityType
    {
      public const int NPC = 0;
      public const int Player = 1;
      public const int Projectile = 2;
      public const int Entity = 3;
    }
  }
}