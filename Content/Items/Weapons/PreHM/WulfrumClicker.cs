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
            CalamityClickersUtils.RegisterBlacklistedClickEffect(BasicAutoclicker);
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
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}
