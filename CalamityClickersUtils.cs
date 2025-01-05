using ClickerClass;
using ClickerClass.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public static class CalamityClickersUtils
    {
        public static ClickerPlayer Clicker(this Player player) => player.GetModPlayer<ClickerPlayer>();
        public static CalamityClickersPlayer CalClicker(this Player player) => player.GetModPlayer<CalamityClickersPlayer>();
        public static CalamityClickersGlobalNPC CalClicker(this NPC npc) => npc.GetGlobalNPC<CalamityClickersGlobalNPC>();
        public static Player Owner(this Projectile proj) => Main.player[proj.owner];
        /*public static string RegisterClickEffect(Mod mod, string internalName, int amount, Color color, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, bool postWildMagic = false, object[] nameArgs = null, object[] descriptionArgs = null)
        {
            string clickerEffect = ClickerCompat.RegisterClickEffect(mod, internalName, amount, () => color, action, preHardMode, nameArgs, descriptionArgs);
            if (postWildMagic)
                CalamityClickers.extraAPI.Call("RegisterPostWildMagicClickerEffect", clickerEffect);
            return clickerEffect;
        }
        public static string RegisterClickEffect(Mod mod, string internalName, int amount, Func<Color> color, Action<Player, EntitySource_ItemUse_WithAmmo, Vector2, int, int, float> action, bool preHardMode = false, bool postWildMagic = false, object[] nameArgs = null, object[] descriptionArgs = null)
        {
            string clickerEffect = ClickerCompat.RegisterClickEffect(mod, internalName, amount, color, action, preHardMode, nameArgs, descriptionArgs);
            if (postWildMagic)
                CalamityClickers.extraAPI.Call("RegisterPostWildMagicClickerEffect", clickerEffect);
            return clickerEffect;
        }*/
        public static List<string> GetHeldClickerEffects(this Player Player) => Player.HeldItem.GetGlobalItem<ClickerItemCore>().itemClickEffects;
        public static void RegisterPostWildMagicClickEffect(string clickEffectName)
        {
            if (!(CalamityClickers.extraAPI.Call("GetPostWildMagicClickerEffectList") as List<string>).Contains(clickEffectName))
                CalamityClickers.extraAPI.Call("RegisterPostWildMagicClickerEffect", clickEffectName);
        }
        public static void RegisterPostNightmareMagicClickEffect(string clickEffectName)
        {
            if (!CalamityClickersSystem.PostNightmareClickerEffects.Contains(clickEffectName))
                CalamityClickersSystem.PostNightmareClickerEffects.Add(clickEffectName);
        }
        public static void RegisterBlacklistedClickEffect(string clickEffectName)
        {
            if (!(CalamityClickers.extraAPI.Call("GetBlacklistedRandomClickerEffectList") as List<string>).Contains(clickEffectName))
                CalamityClickers.extraAPI.Call("RegisterBlacklistedRandomClickerEffect", clickEffectName);
        }
        public static Color GetColorFromHex(string hexValue)
        {
            var cc1 = System.Drawing.ColorTranslator.FromHtml("#" + hexValue.Replace("#", ""));
            return new Color(cc1.R, cc1.G, cc1.B);
        }
        /// <summary>
		/// Calculates simple collision with NPCs, and manages two sets of collections pertaining to them so that damage related code runs properly.
		/// <para>Requires persistent collections for hitTargets and foundTargets, and the method to be called in any AI hook only once (per tick).</para>
		/// <para>This method only works on several assumptions:</para>
		/// <para>* local immunity is used, so that the first contact is a guaranteed hit</para>
		/// <para>* it uses generic "damage through contact" code, and not special laser code (last prism)</para>
		/// </summary>
		/// <param name="projectile">The projectile</param>
		/// <param name="hitTargets">The collection of NPC indexes it has already hit and should be excluded</param>
		/// <param name="foundTargets">The collection of NPC indexes from the last method call to then add to hitTargets</param>
		/// <param name="max">The amount of NPCs it can hit before killing itself</param>
		/// <param name="condition">Custom condition to check for the NPC before setting it as a suitable target. If null, counts as true.</param>
		/// <returns>True if target count is atleast max (which kills itself)</returns>
		public static bool HandleChaining(this Projectile projectile, ICollection<int> hitTargets, ICollection<int> foundTargets, int max, Func<NPC, bool> condition = null)
        {
            //Applies foundTargets from the last call to hitTargets
            foreach (var f in foundTargets)
            {
                if (!hitTargets.Contains(f))
                {
                    //If check can be removed here, but left in in case of debugging/additional method
                    hitTargets.Add(f);
                }
            }
            foundTargets.Clear();

            //Seek suitable targets the projectile collides with
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (!npc.active || npc.dontTakeDamage || projectile.friendly && npc.townNPC) //The only checks that truly prevent damage. No chaseable or immortal, those can still be hit
                {
                    continue;
                }

                //Simple code instead of complicated recreation of vanilla+modded collision code here (which only runs clientside, but this has to be all-side)
                //Hitbox has to be for "next update cycle" because AI (where this should be called) runs before movement updates, which runs before damaging takes place
                Rectangle hitbox = new Rectangle((int)(projectile.position.X + projectile.velocity.X), (int)(projectile.position.Y + projectile.velocity.Y), projectile.width, projectile.height);
                ProjectileLoader.ModifyDamageHitbox(projectile, ref hitbox);

                if (!projectile.Colliding(hitbox, npc.Hitbox)) //Intersecting hitboxes + special checks. Safe to use all-side, lightning aura uses it
                {
                    continue;
                }

                if (condition != null && !condition(npc))
                {
                    //If custom condition returns false
                    continue;
                }

                foundTargets.Add(i);
            }

            if (hitTargets.Count >= max)
            {
                projectile.Kill();
                return true;
            }

            return false;
        }
    }
}
