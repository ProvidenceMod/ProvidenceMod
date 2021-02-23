using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProvidenceMod.Projectiles;

namespace ProvidenceMod
{
  public class ProvidenceGlobalProjectile : GlobalProjectile
  {
    public override bool InstancePerEntity => true;
    public bool amped;
    public bool bloodAmpBoosted;
    public bool Deflectable { get => homingID == -1 && !deflectableOverride; }
    public bool deflectableOverride = false;
    public bool deflected;
    public bool inverseKB;
    public int element = -1;
    public int homingID = -1;
    public Item shotBy;
    public static void AfterImage(Projectile projectile, Color lightColor, Texture2D texture, int counter)
    {
      int height = texture.Height / Main.projFrames[projectile.type];
      int y = height * projectile.frame;
      float rotation = projectile.rotation;
      Rectangle rectangle = new Rectangle(0, y, texture.Width, height);
      Vector2 origin = rectangle.Size() / 2f;
      float alpha = 1f;
      for (int i = projectile.oldPos.Length - 1; i > 0; i--)
      {
        projectile.oldPos[i] = projectile.oldPos[i - 1];
      }
      projectile.oldPos[0] = projectile.position;
      for (int k = 0; k < counter; k++)
      {
        if (k == 0)
        {
          alpha = 1f;
        }
        else
        {
          // alpha = 1f - (k * (1f / counter / 2));
          alpha -= 0.1f;
        }
        Color color = projectile.GetAlpha(lightColor) * alpha;
        Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition;
        Main.spriteBatch.Draw(texture, drawPos, rectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
      }
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
            if (projectile.Providence().element == 0)
              Main.combatText[combatIndex2].color = new Color(238, 74, 89);
            else if (projectile.Providence().element == 1)
              Main.combatText[combatIndex2].color = new Color(238, 74, 204);
            else if (projectile.Providence().element == 2)
              Main.combatText[combatIndex2].color = new Color(238, 226, 74);
            else if (projectile.Providence().element == 3)
              Main.combatText[combatIndex2].color = new Color(74, 95, 238);
            else if (projectile.Providence().element == 4)
              Main.combatText[combatIndex2].color = new Color(74, 238, 137);
            else if (projectile.Providence().element == 5)
              Main.combatText[combatIndex2].color = new Color(145, 74, 238);
            else if (projectile.Providence().element == 6)
              Main.combatText[combatIndex2].color = new Color(255, 216, 117);
            else if (projectile.Providence().element == 7)
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
            if (projectile.Providence().element == 0)
              Main.combatText[combatIndex2].color = new Color(238, 74, 89);
            else if (projectile.Providence().element == 1)
              Main.combatText[combatIndex2].color = new Color(238, 74, 204);
            else if (projectile.Providence().element == 2)
              Main.combatText[combatIndex2].color = new Color(238, 226, 74);
            else if (projectile.Providence().element == 3)
              Main.combatText[combatIndex2].color = new Color(74, 95, 238);
            else if (projectile.Providence().element == 4)
              Main.combatText[combatIndex2].color = new Color(74, 238, 137);
            else if (projectile.Providence().element == 5)
              Main.combatText[combatIndex2].color = new Color(145, 74, 238);
            else if (projectile.Providence().element == 6)
              Main.combatText[combatIndex2].color = new Color(255, 216, 117);
            else if (projectile.Providence().element == 7)
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
          projectile.Providence().element = 0; // Fire
          break;
      }
    }
  }
}