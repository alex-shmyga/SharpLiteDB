using System.Text;

namespace SharpLiteDB.Models
{
    internal class Row
    {
        internal IDictionary<string, object> Data { get; private set; }

        internal Row(IDictionary<string, object> data)
        {
            Data = data;
        }

        public int GetSerializedSize()
        {
            int size = 0;

            foreach (var value in Data.Values)
            {
                if (value is int)
                    size += 4;
                else if (value is string str)
                    size += Encoding.UTF8.GetByteCount(str) + 4;
                else if (value is byte[] byteArray)
                    size += byteArray.Length + 4;
                else
                    throw new NotImplementedException("Unsupported data type.");
            }

            return size;
        }

        public byte[] Serialize()
        {
            List<byte> rowBytes = new List<byte>();

            foreach (var value in Data.Values)
            {
                if (value is int intValue)
                    rowBytes.AddRange(BitConverter.GetBytes(intValue));
                else if (value is string strValue)
                {
                    byte[] strBytes = Encoding.UTF8.GetBytes(strValue);
                    rowBytes.AddRange(BitConverter.GetBytes(strBytes.Length)); // Length prefix
                    rowBytes.AddRange(strBytes);
                }
                else if (value is byte[] byteArrayValue)
                {
                    rowBytes.AddRange(BitConverter.GetBytes(byteArrayValue.Length)); // Length prefix
                    rowBytes.AddRange(byteArrayValue);
                }
                else
                    throw new NotImplementedException("Unsupported data type.");
            }

            return rowBytes.ToArray();
        }

        public static Row Deserialize(byte[] rowData)
        {
            var data = new Dictionary<string, object>();
            int offset = 0;

            // hardcoded so far
            int id = BitConverter.ToInt32(rowData, offset);
            offset += 4;
            data["id"] = id;

            offset += 4;
            int valueLength = rowData.Length - offset;
            string value = Encoding.UTF8.GetString(rowData, offset, valueLength);
            data["value"] = value;

            return new Row(data);
        }
    }
}
