using System;
using System.Collections.Generic;
using System.Linq;
using Harmony;

namespace ModLoader.Patches
{
    [HarmonyPatch(typeof(GameManager), "GetVersionString")]
    public class VersionPatch
    {
        static void Postfix(ref string __result)
        {
            __result += "\nModded - ModLoader v1.0.0.0";
        }
    }
}