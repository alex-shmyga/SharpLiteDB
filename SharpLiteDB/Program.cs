using SharpLiteDB.Storage;

namespace SharpLiteDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storage = new InMemoryStorage();

            // Create a new table
            storage.CreateTable("users");

            // Insert rows
            storage.InsertRow("users", new Dictionary<string, object> { { "id", 1 }, { "name", "Alice" } });
            storage.InsertRow("users", new Dictionary<string, object> { { "id", 2 }, { "name", "Bob" } });

            // Select all rows
            var rows = storage.SelectAll("users");
            foreach (var row in rows)
            {
                Console.WriteLine($"ID: {row.Data["id"]}, Name: {row.Data["name"]}");
            }

            Console.ReadKey();
        }
    }
}
