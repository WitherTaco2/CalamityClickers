using CalamityClickers.Content.Buffs;
using CalamityClickers.Content.Cooldowns;
using CalamityMod;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersPlayer : ModPlayer
    {
        public bool daedalusClicker = false;
        public bool ataxiaClicker = false;
        public bool bloodflareClicker = false;
        public bool godSlayerClicker = false;

        public bool fingerOfBG;
        public override void ResetEffects()
        {
            daedalusClicker = false;
            ataxiaClicker = false;
            bloodflareClicker = false;
            godSlayerClicker = false;

            fingerOfBG = false;
        }
        public override void PostUpdateEquips()
        {
            if (bloodflareClicker)
            {
                float num1 = (Player.statLife / Player.statLifeMax2);
                Player.Clicker().clickerBonusPercent += 0.2f * (1f - num1);

                //ClickerCompat.SetClickerRadiusAdd(Player, bloodflareRadius);
            }
        }
        public override void PostUpdateMiscEffects()
        {
            Player.Clicker().clickerBonusPercent = 1f / (2f - Player.Clicker().clickerBonusPercent);
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ClickerSystem.IsClickerWeapon(item))
            {
                var clickerPlayer = Player.GetModPlayer<ClickerPlayer>();

                bool preventsClickEffects = Player.CanAutoReuseItem(item) && clickerPlayer.ActiveAutoReuseEffect.PreventsClickEffects;
                if (!preventsClickEffects)
                {
                    foreach (var name in ClickerSystem.GetAllEffectNames())
                    {
                        if (clickerPlayer.HasClickEffect(name, out ClickEffect effect))
                        {
                            if ((fingerOfBG && Main.rand.NextBool(10)))
                                effect.Action?.Invoke(Player, source, position, type, damage, knockback);

                        }
                    }
                }
                return false;
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (CalamityKeybinds.ArmorSetBonusHotKey.JustPressed && godSlayerClicker && !Player.HasCooldown(GodSlayerOverclockCooldown.ID))
            {
                Player.AddCooldown(GodSlayerOverclockCooldown.ID, 60 * 60);
                Player.AddBuff(ModContent.BuffType<GodSlayerOverclockBuff>(), 2 * 60);
            }
        }
    }
}
