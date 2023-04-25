using MonkeStatistics.API;

namespace GravityEditor.Core.Pages
{
    internal class CreditsPage : Page
    {
        public override void OnPageOpen()
        {
            base.OnPageOpen();
            SetTitle("Credits");
            SetBackButtonOverride(typeof(MainPage));

            AddLine(1);
            AddLine("This mod created");
            AddLine("by Crafterbot");

            SetLines();
        }
    }
}
