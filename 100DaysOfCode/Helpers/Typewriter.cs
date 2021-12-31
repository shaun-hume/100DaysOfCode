using System;
namespace _100DaysOfCode.Helpers
{
    public static class Typewriter
    {
        public static void Typewrite(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(15);
            }
        }
    }
}