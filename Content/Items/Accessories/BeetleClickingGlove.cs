using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using ClickerClass;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BeetleClickingGlove : ClickerItem, ILocalizedModType, IModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
            Item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ClickerPlayer>().accRegalClickingGlove = true;
            if (player.statLife < player.statLifeMax2 / 10 * 2)
                player.GetDamage<ClickerDamage>() += 0.25f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<RegalClickingGlove>()
                .AddIngredient<NecklaceofVexation>()
                .AddIngredient(ItemID.BeetleHusk, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    public class BeetleClickingGloveDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.CalClicker().clickDebuff < npc.buffTime[buffIndex])
                npc.CalClicker().clickDebuff = npc.buffTime[buffIndex];
            npc.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}
