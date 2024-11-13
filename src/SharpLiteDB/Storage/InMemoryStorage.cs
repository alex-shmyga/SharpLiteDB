using SharpLiteDB.Models;

namespace SharpLiteDB.Storage
{
    internal class InMemoryStorage : IStorage
    {
        private IDictionary<string, Table> tables = new Dictionary<string, Table>();

        public void CreateTable(string name)
        {
            if (tables.ContainsKey(name))
                throw new Exception("Table already exists.");

            tables[name] = new Table(name);
            Console.WriteLine($"InMemoryStorage:Table '{name}' created.");
        }

        public void DropTable(string name)
        {
            if (!tables.ContainsKey(name))
                throw new Exception("Table does not exist.");

            tables.Remove(name);
            Console.WriteLine($"InMemoryStorage:Table '{name}' was removed.");
        }

        public void InsertRow(string tableName, Dictionary<string, object> rowData)
        {
            if (!tables.ContainsKey(tableName))
                throw new Exception("Table does not exist.");

            var row = new Row(rowData);
            tables[tableName].InsertRow(row);
            Console.WriteLine("InMemoryStorage:Row inserted.");
        }

        public IList<Row> SelectAll(string tableName)
        {
            if (!tables.ContainsKey(tableName))
                throw new Exception("Table does not exist.");

            return tables[tableName].Rows;
        }
    }
}
