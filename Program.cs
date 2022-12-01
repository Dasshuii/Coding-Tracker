using System;
using System.Configuration;

namespace coding_tracker
{
    class Program
    {
        static void Main()
        {
            GetUserInput getUserInput = new();

            getUserInput.MainMenu();
        }
    }
}