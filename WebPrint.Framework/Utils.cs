using System;
using System.Collections.Generic;
using System.Text;

namespace WebPrint.Framework
{
    public static class Utils
    {
        /// <summary>
        /// 16进制字符串转成byte数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length/2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i*2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// byte数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>
        /// Bit convert Hex
        /// </summary>
        /// <param name="fourBinary"></param>
        /// <returns></returns>
        public static string BinaryToHex(string fourBinary)
        {
            string hex;
            Binary2Hex.TryGetValue(fourBinary, out hex);

            return hex;
        }

        /// <summary>
        /// 十六进制转二进制
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexToBinary(string hex)
        {
            var binaryArray = new string[hex.Length];
            for (var i = 0; i < hex.Length; i++)
            {
                binaryArray[i] = hex[i] >= 'a'
                                     ? Hex2Binary[hex[i] - 'a' + 10]
                                     : (hex[i] >= 'A' ? Hex2Binary[hex[i] - 'A' + 10] : Hex2Binary[hex[i] - '0']);
            }

            return string.Join("", binaryArray);
        }

        #region hex2binary or binary2hex
        private static readonly Dictionary<string, string> Binary2Hex = new Dictionary<string, string>
            {
                {"0000", "0"},
                {"0001", "1"},
                {"0010", "2"},
                {"0011", "3"},
                {"0100", "4"},
                {"0101", "5"},
                {"0110", "6"},
                {"0111", "7"},
                {"1000", "8"},
                {"1001", "9"},
                {"1010", "A"},
                {"1011", "B"},
                {"1100", "C"},
                {"1101", "D"},
                {"1110", "E"},
                {"1111", "F"}
            };

        private static readonly string[] Hex2Binary =
            {
                "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111",
                "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111"
            };

        #endregion
    }
}
