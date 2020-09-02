using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAsyncClient
{
    class PersonPackage
    {
        public string User { get; set; }
        public string Text { get; set; }

        public PersonPackage(string user, string text)
        {
            User = user;
            Text = text;
        }

        /// <summary>
        /// Deserialize data received.
        /// </summary>
        public PersonPackage(byte[] data)
        {
            int UserLength = BitConverter.ToInt16(data, 0);
            int nameLength = BitConverter.ToInt16(data, 4);

            User = Encoding.ASCII.GetString(data, 8, UserLength);
            Text = Encoding.ASCII.GetString(data, UserLength + 8, nameLength);
        }

        /// <summary>
        ///  Serializes this package to a byte array.
        /// </summary>
        /// <remarks>
        /// Use the <see cref="Buffer"/> or <see cref="Array"/> class for better performance.
        /// </remarks>
        public byte[] ToByteArray()
        {
            List<byte> byteList = new List<byte>();

            byteList.AddRange(BitConverter.GetBytes(User.Length));
            byteList.AddRange(BitConverter.GetBytes(Text.Length));
            byteList.AddRange(Encoding.ASCII.GetBytes(User));
            byteList.AddRange(Encoding.ASCII.GetBytes(Text));
            return byteList.ToArray();
        }
    }
}
