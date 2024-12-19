using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class FishyClicker : ModdedClickerWeapon
    {
        public static string ReelIn { get; internal set; } = string.Empty;
        public override float Radius => 2.6f;
        public override Color RadiusColor => new Color(249, 179, 27);
        public override void SetStaticDefaultsExtra()
        {
            ReelIn = ClickerSystem.RegisterClickEffect(Mod, "ReelIn", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {

            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ReelIn);
            SetDust(Item, DustID.GemAmber);

            Item.damage = 10;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Orange;
            Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
        }
    }
    public class FishyClickerNPC : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 14;
            NPC.aiStyle = -1;
            AIType = -1;
            NPC.life = 50;
            //NPC.friendly = true;
            NPC.DeathSound = SoundID.NPCDeath1;
        }
        /*public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            
        }*/
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (NPC.life < hit.Damage)
            {
                //Main.player[projectile.owner].AddBuff(ModContent.GetInstance<>(), 10*60);
            }
        }
    }
}
