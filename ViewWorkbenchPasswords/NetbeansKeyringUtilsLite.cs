using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedStuffViewer
{
    class NetbeansKeyringUtilsLite
    {
        public static byte[] chars2Bytes(char[] chars)
        {
            byte[] bytes = new byte[chars.Length * 2];
            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i * 2] = (byte)((int)chars[i] / 256);
                bytes[i * 2 + 1] = (byte)(chars[i] % 256);
            }
            return bytes;
        }

        public static char[] bytes2Chars(byte[] bytes)
        {
            char[] result = new char[bytes.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (char)(((bytes[i * 2] & 0x00ff) * 256) + (bytes[i * 2 + 1] & 0x00ff));
            }
            return result;
        }
    }
}
