using SharpLiteDB.Models;

namespace SharpLiteDB.Storage
{
    internal interface IStorage
    {
        void CreateTable(string name);
        void DropTable(string name);
        void InsertRow(string tableName, Dictionary<string, object> rowData);
        IList<Row> SelectAll(string tableName);
    }
}