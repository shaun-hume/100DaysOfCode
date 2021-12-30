using System.Reactive.Disposables;
using ReactiveUI;
using Terminal.Gui;
using _100DaysOfCode.ViewModels;
using NStack;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;

namespace _100DaysOfCode.Views
{
	public class Project1View : Window, IViewFor<Project1ViewModel>
	{
		readonly CompositeDisposable _disposable = new CompositeDisposable();
		private Label gameQuestion;
		private TextField gameInput;

		public Project1View(Project1ViewModel viewModel) : base("Dinomitron #100DaysOfCode Project 1 - Text Game")
		{
			ViewModel = viewModel;
			gameQuestion = GameQuestion("What is your name?");
			gameInput = GameInput();

	}

		public Project1ViewModel ViewModel { get; set; }

		Label GameQuestion(string questionName)
		{
			gameQuestion = new Label(ustring.Make(questionName))
			{
				X = 0,
				Y = 1,
				Width = 40
			};

			ViewModel
				.BindTo(gameQuestion, x => x.Text)

			Add(gameQuestion);
			return gameQuestion;
		}

		TextField GameInput()
		{
				var gameInput = new TextField(ViewModel.GameInput)
				{
					X = Pos.Left(gameQuestion),
					Y = Pos.Top(gameQuestion) + 1,
					Width = 40
				};

				ViewModel
					.WhenAnyValue(x => x.GameInput)
					.BindTo(gameInput, x => x.Text)
					.DisposeWith(_disposable);
				gameInput
					.Events()
					.TextChanged
					.Select(old => gameInput.Text)
					.DistinctUntilChanged()
					.BindTo(ViewModel, x => x.GameInput)
					.DisposeWith(_disposable);

				gameInput.KeyDown += (e) => KeyDownPressUp(e.KeyEvent, "Enter");

				Add(gameInput);
				return gameInput;			
		}

		private void KeyDownPressUp(KeyEvent keyEvent, string v)
		{
			if (keyEvent.Key == Key.Enter)
            {
				Remove(gameQuestion);
				gameQuestion = new Label(ustring.Make("hello"))
				{
					X = 0,
					Y = 1,
					Width = 40
				};
				Add(gameQuestion);
			}
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