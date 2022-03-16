using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Providence
{
	public class ProvidenceGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		public float[] extraAI = new float[6] { 0, 0, 0, 0, 0, 0 };

		public int element = -1;

		public Vector2[] oldCen = new Vector2[10] { new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f) };
	}
}