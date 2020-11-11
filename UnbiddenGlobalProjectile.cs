using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnbiddenMod
{
  public class UnbiddenGlobalProjectile : GlobalProjectile
  {

    // Elemental variables for Projectiles

    public int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place
    
    public bool inverseKB = false;
    // Elemental variables also contained within GlobalItem, GlobalNPC, and Player
    public override bool InstancePerEntity => true;

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