using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providence.Projectiles
{
	public static class ProvidenceGlobalProjectileAI
	{
		public enum ZephyrDartAI
		{
			Normal = 0,
			Friendly = 1,
			WeakHoming = 2,
			AcceleratedHoming = 3,
			Spiral = 4,
			Helix = 5,
			Gravity = 6
		}
	}
}
