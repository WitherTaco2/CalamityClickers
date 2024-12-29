using CalamityClickers.Content.Items.Accessories;
using ClickerClass;
using System;
using Terraria;

namespace CalamityClickers
{
    public static class CalamityClickersModCalls
    {
        public static object Call(params object[] args)
        {
            CalamityClickers calamityClicker = CalamityClickers.mod;
            //Simplify code by resizing args
            Array.Resize(ref args, 25);
            string success = "Success";
            try
            {
                string message = args[0] as string;

                if (message == "RegisterPostMoonlordClickerEffect" || message == "RegisterPostMLClickerEffect")
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
                else if (message == "RegisterBlacklistedClickerEffect")
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
                else if (message == "SetAccessoryItem")
                {
                    var player = args[1] as Player;
                    var accName = args[2] as string;
                    var item = args[3] as Item;
                    if (accName == null)
                    {
                        throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has returned null.");
                    }
                    if (player == null)
                    {
                        throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
                    }
                    if (item == null)
                    {
                        throw new Exception($"Call Error: The item argument for the attempted message, \"{message}\" has returned null.");
                    }

                    CalamityClickersPlayer calClickerPlayer = player.GetModPlayer<CalamityClickersPlayer>();
                    ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

                    if (accName == "BloodyCookies")
                    {
                        calClickerPlayer.accBloodyChocolate = true;
                        calClickerPlayer.accBloodyChocolateItem = item;
                    }
                    if (accName == "BloodyChocCookies")
                    {
                        calClickerPlayer.accBloodyChocolate = true;
                        calClickerPlayer.accBloodyChocolateItem = item;
                        player.GetModPlayer<ClickerPlayer>().EnableClickEffect(BloodyChocCookies.BurnOrBliss);
                    }
                    else if (accName == "FingerOfBloodGod")
                    {
                        calClickerPlayer.accFingerOfBG = true;
                    }
                    else if (accName == "LihzahrdParticleAccelerator")
                    {
                        calClickerPlayer.accPortableParticleAcceleratorUpgrades = item;
                    }
                    else if (accName == "SSMedal")
                    {
                        calClickerPlayer.accSSMedal = item;
                        clickerPlayer.accSMedalItem = item;
                    }

                    throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has no valid entry point.");
                }
                else if (message == "GetAccessory")
                {
                    var player = args[1] as Player;
                    var accName = args[2] as string;
                    if (accName == null)
                    {
                        throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has returned null.");
                    }
                    if (player == null)
                    {
                        throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
                    }

                    CalamityClickersPlayer calClickerPlayer = player.GetModPlayer<CalamityClickersPlayer>();
                    ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

                    if (accName == "BloodyCookies")
                    {
                        return calClickerPlayer.accBloodyChocolate;
                    }
                    /*else if (accName == "BloodyChocolateChip")
                    {
                        //player.GetModPlayer<ClickerPlayer>().EnableClickEffect(BloodyChocCookies.BurnOrBliss);
                    }*/
                    else if (accName == "FingerOfBloodGod")
                    {
                        return calClickerPlayer.accFingerOfBG;
                    }
                    else if (accName == "LihzahrdParticleAccelerator")
                    {
                        return calClickerPlayer.accPortableParticleAcceleratorUpgrades != null && !calClickerPlayer.accPortableParticleAcceleratorUpgrades.IsAir;
                    }
                    else if (accName == "SSMedal")
                    {
                        return calClickerPlayer.accSSMedal != null && !calClickerPlayer.accSSMedal.IsAir;
                    }

                    throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has no valid entry point.");
                }
                else if (message == "GetAccessoryItem")
                {
                    var player = args[1] as Player;
                    var accName = args[2] as string;
                    if (accName == null)
                    {
                        throw new Exception($"Call Error: The accName argument for the attempted message, \"{message}\" has returned null.");
                    }
                    if (player == null)
                    {
                        throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
                    }

                    CalamityClickersPlayer calClickerPlayer = player.GetModPlayer<CalamityClickersPlayer>();
                    //ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

                    if (accName == "LihzahrdParticleAccelerator")
                    {
                        return calClickerPlayer.accPortableParticleAcceleratorUpgrades;
                    }
                    else if (accName == "SSMedal")
                    {
                        return calClickerPlayer.accSSMedal;
                    }

                }
                else if (message == "GetArmorSet")
                {
                    var player = args[1] as Player;
                    var setName = args[2] as string;
                    if (setName == null)
                    {
                        throw new Exception($"Call Error: The setName argument for the attempted message, \"{message}\" has returned null.");
                    }
                    if (player == null)
                    {
                        throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
                    }

                    CalamityClickersPlayer calClickerPlayer = player.GetModPlayer<CalamityClickersPlayer>();

                    if (setName == "Daedalus")
                    {
                        return calClickerPlayer.setDaedalusClicker;
                    }
                    else if (setName == "Hydrothermic")
                    {
                        return calClickerPlayer.setHydrothermicClicker;
                    }
                    else if (setName == "Tarragon")
                    {
                        return calClickerPlayer.setTarragonClicker;
                    }
                    else if (setName == "Intergelactic")
                    {
                        return calClickerPlayer.SetIntergelactic;
                    }
                    else if (setName == "Bloodflare")
                    {
                        return calClickerPlayer.setBloodflareClicker;
                    }
                    else if (setName == "GodSlayer")
                    {
                        return calClickerPlayer.setGodSlayerClicker;
                    }


                    /*
                    else if (setName == "")
                    {
                        return calClickerPlayer.;
                    }
                    */

                    throw new Exception($"Call Error: The setName argument for the attempted message, \"{message}\" has no valid entry point.");
                }
                else if (message == "SetBurnOrBliss")
                {
                    var player = args[1] as Player;
                    if (player == null)
                    {
                        throw new Exception($"Call Error: The player argument for the attempted message, \"{message}\" has returned null.");
                    }

                    player.GetModPlayer<ClickerPlayer>().EnableClickEffect(BloodyChocCookies.BurnOrBliss);
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
