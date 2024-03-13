using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Healing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = projectile.Owner();
            if (player.GetModPlayer<CalamityClickersPlayer>().hydrothermicClicker && ClickerCompat.IsClickerItem(player.HeldItem))
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
            }
        }
    }
}
