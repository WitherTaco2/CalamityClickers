using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class MushyClicker : ModdedClickerWeapon
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public override float Radius => 2.1f;
        public override Color RadiusColor => new Color(90, 167, 209);
        public override void SafeSetStaticDefaults()
        {
            MushyClicker.ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "Mushy", 1, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                player.AddBuff(ModContent.BuffType<Mushy>(), 120);
            }, true);
        }
        public override void SafeSetDefaults()
        {
            AddEffect(Item, MushyClicker.ClickerEffect);

            Item.damage = 6;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Green;
            Item.value = CalamityGlobalItem.Rarity2BuyPrice;
        }
    }
}
