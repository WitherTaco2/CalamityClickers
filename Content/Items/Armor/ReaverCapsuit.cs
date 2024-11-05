using CalamityMod.Items;
using CalamityMod.Items.Armor.Reaver;
using ClickerClass.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ReaverCapsuit : ClickerItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Vanity";
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
            Item.rare = ItemRarityID.Lime;
            Item.vanity = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ReaverScaleMail>() && legs.type == ModContent.ItemType<ReaverCuisses>();
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }
    }
}
