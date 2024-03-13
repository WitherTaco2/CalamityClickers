using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Armor.Aerospec;
using CalamityMod.Items.Materials;
using ClickerClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AerospecCapsuit : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Capsuit";
        public override void SetStaticDefaults()
        {
            ClickerCompat.RegisterClickerItem(this);
            //ArmorIDs.Head.Sets.Draw = true;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = CalamityGlobalItem.Rarity3BuyPrice;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<ClickerDamage>() += 0.08f;
            player.Clicker().clickerRadius += 0.3f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AerospecBreastplate>() && legs.type == ModContent.ItemType<AerospecLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            //player.setBonus = "5% increased movement speed and click critical strike chance\nTaking over 25 damage in one hit will cause a spread of homing feathers to fall\nAllows you to fall more quickly and disables fall damage";
            player.setBonus = ILocalizedModTypeExtensions.GetLocalizedValue(this, "SetBonus");
            Mod calamity = ModLoader.GetMod("CalamityMod");

            //CalamityCompat.CalamityArmorSetBonus(player, "aerospec");
            player.Calamity().aeroSet = true;
            player.moveSpeed += 0.05f;
            player.noFallDmg = true;
            player.GetCritChance<ClickerDamage>() += 5;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AerialiteBar>(5)
                .AddIngredient(ItemID.SunplateBlock, 3)
                .AddIngredient(ItemID.Feather)
                .AddTile(TileID.SkyMill)
                .Register();
        }
    }
}