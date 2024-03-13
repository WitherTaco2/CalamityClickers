using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersPlayer : ModPlayer
    {
        public bool daedalusClicker = false;
        public bool hydrothermicClicker = false;
        public bool bloodflareClicker = false;
        public bool godSlayerClicker = false;
        public override void ResetEffects()
        {
            daedalusClicker = false;
            hydrothermicClicker = false;
            bloodflareClicker = false;
            godSlayerClicker = false;
        }
        public override void PostUpdateEquips()
        {
            if (bloodflareClicker)
            {
                float num1 = 0.2f * (Player.statLife / Player.statLifeMax2);
                Player.Clicker().clickerBonusPercent += 0.2f - num1;

                //ClickerCompat.SetClickerRadiusAdd(Player, bloodflareRadius);
            }
        }
    }
}
