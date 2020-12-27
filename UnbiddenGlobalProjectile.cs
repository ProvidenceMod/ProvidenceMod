using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnbiddenMod.Projectiles;

namespace UnbiddenMod
{
  public class UnbiddenGlobalProjectile : GlobalProjectile
  {
    // Elemental variables for Projectiles

    public int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place
    public bool inverseKB = false;
    // Elemental variables also contained within GlobalItem, GlobalNPC, and Player
    public override bool InstancePerEntity => true;
    public bool deflected = false;
    public bool amped = false;
    public static void AfterImage(Projectile projectile, Color lightColor, Texture2D texture, int counter)
    {
      int height = texture.Height / (int)Main.projFrames[projectile.type];
      int y = height * projectile.frame;
      float rotation = projectile.rotation;
      Rectangle rectangle = new Rectangle(0, y, texture.Width, height);
      Vector2 origin = rectangle.Size() / 2f;
      for (int i = projectile.oldPos.Length - 1; i > 0; i--)
      {
        projectile.oldPos[i] = projectile.oldPos[i - 1];
      }
      projectile.oldPos[0] = projectile.position;
      for (int k = 0; k < counter; k++)
      {
        _ = projectile.position;
        if (k > 0)
        {
          _ = projectile.oldPos[k - 1];
        }
        float alpha;
        if (k == 0)
        {
          alpha = 1f;
        }
        else
        {
          alpha = 1f - (k * 0.200f);
        }
        Color color = projectile.GetAlpha(lightColor) * alpha;
        Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition;
        Main.spriteBatch.Draw(texture, drawPos, rectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
      }
    }
    // Projectile projectile
    // Vector2 offset
    // Vector2 speedX
    // Vector2 speedY
    // float gainStrength
    // float speedCap
    // float slowStrength
    public static void IsHomingNPC(Projectile projectile, Vector2 offset, NPC target, float speedCap, float gainStrength, float slowStrength)
    {
      if (offset.X > 0)
      {
        if (projectile.velocity.X < 0)
          projectile.velocity.X /= slowStrength;
        if (projectile.velocity.X < speedCap)
          projectile.velocity.X += gainStrength;
      }
      if (offset.X < 0)
      {
        if (projectile.velocity.X > 0)
          projectile.velocity.X /= slowStrength;
        if (projectile.velocity.X > -speedCap)
          projectile.velocity.X -= gainStrength;
      }
      if (offset.X == 0)
        projectile.velocity.X = 0f;
      /////
      if (offset.Y > 0)
      {
        if (projectile.velocity.Y < 0)
          projectile.velocity.Y /= slowStrength;
        if (projectile.velocity.Y < speedCap)
          projectile.velocity.Y += gainStrength;
      }
      if (offset.Y < 0)
      {
        if (projectile.velocity.Y > 0)
          projectile.velocity.Y /= slowStrength;
        if (projectile.velocity.Y > -speedCap)
          projectile.velocity.Y -= gainStrength;
      }
      if (offset.Y == 0)
        projectile.velocity.Y = 0f;
    }

    public static void IsHomingPlayer(Projectile projectile, Vector2 offset, Player target, float speedCap, float gainStrength, float slowStrength)
    {
      if (offset.X > 0)
      {
        if (projectile.velocity.X < 0)
          projectile.velocity.X /= slowStrength;
        if (projectile.velocity.X < speedCap)
          projectile.velocity.X += gainStrength;
      }
      if (offset.X < 0)
      {
        if (projectile.velocity.X > 0)
          projectile.velocity.X /= slowStrength;
        if (projectile.velocity.X > -speedCap)
          projectile.velocity.X -= gainStrength;
      }
      if (offset.X == 0)
        projectile.velocity.X = 0f;
      /////
      if (offset.Y > 0)
      {
        if (projectile.velocity.Y < 0)
          projectile.velocity.Y /= slowStrength;
        if (projectile.velocity.Y < speedCap)
          projectile.velocity.Y += gainStrength;
      }
      if (offset.Y < 0)
      {
        if (projectile.velocity.Y > 0)
          projectile.velocity.Y /= slowStrength;
        if (projectile.velocity.Y > -speedCap)
          projectile.velocity.Y -= gainStrength;
      }
      if (offset.Y == 0)
        projectile.velocity.Y = 0f;
    }
    public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
    {
      for (int combatIndex2 = 99; combatIndex2 >= 0; --combatIndex2)
      {
        CombatText combatText = Main.combatText[combatIndex2];
        if ((combatText.lifeTime == 60 || combatText.lifeTime == 120) && combatText.alpha == 1.0)
        {
          if (combatText.color == CombatText.DamagedHostile || combatText.color == CombatText.DamagedHostileCrit)
          {
            if (projectile.Unbidden().element == 0)
              Main.combatText[combatIndex2].color = new Color(238, 74, 89);
            else if (projectile.Unbidden().element == 1)
              Main.combatText[combatIndex2].color = new Color(238, 74, 204);
            else if (projectile.Unbidden().element == 2)
              Main.combatText[combatIndex2].color = new Color(238, 226, 74);
            else if (projectile.Unbidden().element == 3)
              Main.combatText[combatIndex2].color = new Color(74, 95, 238);
            else if (projectile.Unbidden().element == 4)
              Main.combatText[combatIndex2].color = new Color(74, 238, 137);
            else if (projectile.Unbidden().element == 5)
              Main.combatText[combatIndex2].color = new Color(145, 74, 238);
            else if (projectile.Unbidden().element == 6)
              Main.combatText[combatIndex2].color = new Color(255, 216, 117);
            else if (projectile.Unbidden().element == 7)
              Main.combatText[combatIndex2].color = new Color(96, 0, 188);
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
            if (projectile.Unbidden().element == 0)
              Main.combatText[combatIndex2].color = new Color(238, 74, 89);
            else if (projectile.Unbidden().element == 1)
              Main.combatText[combatIndex2].color = new Color(238, 74, 204);
            else if (projectile.Unbidden().element == 2)
              Main.combatText[combatIndex2].color = new Color(238, 226, 74);
            else if (projectile.Unbidden().element == 3)
              Main.combatText[combatIndex2].color = new Color(74, 95, 238);
            else if (projectile.Unbidden().element == 4)
              Main.combatText[combatIndex2].color = new Color(74, 238, 137);
            else if (projectile.Unbidden().element == 5)
              Main.combatText[combatIndex2].color = new Color(145, 74, 238);
            else if (projectile.Unbidden().element == 6)
              Main.combatText[combatIndex2].color = new Color(255, 216, 117);
            else if (projectile.Unbidden().element == 7)
              Main.combatText[combatIndex2].color = new Color(96, 0, 188);
          }
        }
      }
    }
    public override void SetDefaults(Projectile projectile)
    {
      switch (projectile.type)
      {
        case ProjectileID.Flames:
          projectile.Unbidden().element = 0; // Fire
          break;
      }
    }
  }
}