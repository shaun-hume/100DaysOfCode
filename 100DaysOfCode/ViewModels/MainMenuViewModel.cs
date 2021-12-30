﻿using System.Reactive;
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
				Application.Init();

				var ntop = Application.Top;

				var text = new TextView() { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

				var win = new Window("Untitled")
				{
					X = 0,
					Y = 1,
					Width = Dim.Fill(),
					Height = Dim.Fill()
				};
				ntop.Add(win);
				Application.Run(new Project1View(new Project1ViewModel()));
            }
            );        
        }

        [IgnoreDataMember]
		public ReactiveCommand<Unit, Unit> Project1Start { get; }
		public ReactiveCommand<Unit, Unit> Project2Start { get; }
	}
}