using DunGen;
using DunGen.Graph;
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerScanNode
{
    internal class Utils
    {
        internal enum NodeType
        {
            /// <summary>
            /// The blue background scan node used to mark the ship and main entrance
            /// </summary>
            GENERAL = 0,
            /// <summary>
            /// The red background scan node used to mark map hazards and enemies
            /// </summary>
            DANGER = 1,
            /// <summary>
            /// The green background scan node used to mark scrap items
            /// </summary>
            SCRAP = 2,
        }

        static GameObject CreateCanvasScanNode(ref GameObject objectToAddScanNode)
        {
            GameObject scanNodeObject = UnityEngine.Object.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), objectToAddScanNode.transform.position, Quaternion.Euler(Vector3.zero), objectToAddScanNode.transform);
            scanNodeObject.name = "ScanNode";
            scanNodeObject.layer = LayerMask.NameToLayer("ScanNode");
            UnityEngine.Object.Destroy(scanNodeObject.GetComponent<MeshRenderer>());
            UnityEngine.Object.Destroy(scanNodeObject.GetComponent<MeshFilter>());
            return scanNodeObject;
        }

        public static void ChangeScanNode(ref ScanNodeProperties scanNodeProperties, NodeType nodeType, string header = "KROES Scan Node", string subText = "SUBTEXT", int creatureScanID = -1, int scrapValue = 0, int minRange = 2, int maxRange = 7)
        {
            scanNodeProperties.headerText = header;
            scanNodeProperties.subText = subText;
            scanNodeProperties.nodeType = (int)nodeType;
            scanNodeProperties.creatureScanID = creatureScanID;
            scanNodeProperties.scrapValue = scrapValue;
            scanNodeProperties.minRange = minRange;
            scanNodeProperties.maxRange = maxRange;
        }

        public static void AddScanNode(GameObject objectToAddScanNode, NodeType nodeType, string header = "KROES Scan Node", string subText = "SUBTEXT", int creatureScanID = -1, int minRange = 2, int maxRange = 7)
        {
            if (objectToAddScanNode.GetComponentInChildren<ScanNodeProperties>() != null) return;
            GameObject scanNodeObject = CreateCanvasScanNode(ref objectToAddScanNode);
            ScanNodeProperties scanNodeProperties = scanNodeObject.AddComponent<ScanNodeProperties>();
            ChangeScanNode(scanNodeProperties: ref scanNodeProperties, nodeType: nodeType, header: header, subText: subText, creatureScanID: creatureScanID, minRange: minRange, maxRange: maxRange);
        }

        public static void AddGeneralScanNode(GameObject objectToAddScanNode, string header = "KROES Scan Node", string subText = "SUBTEXT", int creatureScanID = -1, int minRange = 2, int maxRange = 7)
        {
            AddScanNode(objectToAddScanNode: objectToAddScanNode, nodeType: NodeType.GENERAL, header: header, subText: subText, creatureScanID: creatureScanID, minRange: minRange, maxRange: maxRange);
        }

        public static void RemoveScanNode(GameObject objectToRemoveScanNode)
        {
            UnityEngine.Object.Destroy(objectToRemoveScanNode.GetComponentInChildren<ScanNodeProperties>());
        }

        public static void AddScannerNodeToAllPlayers(ref StartOfRound startOfRound)
        {
            PlayerControllerB[] playerScripts = startOfRound.allPlayerScripts;

            foreach (PlayerControllerB player in playerScripts)
            {
                if (player == null) continue;
                if (player.gameObject == null) continue;
                ScanPlugin.Log.LogInfo($"Adding Scan Node for player: {player.playerUsername}");
                AddGeneralScanNode(objectToAddScanNode: player.gameObject, header: "Player", subText: player.playerUsername, minRange: 3);
            }
        }
    }
}
