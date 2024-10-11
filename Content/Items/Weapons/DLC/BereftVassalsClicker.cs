using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.DLC
{
    public class BereftVassalsClicker : ModdedClickerWeapon
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            //return ModLoader.TryGetMod("InfernumMode", out var _);
            return false;
        }
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(117, 255, 159);
        public override bool SetBorderTexture => true;
        public override void SetStaticDefaultsExtra()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "WaterSplash", 5, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(SoundID.SplashWeak with { PitchVariance = 0.5f, Volume = 1.75f });

                for (int i = 0; i < 10; i++)
                {
                    Gore bubble = Gore.NewGorePerfect(player.GetSource_FromAI(), position, Main.rand.NextVector2Unit() * Main.rand.NextFloat(0f, 4f), 411);
                    bubble.timeLeft = 8 + Main.rand.Next(6);
                    bubble.scale = Main.rand.NextFloat(0.7f, 1f);
                    bubble.type = Main.rand.NextBool(3) ? 412 : 411;
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickerEffect);
            SetDust(Item, DustID.Obsidian);

            Item.damage = 95;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
        }
    }
}
