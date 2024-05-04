using ClickerClass;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public static class CalamityClickersUtils
    {
        public static ClickerPlayer Clicker(this Player player) => player.GetModPlayer<ClickerPlayer>();
        public static Player Owner(this Projectile proj) => Main.player[proj.owner];
        public static string RegisterClickEffect(Mod mod, string internalName, int amount, Color color, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, bool postMoonLord = false, object[] nameArgs = null, object[] descriptionArgs = null)
        {
            string clickerEffect = ClickerCompat.RegisterClickEffect(mod, internalName, amount, () => color, action, preHardMode, nameArgs, descriptionArgs);
            if (postMoonLord)
                CalamityClickersSystem.PostMLClickerEffects.Add(clickerEffect);
            return clickerEffect;
        }
        public static string RegisterClickEffect(Mod mod, string internalName, int amount, Func<Color> color, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, bool postMoonLord = false, object[] nameArgs = null, object[] descriptionArgs = null)
        {
            string clickerEffect = ClickerCompat.RegisterClickEffect(mod, internalName, amount, color, action, preHardMode, nameArgs, descriptionArgs);
            if (postMoonLord)
                CalamityClickersSystem.PostMLClickerEffects.Add(clickerEffect);
            return clickerEffect;
        }
        public static void RegisterBlacklistedClickEffect(string clickEffectName)
        {
            if (!CalamityClickersSystem.BlacklistedClickerEffects.Contains(clickEffectName))
                CalamityClickersSystem.BlacklistedClickerEffects.Add(clickEffectName);
        }
    }
}
