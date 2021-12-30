using System.Reactive.Disposables;
using ReactiveUI;
using Terminal.Gui;
using _100DaysOfCode.ViewModels;
using NStack;
using ReactiveMarbles.ObservableEvents;

namespace _100DaysOfCode.Views
{
    public class MainMenuView : Window, IViewFor<MainMenuViewModel>
	{
		readonly CompositeDisposable _disposable = new CompositeDisposable();

		public MainMenuView(MainMenuViewModel viewModel) : base("Dinomitron #100DaysOfCode")
		{
			ViewModel = viewModel;
			var project1Button = ProjectButton("Project 1: Text Adventure Game", null);
			var project2Button = ProjectButton("Project 2: Tech Interview Problem", project1Button);
		}

		public MainMenuViewModel ViewModel { get; set; }

		protected override void Dispose(bool disposing)
		{
			_disposable.Dispose();
			base.Dispose(disposing);
		}

		Label TitleLabel()
		{
			var label = new Label("Dinomitron #100DaysOfCode");
			Add(label);
			return label;
		}

		Button ProjectButton(string projectName, View previous)
		{
			var projectButton = new Button(ustring.Make(projectName))
			{
				X = previous != null ? Pos.Left(previous) : 0,
				Y = (previous != null ? Pos.Top(previous) : 0) + 1,
				Width = 40
			};

			return getRelevantFunction(projectButton);			
		}

        private Button getRelevantFunction(Button projectButton)
        {
			if (projectButton.Text == "Project 1: Text Adventure Game")
			{
				projectButton
					.Events()
					.Clicked
					.InvokeCommand(ViewModel, x => x.Project1Start)
					.DisposeWith(_disposable);
			}

			if (projectButton.Text == "Project 2: Tech Interview Problem")
			{
				projectButton
					.Events()
					.Clicked
					.InvokeCommand(ViewModel, x => x.Project2Start)
					.DisposeWith(_disposable);
			}
			
			Add(projectButton);
			return projectButton;
		}

        object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (MainMenuViewModel)value;
		}
	}
}