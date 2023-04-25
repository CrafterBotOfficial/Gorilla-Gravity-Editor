using BepInEx;
using GorillaLocomotion;
using HarmonyLib;
using MonkeStatistics.API;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace GravityEditor
{
    [BepInPlugin(GUID, Name, Version)]
    [BepInDependency("Crafterbot.MonkeStatistics")]

    [Description("HauntedModMenu")]
    [ModdedGamemode]
    internal class Main : BaseUnityPlugin
    {
        public const string
            GUID = "crafterbot.gravityeditor",
            Name = "GravityEditor",
            Version = "1.0.1";
        public static bool RoomValid;
        public static bool Enabled;
        public static bool UseCustom;
        public static float Gravity = 1;

        private Rigidbody Rb;

        private void Awake()
        {
            Logger.LogInfo("Init : " + Name);
            Registry.AddAssembly();
            new Harmony(GUID).PatchAll(Assembly.GetExecutingAssembly());
        }

        #region Enable/Disable
        public void OnEnable() =>
            Enabled = true;
        public void OnDisable()
        {
            Player.Instance.GetComponent<Rigidbody>().useGravity = true;
            Enabled = false;
            UseCustom = false;
        }
        [ModdedGamemodeJoin]
        private void OnJoined() =>
            RoomValid = true;
        [ModdedGamemodeLeave]
        private void OnLeft()
        {
            Player.Instance.GetComponent<Rigidbody>().useGravity = true;
            RoomValid = false;
            UseCustom = false;
        }
        #endregion
    }
}
