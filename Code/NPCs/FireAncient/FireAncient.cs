using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace UnbiddenMod.Code.NPCs.FireAncient
{
    public class FireAncient : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            name = "FireAncient";
            return mod.Properties.Autoload;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Ancient");
        }
        public override void SetDefaults()
        {
            music = mod.GetSoundSlot(SoundType.Music, "Code/Music/FromTheDepths");
			musicPriority = MusicPriority.BossMedium; // By default, musicPriority is BossLow
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 100000;
            npc.townNPC = false;
        }
        public void OnSpawn() //optional
        {
            if(Main.netMode == 0)
            {
                Main.NewText("An ancient being from the firey depths has awoken!", 241, 127, 82, false);
            }
            else if(Main.netMode == 2)
            {
                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral("An ancient being from the firey depths has awoken!"), 255, 241, 127, 82, 0); 
            }
        }

        public override void AI() //this is where you program your AI
        {
            
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D tex = mod.GetTexture("Code/NPCs/FireAncient/FireAncient");
            spriteBatch.Draw(tex, npc.position, new Rectangle((int) npc.position.X, (int) npc.position.Y, tex.Width, tex.Height), Color.White, npc.rotation, npc.Center, 1f, 0, 0);    
        }

        /*public override void HitEffect(int hitDirection, double damage, bool isDead) //This is for whenever your boss gets hit by an attack. Create dust or gore.
        {

        }

        public override void SelectFrame(int frameSize) //This is for animating your boss, such as how Queen Bee changes between charging or hovering images, or how Pumpking rotates its face.
        {

        }*/

        public override void NPCLoot() //this is what makes special things happen when your boss dies, like loot or text
        {
        
        }
    }
}