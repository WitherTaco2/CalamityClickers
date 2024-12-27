using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Armor.Victide;
using ClickerClass;
using ClickerClass.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Armor
{
    public class VictideCapsuit : ClickerItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Capsuit";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
            Item.rare = ItemRarityID.Green;
            Item.defense = 3;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VictideBreastplate>() && legs.type == ModContent.ItemType<VictideGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = this.GetLocalizedValue("SetBonus") + "\n" + CalamityUtils.GetTextValueFromModItem<VictideBreastplate>("CommonSetBonus");
            player.Calamity().victideSet = true;
            player.ignoreWater = true;
            if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
            {
                player.GetDamage<ClickerDamage>() += 0.1f;
                player.lifeRegen += 3;
            }
            player.Clicker().clickerRadius += 0.2f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<MagicDamageClass>() += 0.05f;
        }
    }
}
