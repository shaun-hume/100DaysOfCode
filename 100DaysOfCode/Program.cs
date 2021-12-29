using Terminal.Gui;
using _100DaysOfCode.ViewModels;
using _100DaysOfCode.Views;

Application.Init();
//var top = Application.Top;

//// Creates the top-level window to show
//var win = new Window("Dinomitron #100DaysOfCode")
//{
//	X = 0,
//	Y = 1, // Leave one row for the toplevel menu

//	// By using Dim.Fill(), it will automatically resize without manual intervention
//	Width = Dim.Fill(),
//	Height = Dim.Fill()
//};

//top.Add(win);

//var project1Button = new Button(0, 0, "Project 1: Text Adventure Game");
//var project2Button = new Button(0, 1, "Project 2: Tech Interview Problem");

//// Add some controls, 
//win.Add(
//	project1Button,
//	project2Button
//);

//void project1Button_Click(Object sender, EventArgs e)
//{
//	Console.WriteLine("hey");
//}

Application.Run(new MainMenuView(new MainMenuViewModel()));
