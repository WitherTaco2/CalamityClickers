using ClickerClass;
using ClickerClass.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class IntergelacticCapsuit : ClickerItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Capsuit";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CatalystMod", out var _);
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.defense = 18;
            if (ModLoader.TryGetMod("CatalystMod", out var result))
                Item.rare = result.Find<ModRarity>("SuperbossRarity").Type;
            else
                Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 30);
            Item.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage<ClickerDamage>() += 0.15f;
            player.GetCritChance<ClickerDamage>() += 15;
            player.Clicker().clickerRadius += 0.6f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (ModLoader.TryGetMod("CatalystMod", out var result))
                return body.type == result.Find<ModItem>("IntergelacticBreastplate").Type && legs.type == result.Find<ModItem>("IntergelacticGreaves").Type;
            else
                return true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = this.GetLocalizedValue("SetBonus");

            player.CalClicker().setIntergelacticClicker = Item;
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CatalystMod", out var result))
            {
                CreateRecipe()
                    .AddIngredient(ModContent.ItemType<StatigelCapsuit>())
                    .AddIngredient(result.Find<ModItem>("MetanovaBar").Type, 6)
                    .AddTile(TileID.LunarCraftingStation)
                    .Register();
            }
        }

    }

}
