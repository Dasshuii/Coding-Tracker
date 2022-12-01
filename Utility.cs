using System.Text;

namespace coding_tracker
{
    internal class Utility
    {
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;
            string asterics = "";

            StringBuilder input = new StringBuilder();

            while (true)
            {
                if (isPrompt)
                    Console.WriteLine(prompt);
                isPrompt = false;

                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if(inputKey.Key == ConsoleKey.Enter) break;

                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                
                else if (inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterics + "*");
                }

            }
            return input.ToString();
        }
    }
}