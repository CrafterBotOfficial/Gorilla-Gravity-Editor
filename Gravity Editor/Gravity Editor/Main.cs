/*
    DO not forget hauntedmodmenu support 
*/

using BepInEx;
using Bepinject;
using GorillaLocomotion;
using Gravity_Editor.UI.Views;
using System.ComponentModel;
using UnityEngine;
using Utilla;

namespace Gravity_Editor
{
    [BepInPlugin(ModInfo.ModGUILD, ModInfo.ModName, ModInfo.ModVersion)]
    [BepInDependency("tonimacaroni.computerinterface")]
    [BepInDependency("dev.auros.bepinex.bepinject")]

    [ModdedGamemode]
    public class Main : BaseUnityPlugin
    {
        public static bool ModAllowed;
        public static bool UseGravityExtraForce;

        private void Awake()
        {
            SettingsManager.Load();
            Utilla.Events.GameInitialized += Events_GameInitialized;

            Zenjector.Install<UI.MainInstaller>().OnProject();
        }

        private void Events_GameInitialized(object sender, System.EventArgs e)
        {
            player = GorillaLocomotion.Player.Instance;
            rb = player.gameObject.GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Gravity handler
        /// </summary>
        private Player player;
        private Rigidbody rb;

        private bool _ModActive; // Last frame

        private void LateUpdate()
        {
            if (!ModAllowed)
            {
                if (_ModActive)
                {
                    rb.useGravity = true;
                    _ModActive = false;
                }
                return;
            }

            bool DontGravity = SettingsManager.Settings.GravityDisabled;
            if (DontGravity != rb.useGravity) rb.useGravity = DontGravity;

            if (UseGravityExtraForce) rb.AddForce(Vector3.down * (SettingsManager.Settings.Mass*100));

            _ModActive = true;
        }

        /// <summary>
        /// Modded Gamemode
        /// </summary>
        [ModdedGamemodeJoin]
        private void Joined() => ModAllowed = true;
        [ModdedGamemodeLeave]
        private void Left() => ModAllowed = false;
    }

    /// <summary>
    /// This is the first release of the Gravity Editor mod.
    /// </summary>

    internal class ModInfo
    {
        public const string ModGUILD = "crafterbot.gorillatag.gravityeditor";
        public const string ModName = "Gravity Editor";
        public const string ModVersion = "1.0.0";
    }
}
