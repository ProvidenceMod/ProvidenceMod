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
    public static void AfterImage(Projectile projectile, Color lightColor, Texture2D texture, int counter)
    {
      int height = texture.Height / (int) Main.projFrames[projectile.type];
      int y = height * projectile.frame;
      float rotation = projectile.rotation;
      Rectangle rectangle = new Rectangle(0, y, texture.Width, height);
      Vector2 origin = Utils.Size(rectangle) / 2f;
      for (int i = projectile.oldPos.Length - 1; i > 0; i--)
      {
        projectile.oldPos[i] = projectile.oldPos[i - 1];
      }
      projectile.oldPos[0] = projectile.position;
      for (int k = 0; k < counter; k++) 
      {
				Vector2 previous = projectile.position;
				if (k > 0) 
        {
					previous = projectile.oldPos[k - 1];
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
      Player player = Main.player[projectile.owner];
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
      if(offset.X > 0)
      {
        if(projectile.velocity.X < 0)
          projectile.velocity.X /= slowStrength;
        if(projectile.velocity.X < speedCap)
          projectile.velocity.X += gainStrength;
      }
      if (offset.X < 0)
      {
        if(projectile.velocity.X > 0)
          projectile.velocity.X /= slowStrength;
        if(projectile.velocity.X > -speedCap)
          projectile.velocity.X -= gainStrength;
      }
      if (offset.X == 0)
        projectile.velocity.X = 0f;
      /////
      if(offset.Y > 0)
      {
        if(projectile.velocity.Y < 0)
          projectile.velocity.Y /= slowStrength;
        if(projectile.velocity.Y < speedCap)
          projectile.velocity.Y += gainStrength;
      }
      if (offset.Y < 0)
      {
        if(projectile.velocity.Y > 0)
          projectile.velocity.Y /= slowStrength;
        if(projectile.velocity.Y > -speedCap)
          projectile.velocity.Y -= gainStrength;
      }
      if (offset.Y == 0)
        projectile.velocity.Y = 0f;
    }

    public static void IsHomingPlayer(Projectile projectile, Vector2 offset, Player target, float speedCap, float gainStrength, float slowStrength)
    {
      if(offset.X > 0)
      {
        if(projectile.velocity.X < 0)
          projectile.velocity.X /= slowStrength;
        if(projectile.velocity.X < speedCap)
          projectile.velocity.X += gainStrength;
      }
      if (offset.X < 0)
      {
        if(projectile.velocity.X > 0)
          projectile.velocity.X /= slowStrength;
        if(projectile.velocity.X > -speedCap)
          projectile.velocity.X -= gainStrength;
      }
      if (offset.X == 0)
        projectile.velocity.X = 0f;
      /////
      if(offset.Y > 0)
      {
        if(projectile.velocity.Y < 0)
          projectile.velocity.Y /= slowStrength;
        if(projectile.velocity.Y < speedCap)
          projectile.velocity.Y += gainStrength;
      }
      if (offset.Y < 0)
      {
        if(projectile.velocity.Y > 0)
          projectile.velocity.Y /= slowStrength;
        if(projectile.velocity.Y > -speedCap)
          projectile.velocity.Y -= gainStrength;
      }
      if (offset.Y == 0)
        projectile.velocity.Y = 0f;
    }
    public override void SetDefaults(Projectile projectile)
    {
      switch(projectile.type)
      {
        case ProjectileID.Flames:
          projectile.Unbidden().element = 0; // Fire
          break;
      }
    }
  }
}