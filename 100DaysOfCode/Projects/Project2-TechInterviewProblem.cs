using System;
using System.Numerics;
using _100DaysOfCode.Helpers;
using Combinatorics.Collections;

namespace _100DaysOfCode.Projects
{
    //     There's a staircase with N steps, and you can climb 1 or 2 steps at a time.
    //     Given N, write a function that returns the number of unique ways you can climb the staircase.
    //     The order of the steps matters.

    //For example, if N is 4, then there are 5 unique ways:

    //    1, 1, 1, 1
    //    2, 1, 1
    //    1, 2, 1
    //    1, 1, 2
    //    2, 2

    //What if, instead of being able to climb 1 or 2 steps at a time, you could climb any number from a set of positive integers X?
    //For example, if X = {1, 3, 5}, you could climb 1, 3, or 5 steps at a time. Generalize your function to take in X. 

    public class Project2_TechInterviewProblem
    {
        int numberOfSteps;
        List<int> stairClimbingAbility;

        public Project2_TechInterviewProblem()
        {
            //Ask for number of steps
            numberOfSteps = SetNumberOfSteps();

            //Ask for how many steps the climber can take at a time
            stairClimbingAbility = SetStairClimbingAbility();

            FindCountOfUniqueWaysToClimbStairs(numberOfSteps, stairClimbingAbility);

        }

        private void FindCountOfUniqueWaysToClimbStairs(int numberOfSteps, List<int> stairClimbingAbility)
        {
            var uniqueWaysCount = (ReturnCountOfAllCombinations(numberOfSteps, stairClimbingAbility)).Count;
            Console.Clear();
            Typewrite($"There are {uniqueWaysCount} unique ways to climb the stairs.");
        }

        private List<Array> ReturnCountOfAllCombinations(int numberOfSteps, List<int> stairClimbingAbility)
        {   
            var combinations = new List<Array>();
            var lowestNumberInList = stairClimbingAbility.Min();
            var numberOfMaxSteps = numberOfSteps / lowestNumberInList;
            List<Variations<int>> variationLists = new List<Variations<int>>();

            for (int i = numberOfMaxSteps; i > 0; i--)
            {
                try
                {
                    var variationsTemp = new Variations<int>(stairClimbingAbility, i, GenerateOption.WithRepetition);
                    variationLists.Add(variationsTemp);
                }
                catch (Exception ex)
                {
                    var x = ex;
                }
            }

            foreach (var variationList in variationLists)
            {
                foreach (var variation in variationList)
                {
                    if (variation.Sum(x => x) == numberOfSteps)
                    {                        
                        combinations.Add(variation.ToArray());
                    }
                }
            }
            return combinations;
        }

        private int SetNumberOfSteps()
        {
            Console.Clear();
            Typewrite("How many steps does the staircase have?");
            var userInput = Console.ReadLine();
            var parsedInput = 0;

            if (int.TryParse(userInput, out parsedInput)) return parsedInput;
            return SetNumberOfSteps();
        }

        private List<int> SetStairClimbingAbility()
        {
            Console.Clear();
            Typewrite("How many steps can the person do (write the number separated by a comma below)");
            var userInput = Console.ReadLine();
            var x = userInput.Split(',').Select(Int32.Parse).ToList();
            return x;
        }
    }
}

