using SharpLiteDB.Models;

namespace SharpLiteDB.Storage
{
    internal class DiskStorage : IStorage
    {
        private IDictionary<string, Table> _tables = new Dictionary<string, Table>();

        public void CreateTable(string name)
        {
            if (_tables.ContainsKey(name))
                throw new Exception("Table already exists.");

            var table = new Table(name);
            table.SavePagesToDisk();
            _tables[name] = table;
            Console.WriteLine($"DiskStorage: Table '{name}' created and saved to disk.");
        }

        public void DropTable(string name)
        {
            if (!_tables.ContainsKey(name))
                throw new Exception("Table does not exist.");

            // Remove table files from disk
            int pageIndex = 0;
            while (File.Exists($"{name}_page_{pageIndex}.pg"))
            {
                File.Delete($"{name}_page_{pageIndex}.pg");
                pageIndex++;
            }

            _tables.Remove(name);
            Console.WriteLine($"DiskStorage: Table '{name}' was removed from disk.");
        }

        public void InsertRow(string tableName, Dictionary<string, object> rowData)
        {
            if (!_tables.ContainsKey(tableName))
                throw new Exception("Table does not exist.");

            var row = new Row(rowData);
            var table = _tables[tableName];
            table.InsertRow(row);
            table.SavePagesToDisk();
            Console.WriteLine("DiskStorage: Row inserted and pages saved to disk.");
        }

        public IList<Row> SelectAll(string tableName)
        {
            if (!_tables.ContainsKey(tableName))
                throw new Exception("Table does not exist.");

            var table = _tables[tableName];
            table.LoadPagesFromDisk();
            return table.Rows;
        }
    }
}
