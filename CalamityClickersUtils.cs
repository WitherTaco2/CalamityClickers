using ClickerClass;
using Microsoft.Xna.Framework;
using ClickerClass.Items;
using System.Collections.Generic;
using Terraria;

namespace CalamityClickers
{
    public static class CalamityClickersUtils
    {
        public static ClickerPlayer Clicker(this Player player) => player.GetModPlayer<ClickerPlayer>();
        public static CalamityClickersPlayer CalClicker(this Player player) => player.GetModPlayer<CalamityClickersPlayer>();
        public static CalamityClickersGlobalNPC CalClicker(this NPC npc) => npc.GetGlobalNPC<CalamityClickersGlobalNPC>();
        public static Player Owner(this Projectile proj) => Main.player[proj.owner];
        /*public static string RegisterClickEffect(Mod mod, string internalName, int amount, Color color, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, bool postWildMagic = false, object[] nameArgs = null, object[] descriptionArgs = null)
        {
            string clickerEffect = ClickerCompat.RegisterClickEffect(mod, internalName, amount, () => color, action, preHardMode, nameArgs, descriptionArgs);
            if (postWildMagic)
                CalamityClickers.extraAPI.Call("RegisterPostWildMagicClickerEffect", clickerEffect);
            return clickerEffect;
        }
        public static string RegisterClickEffect(Mod mod, string internalName, int amount, Func<Color> color, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, bool postWildMagic = false, object[] nameArgs = null, object[] descriptionArgs = null)
        {
            string clickerEffect = ClickerCompat.RegisterClickEffect(mod, internalName, amount, color, action, preHardMode, nameArgs, descriptionArgs);
            if (postWildMagic)
                CalamityClickers.extraAPI.Call("RegisterPostWildMagicClickerEffect", clickerEffect);
            return clickerEffect;
        }*/
        public static List<string> GetHeldClickerEffects(this Player Player) => Player.HeldItem.GetGlobalItem<ClickerItemCore>().itemClickEffects;
        public static void RegisterPostWildMagicClickEffect(string clickEffectName)
        {
            if (!(CalamityClickers.extraAPI.Call("GetPostWildMagicClickerEffectList") as List<string>).Contains(clickEffectName))
                CalamityClickers.extraAPI.Call("RegisterPostWildMagicClickerEffect", clickEffectName);
        }
        public static void RegisterPostNightmareMagicClickEffect(string clickEffectName)
        {
            if (!CalamityClickersSystem.PostNightmareClickerEffects.Contains(clickEffectName))
                CalamityClickersSystem.PostNightmareClickerEffects.Add(clickEffectName);
        }
        public static void RegisterBlacklistedClickEffect(string clickEffectName)
        {
            if (!(CalamityClickers.extraAPI.Call("GetBlacklistedRandomClickerEffectList") as List<string>).Contains(clickEffectName))
                CalamityClickers.extraAPI.Call("RegisterBlacklistedRandomClickerEffect", clickEffectName);
        }
        public static Color GetColorFromHex(string hexValue)
        {
            var cc1 = System.Drawing.ColorTranslator.FromHtml("#" + hexValue.Replace("#", ""));
            return new Color(cc1.R, cc1.G, cc1.B);
        }
    }
}
