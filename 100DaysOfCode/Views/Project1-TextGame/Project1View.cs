using System.Reactive.Disposables;
using ReactiveUI;
using Terminal.Gui;
using _100DaysOfCode.ViewModels;
using NStack;
using ReactiveMarbles.ObservableEvents;

namespace _100DaysOfCode.Views
{
	public class Project1View : Window, IViewFor<Project1ViewModel>
	{
		readonly CompositeDisposable _disposable = new CompositeDisposable();

		public Project1View(Project1ViewModel viewModel) : base("Dinomitron #100DaysOfCode Project 1 - Text Game")
		{
			ViewModel = viewModel;
			var gameQuestion = GameQuestion("What is your name?", null);
			var gameInput = GameInput(gameQuestion);
		}

		public Project1ViewModel ViewModel { get; set; }

		Label GameQuestion(string questionName, View previous)
		{
			var gameQuestion = new Label(ustring.Make(questionName))
			{
				X = previous != null ? Pos.Left(previous) : 0,
				Y = (previous != null ? Pos.Top(previous) : 0) + 1,
				Width = 40
			};

			Add(gameQuestion);
			return gameQuestion;
		}

		TextField GameInput(View previous)
		{
			var gameInput = new TextField()
			{
				X = previous != null ? Pos.Left(previous) : 0,
				Y = (previous != null ? Pos.Top(previous) : 0) + 1,
				Width = 40
			};

			Add(gameInput);
			return gameInput;
		}

		protected override void Dispose(bool disposing)
		{
			_disposable.Dispose();
			base.Dispose(disposing);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (Project1ViewModel)value;
		}
	}
}