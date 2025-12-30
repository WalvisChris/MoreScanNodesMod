using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerScanNode
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class ScanPlugin : BaseUnityPlugin
    {
        private const string GUID = "com.kroes.playerscannode";
        private const string NAME = "Player Scan Node";
        private const string VERSION = "1.0.0";

        internal static ManualLogSource Log;
        Harmony harmony = new Harmony(GUID);

        private void Awake()
        {
            Log = Logger;
            harmony.PatchAll(typeof(StartOfRoundPatch));
            Log.LogInfo("Loaded successfully!");
        }
    }
}
