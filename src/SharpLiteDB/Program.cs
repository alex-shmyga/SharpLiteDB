using SharpLiteDB.Factories;
using SharpLiteDB.Storage;

namespace SharpLiteDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStorage storage = new DiskStorage();
            Console.WriteLine($"Welcome to {Constants.DbEngineName}! Type '{Constants.ExitCommand}' to quit.");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input == null || input.Trim().ToLower() == Constants.ExitCommand)
                {
                    Console.WriteLine($"Exiting {Constants.DbEngineName}. Goodbye!");
                    break;
                }

                ProcessCommand(input, storage);
            }
        }

        private static void ProcessCommand(string input, IStorage storage)
        {
            var command = CommandFactory.CreateCommand(input, storage);
            command?.Execute();
        }
    }

    public static class Constants
    {
        public const string DbEngineName = "SharpLiteDB";
        public const string ExitCommand = ".quit";
    }
}
