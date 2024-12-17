using CalamityClickers.Content.Cooldowns;
using CalamityClickers.Content.Items.Accessories;
using CalamityClickers.Content.Items.Armor;
using CalamityClickers.Content.Items.Weapons;
using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Melee;
using ClickerClass;
using ClickerClass.Buffs;
using ClickerClass.Core.Netcode.Packets;
using ClickerClass.Items;
using ClickerClass.Projectiles;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersPlayer : ModPlayer
    {
        public float rageRegenMult = 0;

        public bool clickerSelected = false;

        public bool daedalusClicker = false;
        public bool ataxiaClicker = false;
        public bool tarragonClicker = false;
        public int tarragonClickerPower = 0;
        public int tarragonClickerTime = 0;
        public bool bloodflareClicker = false;
        public bool godSlayerClicker = false;
        public int godSlayerClickerPower = 0;
        public int godSlayerClickerCritCounter = 0;

        public bool fingerOfBG = false;
        public bool beetleClickingGlove = false;
        public bool bloodyChocolate = false;
        public Item bloodyChocolateItem = null;
        public int bloodyChocolateCookieCD = 0;

        public bool bloodyCookieBuff = false;
        public bool godSlayerClickerBuff = false;

        public bool enchLecherous;
        public override void ResetEffects()
        {
            rageRegenMult = 0;

            clickerSelected = false;

            daedalusClicker = false;
            ataxiaClicker = false;
            tarragonClicker = false;
            bloodflareClicker = false;
            godSlayerClicker = false;

            fingerOfBG = false;
            beetleClickingGlove = false;
            bloodyChocolate = false;
            bloodyChocolateItem = null;

            bloodyCookieBuff = false;
            godSlayerClickerBuff = false;

            enchLecherous = false;
        }
        public override void PostUpdateEquips()
        {
            if (bloodflareClicker)
            {
                float num1 = (Player.statLife / Player.statLifeMax2);
                Player.Clicker().clickerBonusPercent += 0.2f * (1f - num1);

                //ClickerCompat.SetClickerRadiusAdd(Player, bloodflareRadius);
            }

            //Tarragon
            if (tarragonClickerTime > 0)
                tarragonClickerTime--;
            else
                tarragonClickerPower = 0;
            if (tarragonClicker)
            {
                Player.statDefense += tarragonClickerPower;
                //float power = 1f - 1f / (1f + 0.02f * tarragonClickerPower);
                //Player.endurance += power;
                Player.endurance += 0.02f * tarragonClickerPower;
            }

            //God Slayer
            if (godSlayerClicker)
                Player.GetCritChance<ClickerDamage>() += godSlayerClickerPower;
            else
                godSlayerClickerPower = 0;

            Item heldItem = Player.HeldItem;
            if (ClickerSystem.IsClickerWeapon(heldItem, out ClickerItemCore clickerItem))
            {
                //EnableClickEffect(clickerItem.itemClickEffects);
                clickerSelected = true;
                if (bloodyChocolateItem != null && !bloodyChocolateItem.IsAir && clickerSelected)
                {
                    Player.Clicker().accCookieItem = null;
                    bloodyChocolateCookieCD++;
                    if (Player.whoAmI == Main.myPlayer && bloodyChocolateCookieCD > 600)
                    {
                        int radius = (int)(95 * Player.Clicker().clickerRadius);
                        if (radius > 350)
                        {
                            radius = 350;
                        }

                        //Sqrt for bias outwards
                        Vector2 pos = Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) * radius * (float)Math.Sqrt(Main.rand.NextFloat(0.1f, 1f));

                        Projectile.NewProjectile(Player.GetSource_Accessory(bloodyChocolateItem), pos + Player.Center, Vector2.Zero, ModContent.ProjectileType<BloodyChocCookiesProjectileCookie>(), 0, 0f, Player.whoAmI);

                        bloodyChocolateCookieCD = 0;
                    }

                    //Cookie Click
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        int cookieType = ModContent.ProjectileType<BloodyChocCookiesProjectileCookie>();
                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            Projectile proj = Main.projectile[i];

                            if (proj.active && proj.owner == Player.whoAmI && proj.type == cookieType && proj.ModProjectile is BloodyChocCookiesProjectileCookie cookie)
                            {
                                if (Main.mouseLeft && Main.mouseLeftRelease && proj.DistanceSQ(Main.MouseWorld) < 30 * 30)
                                {
                                    if (cookie.Frame == 1)
                                    {
                                        SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                                        Player.AddBuff(ModContent.BuffType<CookieBuff>(), 600);
                                        Player.HealLife(10);
                                        for (int k = 0; k < 10; k++)
                                        {
                                            Dust dust = Dust.NewDustDirect(proj.Center, 20, 20, DustID.GemTopaz, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.15f);
                                            dust.noGravity = true;
                                        }
                                    }
                                    else if (cookie.Frame == 2)
                                    {
                                        SoundEngine.PlaySound(SoundID.Item4, Player.Center);
                                        Player.AddBuff(ModContent.BuffType<BloodyChocCookiesBuff>(), 600);
                                        //Player.HealLife(-20);

                                        //Anti healing
                                        {
                                            /*if (Player.statLife >= Player.statLifeMax2)
                                            {
                                                return;
                                            }*/

                                            int result = -20;
                                            Player.statLife += result;
                                            Player.HealEffect(result);
                                            /*if (Player.statLife > Player.statLifeMax2)
                                            {
                                                Player.statLife = Player.statLifeMax2;
                                            }*/
                                            if (Player.statLife <= 0)
                                            {
                                                //Player.statLife = 0;
                                                PlayerDeathReason reason = PlayerDeathReason.ByCustomReason(LangHelper.GetText("Mods.CalamityClickers.Misc.Death.BloodyClicker" + Main.rand.Next(2).ToString()));
                                                Player.KillMe(reason, 1000, 0, false);
                                            }
                                            if (Player.whoAmI != Main.myPlayer)
                                            {
                                                NetMessage.SendData(66, -1, -1, null, Player.whoAmI, result);
                                            }
                                        }

                                        for (int k = 0; k < 10; k++)
                                        {
                                            Dust dust = Dust.NewDustDirect(proj.Center, 20, 20, DustID.GemRuby, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.15f);
                                            dust.noGravity = true;
                                        }
                                    }
                                    else
                                    {
                                        SoundEngine.PlaySound(SoundID.Item2, Player.Center);
                                        Player.AddBuff(ModContent.BuffType<CookieBuff>(), 300);
                                        for (int k = 0; k < 10; k++)
                                        {
                                            Dust dust = Dust.NewDustDirect(proj.Center, 20, 20, 0, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 75, default, 1.5f);
                                            dust.noGravity = true;
                                        }
                                    }

                                    Player.Clicker().paintingCondition_ClickedCookiesCount++;
                                    if (Player.Clicker().paintingCondition_ClickedCookiesCount == 100)
                                    {
                                        Player.Clicker().paintingCondition_Clicked100Cookies = true;
                                        if (Main.netMode == NetmodeID.MultiplayerClient)
                                        {
                                            new Clicked100CookiesPacket(Player).Send();
                                        }
                                    }

                                    proj.Kill();
                                }
                            }
                        }

                    }
                }

                if (Player.whoAmI == Main.myPlayer)
                {
                    if (Player.Clicker().clickerInRange && Player.Clicker().clickerSelected)
                    {

                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            Projectile proj = Main.projectile[i];
                            if (proj.active && proj.owner == Player.whoAmI &&
                                proj.ModProjectile is ClickableClickerProjectile clickable && !clickable.HasChanged && !clickable.Trigger)
                            {
                                if (Main.mouseLeft && Main.mouseLeftRelease && proj.DistanceSQ(Main.MouseWorld) < 30 * 30)
                                {
                                    clickable.Trigger = true; //Handled in the AI
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void PostUpdateMiscEffects()
        {
            Player.Clicker().clickerBonusPercent = 1f / (2f - Player.Clicker().clickerBonusPercent);

            UpdateRippers();
        }
        private void UpdateRippers()
        {
            ref float rage = ref Player.Calamity().rage;
            ref float rageMax = ref Player.Calamity().rageMax;


            // This is how much Rage will be changed by this frame.
            float rageDiff = 0;

            // If the player equips multiple rage generation accessories they get the max possible effect without stacking any of them.
            {
                float rageGen = 0f;

                // Shattered Community provides constant rage generation (stronger than Heart of Darkness).
                if (Player.Calamity().shatteredCommunity)
                {
                    float scRageGen = rageMax * ShatteredCommunity.RagePerSecond / 60f;
                    if (rageGen < scRageGen)
                        rageGen = scRageGen;
                }
                // Heart of Darkness grants constant rage generation.
                else if (Player.Calamity().heartOfDarkness)
                {
                    float hodRageGen = rageMax * HeartofDarkness.RagePerSecond / 60f;
                    if (rageGen < hodRageGen)
                        rageGen = hodRageGen;
                }

                rageDiff += rageGen;
            }

            // Holding Gael's Greatsword grants constant rage generation.
            if (Player.Calamity().heldGaelsLastFrame)
                rageDiff += rageMax * GaelsGreatsword.RagePerSecond / 60f;

            // Calculate and grant proximity rage.
            // Regular enemies can give up to 1x proximity rage. Bosses can give up to 3x. Multiple regular enemies don't stack.
            // Proximity rage is maxed out when within 10 blocks (160 pixels) of the enemy's hitbox.
            // Its max range is 50 blocks (800 pixels), at which you get zero proximity rage.
            // Proximity rage does not generate while Rage Mode is active.
            if (!Player.Calamity().rageModeActive)
            {
                float bossProxRageMultiplier = 3f;
                float minProxRageDistance = 160f;
                float maxProxRageDistance = 800f;
                float enemyDistance = maxProxRageDistance + 1f;
                float bossDistance = maxProxRageDistance + 1f;

                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (npc.type == NPCID.None || !npc.IsAnEnemy() || !npc.Calamity().ProvidesProximityRage)
                        continue;

                    // Take the longer of the two directions for the NPC's hitbox to be generous.
                    float generousHitboxWidth = Math.Max(npc.Hitbox.Width / 2f, npc.Hitbox.Height / 2f);
                    float hitboxEdgeDist = npc.Distance(Player.Center) - generousHitboxWidth;

                    // If this enemy is closer than the previous, reduce the current minimum proximity distance.
                    if (enemyDistance > hitboxEdgeDist)
                    {
                        enemyDistance = hitboxEdgeDist;

                        // If they're a boss, reduce the boss distance.
                        // Boss distance will always be >= enemy distance, so there's no need to do another check.
                        // Worm boss body and tail segments are not counted as bosses for this calculation.
                        if (npc.IsABoss() && !CalamityLists.noRageWormSegmentList.Contains(npc.type))
                            bossDistance = hitboxEdgeDist;
                    }
                }

                // Helper function to implement proximity rage formula
                float ProxRageFromDistance(float dist)
                {
                    // Adjusted distance with the 160 grace pixels added in. If you're closer than that it counts as zero.
                    float d = Math.Max(dist - minProxRageDistance, 0f);

                    // The first term is exponential decay which reduces rage gain significantly over distance.
                    // The second term is a linear component which allows a baseline but weak rage generation even at far distances.
                    // This function takes inputs from 0.0 to 640.0 and returns a value from 1.0 to 0.0.
                    float r = 1f / (0.034f * d + 2f) + (590.5f - d) / 1181f;
                    return MathHelper.Clamp(r, 0f, 1f);
                }

                // If anything is close enough then provide proximity rage.
                // You can only get proximity rage from one target at a time. You gain rage from whatever target would give you the most rage.
                if (enemyDistance <= maxProxRageDistance)
                {
                    // If the player is close enough to get proximity rage they are also considered to have rage combat frames.
                    // This prevents proximity rage from fading away unless you run away without attacking for some reason.
                    Player.Calamity().rageCombatFrames = Math.Max(Player.Calamity().rageCombatFrames, 3);

                    float proxRageFromEnemy = ProxRageFromDistance(enemyDistance);
                    float proxRageFromBoss = 0f;
                    if (bossDistance <= maxProxRageDistance)
                        proxRageFromBoss = bossProxRageMultiplier * ProxRageFromDistance(bossDistance);

                    float finalProxRage = Math.Max(proxRageFromEnemy, proxRageFromBoss);

                    // 300% proximity rage (max possible from a boss) will fill the Rage meter in 15 seconds.
                    // 100% proximity rage (max possible from an enemy) will fill the Rage meter in 45 seconds.
                    rageDiff += finalProxRage * rageMax / CalamityUtils.SecondsToFrames(45f);
                }
            }

            bool rageFading = Player.Calamity().rageCombatFrames <= 0 && !Player.Calamity().heartOfDarkness && !Player.Calamity().shatteredCommunity;

            // If Rage Mode is currently active, you smoothly lose all rage over the duration.
            //if (Player.Calamity().rageModeActive)
            //    rageDiff -= rageMax / Player.Calamity().RageDuration;

            // If out of combat and NOT using Heart of Darkness or Shattered Community, Rage fades away.
            //else if (!Player.Calamity().rageModeActive && rageFading)
            //    rageDiff -= rageMax / CalamityUtils.SecondsToFrames(30);

            // Apply the rage change and cap rage in both directions.
            // Changes are only applied if the Rage mechanic is available.
            if (Player.Calamity().RageEnabled)
            {
                rage += rageDiff * rageRegenMult;
                if (rage < 0f)
                    rage = 0f;

                if (rage >= rageMax)
                {
                    // If Rage is not active, it is capped at 100%.
                    if (!Player.Calamity().rageModeActive)
                        rage = rageMax;

                    // If using the Shattered Community, Rage is capped at 200% while it's active.
                    // This prevents infinitely stacking rage before a fight by standing on spikes/lava with a regen build or the Nurse handy.
                    else if (Player.Calamity().shatteredCommunity && rage >= 2f * rageMax)
                        rage = 2f * rageMax;
                }
            }
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ClickerSystem.IsClickerWeapon(item))
            {
                var clickerPlayer = Player.GetModPlayer<ClickerPlayer>();

                bool preventsClickEffects = Player.CanAutoReuseItem(item) && clickerPlayer.ActiveAutoReuseEffect.PreventsClickEffects;
                if (!preventsClickEffects)
                {
                    foreach (var name in ClickerSystem.GetAllEffectNames())
                    {
                        if (clickerPlayer.HasClickEffect(name, out ClickEffect effect))
                        {
                            if (fingerOfBG && !Player.HasCooldown(FingerOfBloodGodCooldown.ID))
                            {
                                effect.Action?.Invoke(Player, source, position, type, damage, knockback);
                                Player.AddCooldown(FingerOfBloodGodCooldown.ID, 60 * 10);
                            }
                        }
                    }
                }
                return false;
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.type == ModContent.ProjectileType<ClickDamage>())
            {
                if (ataxiaClicker)
                {
                    target.AddBuff(ModContent.BuffType<HydrothermicCapsuitDebuff>(), (int)MathHelper.Clamp(target.CalClicker().hydrothermicBoil + 6, 5 * 60, 20 * 60));
                }
                if (tarragonClicker && !Player.HasCooldown(TarragonClickerCooldown.ID))
                {
                    tarragonClickerPower++;
                    if (tarragonClickerPower > 50)
                        tarragonClickerPower = 50;
                    tarragonClickerTime += 60;
                    if (tarragonClickerTime > 50 * 60)
                        tarragonClickerTime = 50 * 60;
                }
                if (bloodflareClicker)
                {
                    Player.lifeRegenTime += 2;
                }
                if (godSlayerClicker && !godSlayerClickerBuff)
                {
                    if (!hit.Crit)
                        godSlayerClickerPower++;
                    else
                    {
                        godSlayerClickerPower = 0;
                        if (!Player.HasCooldown(UltraboostCooldown.ID))
                        {
                            godSlayerClickerCritCounter++;
                            if (godSlayerClickerCritCounter > 50)
                            {
                                Player.AddBuff(ModContent.BuffType<GodSlayerCapsuitBuff>(), 5 * 60);
                                Player.AddCooldown(UltraboostCooldown.ID, 30 * 60);
                                godSlayerClickerCritCounter = 0;
                            }
                        }
                    }
                }
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (proj.DamageType is ClickerDamage)
            {
                //if (godSlayerClicker)
                if (godSlayerClickerBuff)
                    modifiers.SetCrit();
            }
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            tarragonClickerPower = 0;
            Player.AddCooldown(TarragonClickerCooldown.ID, 10 * 60);
        }
    }
}
