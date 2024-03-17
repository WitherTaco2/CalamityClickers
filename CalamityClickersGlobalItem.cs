using CalamityClickers.Content.Items.Weapons.HM;
using CalamityClickers.Content.Items.Weapons.PostML;
using CalamityClickers.Content.Items.Weapons.PreHM;
using CalamityMod;
using CalamityMod.Items.TreasureBags;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickersGlobalItem : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Mod calamity = ModLoader.GetMod("CalamityMod");
            if (ClickerCompat.IsClickerItem(item))
            {
                /*if (player.GetModPlayer<CalamityClickersPlayer>().daedalusClicker && Main.rand.NextBool(5))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int damageA = (int)(damage * 0.5f);
                        Vector2 vec1 = Main.MouseWorld;
                        Vector2 vec2 = new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f));
                        //Projectile.NewProjectile(vec1, vec2, ProjectileID.CrystalShard, damageA, knockback, player.whoAmI);
                        Projectile.NewProjectile(source, Main.MouseWorld, Main.rand.NextVector2Circular(5f, 5f), ProjectileID.CrystalShard, damage / 10, knockback / 2, player.whoAmI);
                    }
                }
                if (player.GetModPlayer<CalamityClickersPlayer>().godSlayerClicker && Main.rand.NextBool(5))
                {
                    int damageA = (int)(damage * 0.15f);
                    for (int i = 0; i < 4; i++)
                    {
                        int numA = 50;
                        Vector2 vec1 = new Vector2(Main.MouseWorld.X + Main.rand.Next(-numA, numA), Main.MouseWorld.Y + Main.rand.Next(-numA, numA));
                        Vector2 vec2 = new Vector2(0f, 0f);
                        Projectile.NewProjectile(vec1, vec2, ModContent.ProjectileType<NebulaStar>(), damageA, knockBack, player.whoAmI);
                    }
                }*/
                return false;
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            foreach (var rule in itemLoot.Get())
            {
                if (rule is CommonDrop normalDrop)
                {
                    if (normalDrop.itemId == ModContent.ItemType<TheClicker>())
                    {
                        itemLoot.Remove(rule);
                    }
                }
            }






            if (item.type == ModContent.ItemType<DesertScourgeBag>())
            {
                itemLoot.Add(ModContent.ItemType<ScourgeClicker>(), 3);
            }
            if (item.type == ModContent.ItemType<CrabulonBag>())
            {
                itemLoot.Add(ModContent.ItemType<MushyClicker>(), 3);
            }
            if (item.type == ModContent.ItemType<HiveMindBag>())
            {
                itemLoot.Add(ModContent.ItemType<RottenClicker>(), 3);
            }

            if (item.type == ModContent.ItemType<CryogenBag>())
            {
                itemLoot.Add(ModContent.ItemType<CryoClicker>(), 3);
            }

            if (item.type == ModContent.ItemType<DragonfollyBag>())
            {
                itemLoot.Add(ModContent.ItemType<RedLightningClicker>(), 3);
            }
        }
    }
}
