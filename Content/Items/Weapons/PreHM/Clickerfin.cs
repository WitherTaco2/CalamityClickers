﻿using CalamityMod;
using CalamityMod.BiomeManagers;
using CalamityMod.Items;
using ClickerClass;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityClickers.Content.Items.Weapons.PreHM
{
    public class Clickerfin : ModdedClickerWeapon
    {
        public static string ReelIn { get; internal set; } = string.Empty;
        public override float Radius => 2.6f;
        public override Color RadiusColor => new Color(249, 179, 27);
        public override void SetStaticDefaultsExtra()
        {
            ReelIn = ClickerSystem.RegisterClickEffect(Mod, "ReelIn", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Vector2 pos = position + Main.rand.NextVector2CircularEdge(100, 100);
                NPC.NewNPC(source, (int)pos.X, (int)pos.Y, ModContent.NPCType<ClickerfinNPC>(), 0, Main.rand.NextFloat(0.5f, 2f));
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
    public class ClickerfinNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.HideFromBestiary();
        }
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
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment) => NPC.lifeMax = 50;
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (NPC.life < hit.Damage)
            {
                Main.player[projectile.owner].AddBuff(ModContent.BuffType<ClickerfinBuff>(), 10 * 60);
            }
        }
        public override void AI()
        {
            NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;
            NPC.velocity = new Vector2(NPC.velocity.X, MathF.Sin(Main.GlobalTimeWrappedHourly * NPC.ai[0]) / 10f);
            NPC.ai[1]++;
            if (NPC.ai[1] > 50)
            {
                NPC.velocity = new Vector2(-NPC.velocity.X, NPC.velocity.Y);
            }
        }
    }
    public class ClickerfinBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<ClickerDamage>() += 0.25f;
        }
    }
    public class ClickerfinNormalNPC : ModNPC
    {
        public override string Texture => ModContent.GetInstance<ClickerfinNPC>().Texture;
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.HasMod("InfernumMode");
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
            NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
        }
        public override void SetDefaults()
        {
            NPC.npcSlots = 0.1f;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.damage = 0;
            NPC.width = 24;
            NPC.height = 24;
            NPC.lifeMax = 5;
            NPC.aiStyle = -1;
            AIType = -1;
            NPC.value = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.chaseable = false;
            SpawnModBiomes = [ModContent.GetInstance<AbyssLayer1Biome>().Type];
            NPC.waterMovementSpeed = 0f;

        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Mods.CalamityClickers.NPCs.ClickerfinNormalNPC.Bestiary")
            });
        }
        public bool HasCreatedSchool
        {
            get => NPC.ai[0] == 1f;
            set => NPC.ai[0] = value.ToInt();
        }
        public Player NearestPlayer => Main.player[NPC.target];

        public const int MinSchoolSize = 2;

        public const int MaxSchoolSize = 6;
        public override void AI()
        {
            NPC.noGravity = true;
            NPC.TargetClosest();

            // Emit light.
            Lighting.AddLight(NPC.Center, Color.White.ToVector3());

            // Choose a direction.
            NPC.spriteDirection = (NPC.velocity.X > 0f).ToDirectionInt();

            // Create an initial school of fish if in water.
            // Fish spawned by this cannot create more fish.
            if (Main.netMode != NetmodeID.MultiplayerClient && !HasCreatedSchool && NPC.wet)
            {
                // Larger schools are made rarer by this exponent by effectively "squashing" randomness.
                float fishInterpolant = MathF.Pow(Main.rand.NextFloat(), 4f);
                int fishCount = (int)Utils.Lerp(MinSchoolSize, MaxSchoolSize, fishInterpolant);

                for (int i = 0; i < fishCount; i++)
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPC.type, NPC.whoAmI, 1f);

                HasCreatedSchool = true;
                NPC.netUpdate = true;
                return;
            }

            if (NPC.downedBoss3)
                NPC.catchItem = (short)ModContent.ItemType<Clickerfin>();

            // Sit helplessly if not in water.
            if (!NPC.wet)
            {
                if (Math.Abs(NPC.velocity.Y) < 0.45f)
                {
                    NPC.velocity.X *= 0.95f;
                    NPC.rotation = NPC.rotation.AngleLerp(0f, 0.15f).AngleTowards(0f, 0.15f);
                }
                NPC.noGravity = false;
                return;
            }

            Vector2 ahead = NPC.Center + NPC.velocity * 40f;
            bool aboutToLeaveWorld = ahead.X >= Main.maxTilesX * 16f - 700f || ahead.X < 700f;
            bool shouldTurnAround = aboutToLeaveWorld;
            for (float x = -0.47f; x < 0.47f; x += 0.06f)
            {
                Vector2 checkDirection = NPC.velocity.SafeNormalize(Vector2.Zero).RotatedBy(x);
                if (!Collision.CanHit(NPC.Center, 1, 1, NPC.Center + checkDirection * 125f, 1, 1) ||
                    !Collision.WetCollision(NPC.Center + checkDirection * 50f, NPC.width, NPC.height))
                {
                    shouldTurnAround = true;
                    break;
                }
            }

            // Avoid walls and exiting water.
            if (shouldTurnAround)
            {
                float distanceToTileOnLeft = CalamityClickersUtils.DistanceToTileCollisionHit(NPC.Center, NPC.velocity.RotatedBy(-MathHelper.PiOver2)) ?? 999f;
                float distanceToTileOnRight = CalamityClickersUtils.DistanceToTileCollisionHit(NPC.Center, NPC.velocity.RotatedBy(MathHelper.PiOver2)) ?? 999f;
                float turnDirection = distanceToTileOnLeft > distanceToTileOnRight ? -1f : 1f;
                Vector2 idealVelocity = NPC.velocity.RotatedBy(MathHelper.PiOver2 * turnDirection);
                if (aboutToLeaveWorld)
                    idealVelocity = ahead.X >= Main.maxTilesX * 16f - 700f ? -Vector2.UnitX * 4f : Vector2.UnitX * 4f;

                NPC.velocity = NPC.velocity.MoveTowards(idealVelocity, 0.15f);
                NPC.velocity = Vector2.Lerp(NPC.velocity, idealVelocity, 0.15f);
            }
            else
                DoSchoolingMovement();

            // Move in some random direction if stuck.
            if (NPC.velocity == Vector2.Zero)
            {
                NPC.velocity = Main.rand.NextVector2CircularEdge(4f, 4f);
                NPC.netUpdate = true;
            }

            // Clamp velocities.
            NPC.velocity = NPC.velocity.ClampMagnitude(1.6f, 7f);
            if (NPC.velocity.Length() < 3.45f)
                NPC.velocity *= 1.024f;

            // Define rotation.
            NPC.rotation = NPC.velocity.ToRotation();
            if (NPC.spriteDirection == -1)
                NPC.rotation += MathHelper.Pi;
        }

        // Does schooling movement in conjunction with other sea minnows.
        // This is largely based on the boids algorithm.
        public void DoSchoolingMovement()
        {
            List<NPC> otherFish = Main.npc.Take(Main.maxNPCs).Where(n =>
            {
                bool nearbyAndInRange = n.WithinRange(NPC.Center, 1350f) && Collision.CanHitLine(NPC.Center, 1, 1, n.Center, 1, 1);
                return n.type == NPC.type && n.whoAmI != NPC.whoAmI && nearbyAndInRange;
            }).ToList();

            // Get the center of the flock position and move towards it.
            List<NPC> flockNeighbors = otherFish.Where(n => n.WithinRange(NPC.Center, 300f)).ToList();
            Vector2 centerOfFlock;
            if (flockNeighbors.Count > 0)
            {
                centerOfFlock = Vector2.Zero;
                foreach (NPC neighbor in flockNeighbors)
                    centerOfFlock += neighbor.Center;
                centerOfFlock /= flockNeighbors.Count;
            }
            else
                centerOfFlock = NPC.Center;

            float clockCenterMoveInterpolant = Utils.GetLerpValue(0f, 40f, NPC.Distance(centerOfFlock), true);
            NPC.velocity += NPC.SafeDirectionTo(centerOfFlock, -Vector2.UnitY) * clockCenterMoveInterpolant * 0.1f;

            // Align with other fish.
            List<NPC> alignmentNeighbors = otherFish.Where(n => n.WithinRange(NPC.Center, 300f)).ToList();
            Vector2 flockDirection;
            if (flockNeighbors.Count > 0)
            {
                flockDirection = Vector2.Zero;
                foreach (NPC neighbor in flockNeighbors)
                    flockDirection += neighbor.velocity;
                flockDirection /= flockNeighbors.Count;
            }
            else
                flockDirection = NPC.velocity.RotatedBy(MathHelper.Pi * 0.012f);

            // Angle towards the flock's current direction.
            NPC.velocity = NPC.velocity.ToRotation().AngleLerp(flockDirection.ToRotation(), 0.04f).ToRotationVector2() * NPC.velocity.Length();

            // Avoid close fish.
            List<NPC> closeNeighbors = otherFish.Where(n => n.WithinRange(NPC.Center, 100f)).ToList();
            if (flockNeighbors.Count > 0)
            {
                Vector2 avoidVelocity = Vector2.Zero;
                foreach (NPC neighbor in flockNeighbors)
                {
                    float avoidFactor = Utils.GetLerpValue(150f, 0f, NPC.Distance(neighbor.Center), true);
                    avoidVelocity -= NPC.SafeDirectionTo(neighbor.Center, Vector2.UnitX) * avoidFactor * 0.74f;
                }
                avoidVelocity /= flockNeighbors.Count;
                NPC.velocity += avoidVelocity;
            }

            // Avoid the closest player.
            if (NearestPlayer.active && !NearestPlayer.dead)
            {
                float playerAvoidanceInterpolant = Utils.GetLerpValue(250f, 100f, NPC.Distance(NearestPlayer.Center), true);
                NPC.velocity += NPC.SafeDirectionTo(NearestPlayer.Center) * playerAvoidanceInterpolant * -0.85f;
            }

            // Swim around idly.
            NPC.velocity = NPC.velocity.RotatedBy(MathHelper.Pi * (NPC.whoAmI % 2f == 0f).ToDirectionInt() * 0.004f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            var calPlayer = spawnInfo.Player.Calamity();
            bool inFirst3Layers = calPlayer.ZoneAbyssLayer1;
            if (inFirst3Layers && spawnInfo.Water)
                return SpawnCondition.CaveJellyfish.Chance * 0.5f;
            return 0f;
        }
    }
}
