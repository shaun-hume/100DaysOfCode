using _100DaysOfCode;
using _100DaysOfCode.Projects;

InitialiseAndAskUserForWhatProjectToRun();

void InitialiseAndAskUserForWhatProjectToRun()
{
    Typewrite("Choose the project to run");
    Typewrite("1. Project 1: Text Adventure Game");
    Typewrite("2. Project 2: Tech Interview Problem");
    Typewrite("3. Project 3: Dependency Injection");

    var option = Console.ReadLine();
    ProcessUserInput(option);
}

void ProcessUserInput(string option)
{
    int numericOption;

    if (int.TryParse(option, out numericOption) == false)
    {
        Console.Clear();
        Typewrite("Invalid option selected. Please just write the number for the option. ");
        InitialiseAndAskUserForWhatProjectToRun();
        return;
    }
    switch (numericOption)
    {
        case 1:
            var project1 = new Project1_TextAdventure();
            break;
        case 2:
            var project2 = new Project2_TechInterviewProblem();
            break;
        case 3:
            var project3 = new Project3_DependencyInjection();
            break;
        default:
            break;
    }
}