using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public static class LangHelper
    {
        /// <summary>
        /// prefixes the modname for the key
        /// </summary>
        /// <returns>Text associated with this key</returns>
        public static string GetTextByMod(Mod mod, string key, params object[] args)
        {
            return Language.GetTextValue($"Mods.{mod.Name}.{key}", args);
        }

        /// <summary>
        /// Defaults to Mods.CalamityClickers. as the prefix for the key
        /// </summary>
        /// <returns>Text associated with this key</returns>
        internal static string GetText(string key, params object[] args)
        {
            return GetTextByMod(CalamityClickers.mod, key, args);
        }
        public static LocalizedText GetLocalizedText(Mod mod, string key)
        {
            return Language.GetOrRegister($"Mods.{mod.Name}.{key}");
        }
        internal static LocalizedText GetLocalizedText(string key)
        {
            return GetLocalizedText(CalamityClickers.mod, key);
        }
    }
}
