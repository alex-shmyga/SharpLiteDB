using SharpLiteDB.Commands;
using SharpLiteDB.Storage;
using System.Text.RegularExpressions;

namespace SharpLiteDB.Factories
{
    // TODO: add unit tests and refactor
    internal static class CommandFactory
    {
        internal static ICommand CreateCommand(string input, InMemoryStorage storage)
        {
            if (input.StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(input, @"CREATE TABLE (\w+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string tableName = match.Groups[1].Value;
                    return new CreateTableCommand(storage, tableName);
                }
                Console.WriteLine("Syntax Error: Expected format 'CREATE TABLE table_name'.");
                return null;
            }
            if (input.StartsWith("DROP TABLE", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(input, @"DROP TABLE (\w+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string tableName = match.Groups[1].Value;
                    return new DropTableCommand(storage, tableName);
                }
                Console.WriteLine("Syntax Error: Expected format 'CREATE TABLE table_name'.");
                return null;
            }
            else if (input.StartsWith("INSERT INTO", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(input, @"INSERT INTO (\w+) \((.*?)\) VALUES \((.*?)\)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string tableName = match.Groups[1].Value;
                    var columns = match.Groups[2].Value.Split(',');
                    var values = match.Groups[3].Value.Split(',');

                    if (columns.Length != values.Length)
                    {
                        Console.WriteLine("Syntax Error: Columns and values count mismatch.");
                        return null;
                    }

                    var rowData = new Dictionary<string, object>();
                    for (int i = 0; i < columns.Length; i++)
                    {
                        rowData[columns[i].Trim()] = values[i].Trim();
                    }

                    return new InsertCommand(storage, tableName, rowData);
                }
                Console.WriteLine("Syntax Error: Expected format 'INSERT INTO table_name (col1, col2) VALUES (val1, val2)'.");
                return null;
            }
            else if (input.StartsWith("SELECT * FROM", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(input, @"SELECT \* FROM (\w+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string tableName = match.Groups[1].Value;
                    return new SelectCommand(storage, tableName);
                }
                Console.WriteLine("Syntax Error: Expected format 'SELECT * FROM table_name'.");
                return null;
            }

            Console.WriteLine("Unrecognized command.");
            return null;
        }
    }
}
