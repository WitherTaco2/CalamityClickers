using CalamityMod.Dusts;
using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.HM
{
    public class StellarClicker : ModdedClickerWeapon
    {
        public static string StellarScythe { get; internal set; } = string.Empty;
        public override float Radius => 4.1f;
        public override Color RadiusColor => new Color(109, 242, 196);
        public override void SetStaticDefaultsExtra()
        {
            StellarScythe = ClickerSystem.RegisterClickEffect(Mod, "StellarScythe", 7, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(SoundID.Item71, position);
                Projectile.NewProjectile(source, position, Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * 5, ModContent.ProjectileType<StellarClickerProjectile>(), damage, knockBack, player.whoAmI);
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, StellarScythe);
            SetDust(Item, ModContent.DustType<AstralBasic>());

            Item.damage = 67;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
        }
    }
    public class StellarClickerProjectile : ModdedClickerProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Enemy/MantisRing";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaultsExtra()
        {
            Projectile.width = 72;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            //Projectile.DamageType = ModContent.Get;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 8;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 7;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            //Frames
            Projectile.frameCounter++;
            if (Projectile.frameCounter == 10)
            {
                Projectile.frame++;
                if (Projectile.frame > Main.projFrames[Projectile.type] - 1)
                    Projectile.frame = 0;
                Projectile.frameCounter = 0;
            }

            //Main AI
            if (Projectile.timeLeft > 270)
                Projectile.velocity *= 0.9f;
            if (Projectile.timeLeft == 200)
                Projectile.velocity = (Main.MouseWorld - Projectile.Center).SafeNormalize(Vector2.Zero) * 30;
        }

    }
}
