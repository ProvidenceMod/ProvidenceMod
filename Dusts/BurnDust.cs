using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.Lighting;
using static ProvidenceMod.ProvidenceUtils;
using System;

namespace ProvidenceMod.Dusts
{
  public class BurnDust : ModDust
  {
    public Vector2[] oldPos = new Vector2[] { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), };
    public bool velocitySet;
    public double AngularVelocity;
    public float degrees;
    public int cooldown = 60;
    public int lifeTime = 10;
    public Vector4 color = new Vector4(255, 128, 0, 0);
    public bool rising;
    public bool lowering = true;
    public override void OnSpawn(Dust dust)
    {
      dust.scale = 1.25f;
      dust.noGravity = true;
      dust.noLight = true;
    }
    public override bool MidUpdate(Dust dust)
    {
      return true;
    }
    public override bool Update(Dust dust)
    {
      AddLight(dust.position, ProvidenceUtils.ColorRGBIntToFloat(new Vector3(227, 79, 79)));
      return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor) => ColorShift(new Color(255, 255, 0), new Color(255, 0, 0), 5f);
  }
}