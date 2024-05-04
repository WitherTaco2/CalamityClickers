using CalamityClickers.Content.Cooldowns;
using CalamityMod.Cooldowns;
using System;
using Terraria.ModLoader;

namespace CalamityClickers
{
    public class CalamityClickers : Mod
    {
        public static CalamityClickers mod;
        public override void Load()
        {
            mod = this;
            CooldownRegistry.Register<GodSlayerOverclockCooldown>(GodSlayerOverclockCooldown.ID);
        }
        public override void Unload()
        {
            mod = null;
        }
        public override object Call(params object[] args)
        {
            CalamityClickers calamityClicker = CalamityClickers.mod;
            //Simplify code by resizing args
            Array.Resize(ref args, 25);
            string success = "Success";
            try
            {
                string message = args[0] as string;

                if (message == "RegisterPostMoonlordClickerEffect")
                {
                    var name = args[1] as string;
                    if (name == null)
                    {
                        throw new Exception($"Call Error: The name argument for the attempted message, \"{message}\" has returned null.");
                    }
                    if (!CalamityClickersSystem.PostMLClickerEffects.Contains(name))
                    {
                        CalamityClickersSystem.PostMLClickerEffects.Add(name);
                    }
                    else
                    {
                        throw new Exception($"Call Error: Post-moonlord Clicker effect \"{name}\" is already registered, \"{message}\" has returned null.");
                    }
                }
                if (message == "RegisterBlacklistedClickerEffect")
                {
                    var name = args[1] as string;
                    if (name == null)
                    {
                        throw new Exception($"Call Error: The name argument for the attempted message, \"{message}\" has returned null.");
                    }
                    if (!CalamityClickersSystem.BlacklistedClickerEffects.Contains(name))
                    {
                        CalamityClickersSystem.BlacklistedClickerEffects.Add(name);
                    }
                    else
                    {
                        throw new Exception($"Call Error: Blacklisted Clicker effect \"{name}\" is already registered, \"{message}\" has returned null.");
                    }
                }
            }
            catch (Exception e)
            {
                calamityClicker.Logger.Error($"Call Error: {e.StackTrace} {e.Message}");
            }
            return null;
        }
    }
}