using _100DaysOfCode;
using _100DaysOfCode.Projects;

InitialiseAndAskUserForWhatProjectToRun();

void InitialiseAndAskUserForWhatProjectToRun()
{
    Typewrite("Choose the project to run \r\n" +
        "1. Project 1: Text Adventure Game \r\n" +
        "2. Project 2: Tech Interview Problem\r\n" +
        "3. Project 3: Dependency Injection\r\n" +
        "4. Project 4: String Concatenation\r\n");

    var option = Console.ReadLine();
    ProcessUserInput(option);
}

void ProcessUserInput(string option)
{
    int numericOption;

    if (int.TryParse(option, out numericOption) == false)
    {
        Console.Clear();
        Typewrite("Invalid option selected. Please just write the number for the option. \r\n");
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
        case 4:
            var project4 = new Project4_StringConcatenation();
            break;
        default:
            break;
    }
}