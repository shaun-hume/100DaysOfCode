using System.Reactive;
using System.Runtime.Serialization;
using ReactiveUI;
using Terminal.Gui;
using _100DaysOfCode.Views;

namespace _100DaysOfCode.ViewModels
{
    [DataContract]
    public class Project1ViewModel : ReactiveObject
    {
        public Project1ViewModel()
        {
            Project1Start = ReactiveCommand.Create(() => {
                Application.Init();
                Application.Run(new MainMenuView(new MainMenuViewModel()));
            }
            );
        }

        [IgnoreDataMember]
        public ReactiveCommand<Unit, Unit> Project1Start { get; }
        public ReactiveCommand<Unit, Unit> Project2Start { get; }
    }
}