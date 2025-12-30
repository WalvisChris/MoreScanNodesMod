using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerScanNode
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        static void Postfix(ref StartOfRound __instance)
        {
            Utils.AddScannerNodeToAllPlayers(ref __instance);
        }
    }
}
