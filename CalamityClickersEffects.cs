using ClickerClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersEffects : ModSystem
    {
        #region Registered Effects
        //Armor
        public static string DaedalusSet { get; internal set; } = string.Empty;
        //Clickers
        public static string WildMagic { get; internal set; } = string.Empty;
        public override void SetStaticDefaults()
        {
            WildMagic = CalamityClickersUtils.RegisterClickEffect(Mod, "WildMagicNew", 6, new Color(175, 75, 255), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                List<string> excluded = new List<string>
                {
                    CalamityClickersEffects.WildMagic,
                    ClickEffect.WildMagic,
                    ClickEffect.Mania,
                    ClickEffect.AutoClick,
                    ClickEffect.PhaseReach,
                    ClickEffect.Bold,
                    ClickEffect.Yoink,
                    ClickEffect.Nab
                };

                List<ClickEffect> allowed = new List<ClickEffect>();

                foreach (var name in ClickerSystem.GetAllEffectNames())
                {
                    if (!excluded.Contains(name) && !CalamityClickersSystem.PostMLClickerEffects.Contains(name) && ClickerSystem.IsClickEffect(name, out ClickEffect effect))
                    {
                        allowed.Add(effect);
                    }
                }

                if (allowed.Count == 0) return;

                ClickEffect random = Main.rand.Next(allowed);
                random.Action?.Invoke(player, source, position, type, damage, knockBack);
            });
        }
        #endregion
    }
}
