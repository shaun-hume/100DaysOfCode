using System.Reactive;
using System.Runtime.Serialization;
using ReactiveUI;
using Terminal.Gui;
using _100DaysOfCode.Views;
using NStack;
using ReactiveUI.Fody.Helpers;

namespace _100DaysOfCode.ViewModels
{
    [DataContract]
    public class Project1ViewModel : ReactiveObject
    {            
        public Project1ViewModel()
        {
            ProcessCommand = ReactiveCommand.Create(() => {
                Console.WriteLine(GameInput);
            }
            );
        }

        private void ProcessCommandFromUser()
        {
            throw new NotImplementedException();
        }

        [Reactive, DataMember]
        public ustring GameInput { get; set; } = ustring.Empty;

        [IgnoreDataMember]
        public ReactiveCommand<Unit, Unit> ProcessCommand { get; }

    }
}