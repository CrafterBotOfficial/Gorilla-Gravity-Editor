using ComputerInterface.Interfaces;
using Gravity_Editor.UI.Views;
using System;
using Zenject;

namespace Gravity_Editor.UI
{
    public class Entry : IComputerModEntry
    {
        public string EntryName => ModInfo.ModName;
        public Type EntryViewType => typeof(MainView);
    }
    public class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IComputerModEntry>().To<Entry>().AsSingle();
        }
    }
}
