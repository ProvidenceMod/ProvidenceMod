using Terraria.ID;
using Terraria.ModLoader;

namespace UnbiddenMod.NPCs
{
  // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
  public class FireSlime : ModNPC
  {
    public override bool Autoload(ref string name)
    {
      name = "FireSlime";
      return mod.Properties.Autoload;
    }

    public override void SetStaticDefaults()
    {
      // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
      DisplayName.SetDefault("Fire Slime");
    }

    public override void SetDefaults()
    {
      npc.Unbidden().resists = new float[8] { 0.25f, 1.5f, 0.25f, 0.5f, 1f, 1f, 1f, 1f };
      npc.Unbidden().contactDamageEl = 0; // Fire
      npc.townNPC = false;
      npc.width = 24;
      npc.height = 18;
      npc.aiStyle = 1;
      npc.damage = 10;
      npc.defense = 2;
      npc.lifeMax = 35;
      npc.HitSound = SoundID.NPCHit1;
      npc.DeathSound = SoundID.NPCDeath1;
      npc.value = 25;
      npc.buffImmune[BuffID.OnFire] = true;
      animationType = 1;
    }
  }
}