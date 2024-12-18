using CalamityClickers.Content.Items.Accessories;
using ClickerClass.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = projectile.Owner();
            //Legecy Hydrothermic clicker armor set bonus
            /*if (player.GetModPlayer<CalamityClickersPlayer>().ataxiaClicker && ClickerCompat.IsClickerItem(player.HeldItem))
            {
                if (Main.player[Main.myPlayer].lifeSteal > 0 && target.canGhostHeal && (target.type != NPCID.TargetDummy && target.type != ModContent.NPCType<SuperDummyNPC>()) && !player.moonLeech)
                {
                    float healMultiplier = 0.1f - projectile.numHits * 0.05f;
                    float healAmount = projectile.damage * healMultiplier;
                    if (healAmount > 50f)
                        healAmount = 50f;
                    if (CalamityGlobalProjectile.CanSpawnLifeStealProjectile(healMultiplier, healAmount))
                        CalamityGlobalProjectile.SpawnLifeStealProjectile(projectile, player, healAmount, ModContent.ProjectileType<HydrothermicHealOrb>(), 1200f, 2f);
                }
            }*/
            if (player.CalClicker().accBeetleClickingGlove && projectile.type == ModContent.ProjectileType<ClickDamage>())
            {
                target.AddBuff(ModContent.BuffType<BeetleClickingGloveDebuff>(), 120);
                target.CalClicker().clickDebuffOwner = projectile.owner;
            }
        }
    }
}
