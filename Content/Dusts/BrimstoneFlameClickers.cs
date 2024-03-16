using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Dusts
{
    public class BrimstoneFlameClickers : ModDust
    {
        /*public override void OnSpawn(Dust dust)
        {
            Dust.ap
        }*/
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}
