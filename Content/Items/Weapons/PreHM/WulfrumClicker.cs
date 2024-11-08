using CalamityMod.Items;
using CalamityMod.Items.Materials;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class WulfrumClicker : ModdedClickerWeapon
    {
        public static string BasicAutoclicker { get; internal set; } = string.Empty;
        public override float Radius => 1.5f;
        public override Color RadiusColor => new Color(160, 237, 0);
        public override void SetStaticDefaultsExtra()
        {
            BasicAutoclicker = ClickerSystem.RegisterClickEffect(Mod, "BasicAutoclicker", 1, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {

            }, true);
            CalamityClickersUtils.RegisterBlacklistedClickEffect(ClickerEffect);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, BasicAutoclicker);
            SetDust(Item, 229);

            Item.damage = 4;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.value = CalamityGlobalItem.RarityBlueBuyPrice;

        }
        public bool autoClicker = true;
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem.type == Item.type && autoClicker)
            {
                ClickerCompat.SetAutoReuseEffect(player, 8);
                player.AddBuff(ModContent.BuffType<WulfrumClickerBuff>(), 3);
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                autoClicker = !autoClicker;
                return true;
            }
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<WulfrumMetalScrap>(8)
                .AddIngredient<EnergyCore>()
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    public class WulfrumClickerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[base.Type] = true;
            Main.buffNoSave[base.Type] = true;
        }
    }
}
