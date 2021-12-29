using System.Reactive;
using System.Runtime.Serialization;
using ReactiveUI;
using Terminal.Gui;
using _100DaysOfCode.Views;

namespace _100DaysOfCode.ViewModels
{
    [DataContract]
	public class MainMenuViewModel : ReactiveObject
    {
        public MainMenuViewModel()
        {
            Project1Start = ReactiveCommand.Create(() => {
                Console.Clear();
                Application.Init();
                Application.Run(new Project1View(new Project1ViewModel()));
            }
            );        
        }

        [IgnoreDataMember]
		public ReactiveCommand<Unit, Unit> Project1Start { get; }
		public ReactiveCommand<Unit, Unit> Project2Start { get; }
	}
}