using GorillaLocomotion;
using MonkeStatistics.API;
using UnityEngine;

namespace GravityEditor.Core.Pages
{
    [DisplayInMainMenu("Gravity Editor")]
    internal class MainPage : Page
    {
        public override void OnPageOpen()
        {
            base.OnPageOpen();
            SetTitle("Gravity Editor");
            SetAuthor("By Crafterbot");
            SetBackButtonOverride(typeof(MonkeStatistics.Core.Pages.MainPage));
            if (!Main.RoomValid)
            {
                AddLine(2);
                AddLine("You must be");
                AddLine("in a modded");
                AddLine("lobby to use");
                AddLine("This plugin");
                SetLines();
                return;
            }
            // In Modded room 
            Build();
            SetLines();
        }

        private void Build()
        {
            AddLine("[Enabled]", new ButtonInfo(OnEnablePress, 0, ButtonInfo.ButtonType.Toggle, Player.Instance.GetComponent<Rigidbody>().useGravity));
            AddLine(1);
            AddLine("Use Custom?", new ButtonInfo(OnUseCustomPress, 0, ButtonInfo.ButtonType.Toggle, Main.UseCustom));
            AddLine(1);
            AddLine("Gravity:" + Main.Gravity);
            AddLine("Gravity[+]", new ButtonInfo(OnGravityChange, 1, ButtonInfo.ButtonType.Press));
            AddLine("Gravity[-]", new ButtonInfo(OnGravityChange, -1, ButtonInfo.ButtonType.Press));
            AddLine(1);
            AddLine("Credits", new ButtonInfo((object Sender, object[] Args) => ShowPage(typeof(Pages.CreditsPage)), 0));
        }

        // Event handlers

        public void OnEnablePress(object Sender, object[] Args)
        {
            Main.Enabled = !Main.Enabled;
        }
        public void OnUseCustomPress(object Sender, object[] Args)
        {
            Main.UseCustom = !Main.UseCustom;
        }
        public void OnGravityChange(object Sender, object[] Args)
        {
            Main.Gravity += (int)Args[0];

            // This will not change any buttons, only the text
            TextLines = new Line[0];
            Build();
            UpdateLines();
        }
    }
}

