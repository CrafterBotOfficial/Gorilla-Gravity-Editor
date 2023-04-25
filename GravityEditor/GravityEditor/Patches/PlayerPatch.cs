using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;

namespace GravityEditor.Patches
{
    [HarmonyPatch(typeof(Player), "LateUpdate", MethodType.Normal)]
    internal class PlayerPatch
    {
        private static Rigidbody Rb;
        [HarmonyPrefix]
        private static void Patch()
        {
            if (!Main.RoomValid)
                return;
            if (Rb == null)
                Rb = Player.Instance.GetComponent<Rigidbody>();
            else
                Rb.useGravity = Main.Enabled;

            if (Main.UseCustom)
                Rb.AddForce(Vector3.down * (Main.Gravity * -100));
        }
    }
}
