using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Rarities;
using ClickerClass;
using ClickerClass.Items;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AuricTeslaRadarCapsuit : ClickerItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Capsuit";
        public override void SetStaticDefaults()
        {
            ClickerCompat.RegisterClickerItem(this);
        }

        public override void SetDefaults()
        {
            Item.width = 27;
            Item.height = 22;
            Item.value = CalamityGlobalItem.Rarity15BuyPrice;
            Item.rare = ModContent.RarityType<Violet>();
            Item.defense = 38;
        }

        public override void UpdateEquip(Player player)
        {
            //ClickerCompat.SetDamageAdd(player, 0.13f);
            //ClickerCompat.SetClickerCritAdd(player, 13);
            //ClickerCompat.SetClickerRadiusAdd(player, WitherTacoLib.Math.Radius(0.5f));
            //ClickerCompat.SetClickerBonusPercentAdd(player, 0.1f);

            player.GetDamage<ClickerDamage>() += 0.3f;
            player.GetCritChance<ClickerDamage>() += 30;
            player.Clicker().clickerBonusPercent -= 0.1f;
            player.Clicker().clickerRadius += 1f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            Mod calamity = ModLoader.GetMod("CalamityMod");
            return body.type == ModContent.ItemType<AuricTeslaBodyArmor>() && legs.type == ModContent.ItemType<AuricTeslaCuisses>();
        }

        public override void UpdateArmorSet(Player player)
        {
            //player.setBonus = "Clicker Tarragon, Bloodflare, God Slayer, and Silva armor effects\n30% increased clicker damage and crit";
            player.setBonus = ILocalizedModTypeExtensions.GetLocalizedValue(this, "SetBonus");

            //ClickerCompat.SetDamageAdd(player, 0.30f);
            //ClickerCompat.SetClickerCritAdd(player, 30);
            //player.GetDamage<ClickerDamage>() += 0.3f;
            //player.GetCritChance<ClickerDamage>() += 30;

            //Tarragon
            player.Calamity().tarraSet = true;

            //Bloodflare
            player.Calamity().bloodflareSet = true;
            player.crimsonRegen = true;
            //player.GetModPlayer<ClickerAddonPlayer>().bloodflareCapsuit = true;
            player.lavaMax += 240;
            player.ignoreWater = true;

            //God Slayer
            player.Calamity().godSlayer = true;
            //player.GetModPlayer<ClickerAddonPlayer>().godSlayerCapsuit = true;

            //Silva - Now not used
            //CalamityCompat.CalamityArmorSetBonus(player, "silva");
            //player.GetModPlayer<ClickerAddonPlayer>().silvaCapsuit = true;

            //Auric
            player.Calamity().auricSet = true;
            player.thorns += 3f;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        /*public override void AddRecipes()
		{
			CreateRecipe()
				//.AddIngredient<>()
				//.AddIngredient<>()
				//.AddIngredient<>()
				.AddTile<DraedonsForge>()
				.Register();

        }*/
    }
}