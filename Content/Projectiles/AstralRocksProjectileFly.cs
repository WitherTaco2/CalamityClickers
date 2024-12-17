using CalamityMod;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Projectiles
{
    public class AstralRocksProjectileFly : ModProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override string Texture => ModContent.GetInstance<AstralRocksProjectile>().Texture;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 13;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 100;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.aiStyle = -1;
            AIType = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public override void AI()
        {
            CalamityUtils.HomeInOnNPC(Projectile, true, 2000, 70, 40);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1);
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], Color.Purple * 0.5f, 1);
            return false;
        }

    }
}
