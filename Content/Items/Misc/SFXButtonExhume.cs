using ClickerClass;
using ClickerClass.Items.Misc;
using System;
using Terraria.Audio;

namespace CalamityClickers.Content.Items.Misc
{
    public class SFXButtonExhume : SFXButtonBase
    {
        public static void PlaySound(int stack)
        {
            SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/WeaponExhume").WithVolumeScale(0.5f * stack) with
            {
                PitchVariance = 0.2f,
            };
            SoundEngine.PlaySound(style);
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ClickerSystem.RegisterSFXButton(this, (Action<int>)PlaySound); //The cast is necessary here to avoid a warning
        }
    }
}
