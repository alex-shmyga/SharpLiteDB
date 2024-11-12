﻿using SharpLiteDB.Models;

namespace SharpLiteDB.Storage
{
    internal class InMemoryStorage
    {
        private IDictionary<string, Table> tables = new Dictionary<string, Table>();

        internal void CreateTable(string name)
        {
            if (tables.ContainsKey(name))
                throw new Exception("Table already exists.");

            tables[name] = new Table(name);
            Console.WriteLine($"Table '{name}' created.");
        }

        internal void InsertRow(string tableName, Dictionary<string, object> rowData)
        {
            if (!tables.ContainsKey(tableName))
                throw new Exception("Table does not exist.");

            var row = new Row(rowData);
            tables[tableName].InsertRow(row);
            Console.WriteLine("Row inserted.");
        }

        internal IList<Row> SelectAll(string tableName)
        {
            if (!tables.ContainsKey(tableName))
                throw new Exception("Table does not exist.");

            return tables[tableName].Rows;
        }
    }
}