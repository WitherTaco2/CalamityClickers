using CalamityMod;
using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.DLC
{
    public class BereftVassalsClicker : ModdedClickerWeapon
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("InfernumMode", out var _);
        }
        public static string TaurusFang { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(36, 94, 187);
        public override bool SetBorderTexture => true;
        public override void SetStaticDefaultsExtra()
        {
            TaurusFang = ClickerSystem.RegisterClickEffect(Mod, "TaurusFang", 20, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Vector2 pos = position;

                NPC npc = CalamityUtils.ClosestNPCAt(pos, 1000, false, true);
                if (npc != null)
                {
                    Vector2 vector = npc.Center - pos;
                    float speed = 10f;
                    float mag = vector.Length();
                    if (mag > speed)
                    {
                        mag = speed / mag;
                        vector *= mag;
                    }
                    int branch = 6;
                    for (int i = -branch; i < branch; i++)
                    {
                        Projectile.NewProjectile(source, pos, vector.RotatedBy(0.2f * i) * (1f + MathF.Sin((float)(i + branch) / (branch * 2 + 1) * MathHelper.Pi) / 2), ModContent.ProjectileType<BereftVassalsClickerPro>(), damage / 2, knockBack, player.whoAmI);
                    }
                }
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, TaurusFang);
            SetDust(Item, DustID.Obsidian);

            Item.damage = 95;
            Item.knockBack = 1f;
            if (ModLoader.TryGetMod("InfernumMode", out var result))
                Item.rare = result.Find<ModRarity>("InfernumVassalRarity").Type;
            else
                Item.rare = ItemRarityID.Cyan;
            Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
        }
    }
    public class BereftVassalsClickerPro : ModdedClickerProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("InfernumMode", out var _);
        }
        public override bool UseInvisibleProjectile => false;
        public override void SetDefaultsExtra()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.aiStyle = -1;
            AIType = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[0] = -1;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == -1)
            {
                CalamityUtils.HomeInOnNPC(Projectile, false, 1000, 10, 80);
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            else
            {
                Projectile.Center = Main.npc[(int)Projectile.ai[0]].Center - new Vector2(Projectile.ai[1], Projectile.ai[2]);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.timeLeft < 500;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == -1)
            {
                Projectile.ai[0] = target.whoAmI;
                Vector2 v = target.Center - Projectile.Center;
                Projectile.ai[1] = v.X;
                Projectile.ai[2] = v.Y;
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.velocity = Vector2.Zero;
                Projectile.timeLeft = 300;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.ai[1] == 1)
            {
                modifiers.FinalDamage /= 4;
            }
            if (Projectile.ai[0] != -1)
            {
                Projectile.ai[1] = 1;
            }
        }
    }
}
