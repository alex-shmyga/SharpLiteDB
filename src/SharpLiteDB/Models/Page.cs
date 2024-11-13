namespace SharpLiteDB.Models
{
    internal class Page
    {
        public const int PageSize = 4096; // 4 KB per page
        private List<Row> _rows;

        public Page()
        {
            _rows = new List<Row>();
        }

        public List<Row> Rows => _rows;

        public bool IsFull => GetSerializedSize() >= PageSize;

        public int GetSerializedSize()
        {
            return _rows.Sum(row => row.GetSerializedSize());
        }

        public void AddRow(Row row)
        {
            if (IsFull)
                throw new InvalidOperationException("Page is full.");

            _rows.Add(row);
        }

        public byte[] Serialize()
        {
            List<byte> pageBytes = new List<byte>();

            foreach (var row in _rows)
            {
                byte[] rowData = row.Serialize();
                pageBytes.AddRange(BitConverter.GetBytes(rowData.Length)); // Length prefix
                pageBytes.AddRange(rowData);
            }

            return pageBytes.ToArray();
        }

        public static Page Deserialize(byte[] data)
        {
            var page = new Page();
            int offset = 0;

            while (offset < data.Length)
            {
                if (offset + 4 > data.Length)
                    throw new InvalidOperationException("Not enough data to read row length prefix.");

                int rowLength = BitConverter.ToInt32(data, offset);
                offset += 4;

                if (offset + rowLength > data.Length)
                    throw new InvalidOperationException("Not enough data to read the full row.");

                byte[] rowData = new byte[rowLength];
                Array.Copy(data, offset, rowData, 0, rowLength);
                offset += rowLength;

                Row row = Row.Deserialize(rowData);
                page._rows.Add(row);
            }

            return page;
        }
    }
}
