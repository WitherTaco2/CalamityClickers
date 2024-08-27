using CalamityMod.Items;
using CalamityMod.Items.Armor.Daedalus;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using ClickerClass;
using ClickerClass.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DaedalusCapsuit : ClickerItem, ILocalizedModType
    {
        public static string ClickerEffect { get; internal set; } = string.Empty;
        public new string LocalizationCategory => "Items.Armor.Capsuit";

        public override void SetStaticDefaults()
        {
            ClickerEffect = ClickerSystem.RegisterClickEffect(Mod, "DaedalusLightning", 15, new Color(218, 105, 233), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int k = 0; k < 3; k++)
                {
                    Projectile proj = Projectile.NewProjectileDirect(source, position - Main.rand.NextVector2Square(-10, 10) - new Vector2(0, 500), new Vector2(0, 5), ModContent.ProjectileType<DaedalusLightning>(), damage, knockBack, player.whoAmI);
                    proj.DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            });
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<ClickerDamage>() += 0.08f;
            player.GetCritChance<ClickerDamage>() += 7;
            player.Clicker().clickerRadius += 0.5f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DaedalusBreastplate>() && legs.type == ModContent.ItemType<DaedalusLeggings>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ILocalizedModTypeExtensions.GetLocalizedValue(this, "SetBonus");

            player.GetModPlayer<CalamityClickersPlayer>().daedalusClicker = true;
            player.GetDamage<ClickerDamage>() += 0.05f;
            player.Clicker().EnableClickEffect(ClickerEffect);

        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<CryonicBar>(7).
                AddIngredient<EssenceofEleum>().
                AddTile(TileID.MythrilAnvil).
                Register();
        }
    }
}
