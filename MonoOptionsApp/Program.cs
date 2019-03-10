using System;
using Mono.Options;

namespace MonoOptionsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // These variables will be set when the command line is parsed
            int number1 = 0;
            bool isNumber1Integer = false;
            int number2 = 0;
            bool isNumber2Integer = false;
            bool showHelp = false;

            // These are the available options, not that they set the variables
            var options = new OptionSet();
            options.Add("n1|number1=", "First number for adding.",
              v => isNumber1Integer = int.TryParse(v, out number1));
            options.Add("n2|number2=", "Second number for adding.",
              v => isNumber2Integer = int.TryParse(v, out number2));
            options.Add("h|help", "Show this message and exit.",
              v => showHelp = v != null);

            // In case of options can not be parsed exeption message will be shown
            try
            {
                options.Parse(args);
            }
            catch(OptionException e)
            {
                Console.Write("MonoOptionsApp: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `MonoOptionsApp --help' for more information.");
                return;
            }

            if (showHelp)
            {
                ShowHelp(options);
                return;
            }

            if (args.Length == 2 && isNumber1Integer && isNumber2Integer)
            {
                int sum = number1 + number2;
                Console.WriteLine("The sum of the provided numbers is: " + sum);
            }
            else
            {
                ShowHelp(options);
            }

            Console.WriteLine("Press any button on your keyboard to exit the application...");
            Console.ReadKey();
        }
        static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: The program adds two integer numbers provided as command line arguments. E.g.:");
            Console.WriteLine(@">C:\AppFolder\MonoOptionsApp.exe --n1=5 --n2=7");
            Console.WriteLine("As a result of the program execution (after pressing Enter button) you will see the sum of the provided numbers in the console output.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
