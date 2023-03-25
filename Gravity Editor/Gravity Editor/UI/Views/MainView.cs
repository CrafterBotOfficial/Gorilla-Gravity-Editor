using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Text;

namespace Gravity_Editor.UI.Views
{
    internal class MainView : ComputerView
    {
        public static MainView Instance;

        private UISelectionHandler selectionHandler;

        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            Instance = this;

            selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            selectionHandler.CurrentSelectionIndex = SettingsManager.Settings.SelectedIndex;
            selectionHandler.MaxIdx = 3;
            selectionHandler.ConfigureSelectionIndicator("<color=#ed6540>></color> ", "", "  ", "");

            selectionHandler.OnSelected += SelectionHandler_OnSelected;

            Build();
        }

        private void SelectionHandler_OnSelected(int obj)
        {
            switch (obj)
            {
                case 0:
                    SettingsManager.Settings.GravityDisabled = !SettingsManager.Settings.GravityDisabled;
                    SettingsManager.Save();
                    break;
                case 1:
                    Main.UseGravityExtraForce = !Main.UseGravityExtraForce;
                    break;
                // 2 is handled OnKeyPress
                case 3:
                    SettingsManager.Reset();
                    SettingsManager.Load();
                    Build();
                    break;
            }
        }

        // Green: #09ff00
        // Red: #ff0800
        public void Build()
        {
            // Text
            string DisableGravity_Text = !SettingsManager.Settings.GravityDisabled ? "Gravity : <color=#ff0800>[Disabled]</color>" : "Gravity : <color=#09ff00>[Enabled]</color>";
            string Enabled_Text = Main.UseGravityExtraForce ? "Gravity Force : <color=#09ff00>[Enabled]</color>" : "Gravity Force : <color=#ff0800>[Disabled]</color>";
            string GravityForce_Text = $"Gravity Force : {SettingsManager.Settings.Mass}";

            string Warning_Text = Main.ModAllowed ? "" : "<color=#ff0800>WARNING: This mod is not allowed on this server!</color>";

            StringBuilder stringBuilder = Universal.Header(SCREEN_WIDTH, "Gravity Editor", "By Crafterbot", 2);

            // Options
            stringBuilder
                .AppendLine(selectionHandler.GetIndicatedText(0, DisableGravity_Text))
                .AppendLine(selectionHandler.GetIndicatedText(1, Enabled_Text))
                .AppendLine(selectionHandler.GetIndicatedText(2, GravityForce_Text))
                .AppendLine(selectionHandler.GetIndicatedText(3, "Reset"))

            .AppendLines(0)
            .AppendSize(Warning_Text, 75);

            SetText(stringBuilder);
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            base.OnKeyPressed(key);

            if (selectionHandler.HandleKeypress(key))
            {
                Build();
                return;
            }

            switch (key)
            {
                // Arrow button
                case EKeyboardKey.Left:
                    if (selectionHandler.CurrentSelectionIndex == 2)
                    {
                        SettingsManager.Settings.Mass -= 0.5f;
                        SettingsManager.Save();
                        Build();
                    }
                    break;
                case EKeyboardKey.Right:
                    if (selectionHandler.CurrentSelectionIndex == 2)
                    {
                        SettingsManager.Settings.Mass += 0.5f;
                        SettingsManager.Save();
                        Build();
                    }
                    break;

                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
