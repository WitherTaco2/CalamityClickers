using CalamityClickers.Content.Cooldowns;
using CalamityClickers.Content.Items.Weapons.PostML.Yharon;
using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ClickerClass;
using ClickerClass.Items;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers.Content.Items.Weapons.Donor
{
    public class MarkOfRatKing : ModdedClickerWeapon
    {
        public static string Mischief { get; internal set; } = string.Empty;
        public override float Radius => 8;
        public override Color RadiusColor => Color.Yellow;
        //public override bool SetBorderTexture => true;
        public override void SetStaticDefaultsExtra()
        {
            Mischief = CalamityClickersUtils.RegisterClickEffect(Mod, "Mischief", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                int rat = ModContent.ProjectileType<MarkOfRatKingProjectile>();
                if (player.ownedProjectileCounts[rat] < 10)
                    Projectile.NewProjectile(source, position, Vector2.Zero, rat, damage / 10, knockBack, player.whoAmI);
            }, postMoonLord: true);
            CalamityClickersUtils.RegisterBlacklistedClickEffect(Mischief);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, Mischief);
            SetDust(Item, DustID.Obsidian);

            Item.damage = 600;
            Item.knockBack = 1f;
            Item.rare = ModContent.RarityType<HotPink>();
            Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;

            Item.Calamity().donorItem = true;
        }
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem.type == Item.type)
            {
                int num1 = player.selectedItem - 1;
                if (num1 > 0)
                {
                    if (ClickerSystem.IsClickerWeapon(player.inventory[num1], out var clickerItem))
                    {
                        List<string> itemClickEffects = clickerItem.itemClickEffects;
                        foreach (string effect in itemClickEffects)
                        {
                            player.Clicker().EnableClickEffect(effect);
                        }
                    }
                }
                int num2 = player.selectedItem + 1;
                if (num2 < player.inventory.Length - 1)
                {
                    if (ClickerSystem.IsClickerWeapon(player.inventory[num2], out var clickerItem))
                    {
                        List<string> itemClickEffects = clickerItem.itemClickEffects;
                        foreach (string effect in itemClickEffects)
                        {
                            player.Clicker().EnableClickEffect(effect);
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MiceCatalystClicker>()
                .AddIngredient<MouseClicker>()
                .AddIngredient(ItemID.Rat, 10)
                .AddIngredient<ShadowspecBar>(5)
                .AddTile(ModContent.TileType<DraedonsForge>())
                .Register();
        }
    }
    public class MarkOfRatKingProjectile : ModdedClickerProjectile
    {
        public override bool UseInvisibleProjectile => false;
        public override void SetDefaultsExtra()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            AIType = -1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = lifeTime;
        }
        public NPC target;
        private int lifeTime = 5 * 60;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            ClickerPlayer clickerPlayer = player.Clicker();

            target = Projectile.Center.ClosestNPCAt(1200f);

            if (target != null && Projectile.timeLeft < lifeTime - 10)
            {
                float inertia = 20;
                float speed = 60;
                Vector2 moveDirection = Projectile.SafeDirectionTo(target.Center, Vector2.UnitY);
                Projectile.velocity = (Projectile.velocity * (inertia - 1f) + moveDirection * speed) / inertia;

                Projectile.ai[0]++;
                if ((target.Center - Projectile.Center).Length() < 30)
                {
                    if (Projectile.ai[0] > 60 * 4 / clickerPlayer.clickerPerSecond)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ClickDamage>(), Projectile.damage, Projectile.knockBack, Projectile.owner);

                        bool preventsClickEffects = player.CanAutoReuseItem(player.HeldItem) && clickerPlayer.ActiveAutoReuseEffect.PreventsClickEffects;
                        if (!preventsClickEffects)
                        {
                            player.CalClicker().clickAmount++;
                        }

                        //clickerPlayer.clickTimers.Add(new Ref<float>(1f));
                        player.CalClicker().clickerTotal++;

                        if (!preventsClickEffects)
                        {
                            foreach (var name in ClickerSystem.GetAllEffectNames())
                            {
                                if (clickerPlayer.HasClickEffect(name, out ClickEffect effect) && name != MarkOfRatKing.Mischief)
                                {
                                    //Find click amount
                                    int clickAmountTotal = clickerPlayer.GetClickAmountTotal(player.HeldItem.GetGlobalItem<ClickerItemCore>(), name) * 3;
                                    bool reachedAmount = (clickerPlayer.clickAmount + player.CalClicker().clickAmount) % clickAmountTotal == 0;

                                    if (reachedAmount || (clickerPlayer.accTriggerFinger && clickerPlayer.OutOfCombat))
                                    {
                                        effect.Action?.Invoke(player, new EntitySource_ItemUse_WithAmmo(player, player.HeldItem, 0), Projectile.Center, ModContent.ProjectileType<ClickDamage>(), Projectile.damage, Projectile.knockBack);

                                        if (clickerPlayer.accTriggerFinger)
                                        {
                                            //TODO looks like a hack
                                            clickerPlayer.outOfCombatTimer = ClickerPlayer.OutOfCombatTimeMax;
                                        }
                                    }
                                    if (player.CalClicker().accFingerOfBG && !player.HasCooldown(FingerOfBloodGodCooldown.ID))
                                    {
                                        effect.Action?.Invoke(player, new EntitySource_ItemUse_WithAmmo(player, player.HeldItem, 0), Projectile.Center, ModContent.ProjectileType<ClickDamage>(), Projectile.damage, Projectile.knockBack);
                                        player.AddCooldown(FingerOfBloodGodCooldown.ID, 60 * 10);
                                    }
                                }
                            }
                        }

                        Projectile.ai[0] = 0;
                    }
                }
                //player.HeldItem.Shoot
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D t = TextureAssets.Projectile[Type].Value;

            Main.spriteBatch.Draw(t, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, new Vector2(t.Width, 0), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}
