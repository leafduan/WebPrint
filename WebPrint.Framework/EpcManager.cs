using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPrint.Framework
{
    public class EpcManager
    {
        private static readonly string s_partitionDefault;
        private static readonly string s_filterValueDefault;
        private static readonly string s_headerDefault;
        private static readonly int s_headerBits;
        private static readonly int s_filterValueBits;
        private static readonly int s_partitionBits;
        private static readonly int s_gtinBits;
        private static readonly int s_serialNbrBits;

        public string Header { get; private set; }
        public string FilterValue { get; private set; }
        public string Partition { get; private set; }
        public GtinManager GTIN { get; set; }
        public string SerialNumber { get; set; }

        static EpcManager()
        {
            s_headerDefault = "48";
            s_filterValueDefault = "1";
            s_partitionDefault = "5";

            s_headerBits = 8;
            s_filterValueBits = 3;
            s_partitionBits = 3;
            s_gtinBits = 44;
            s_serialNbrBits = 38;
        }

        public EpcManager(string header, string filterValue, string partition)
        {
            this.Header = header;
            this.FilterValue = filterValue;
            this.Partition = partition;
        }

        /// <summary>
        /// 使用默认的 Header，FilterValue，Partition 值
        /// </summary>
        public EpcManager()
            : this(s_headerDefault, s_filterValueDefault, s_partitionDefault) 
        { }

        /// <summary>
        /// 生成24位十六进制的EPC 调用方法 EpcManager.UpcConvertEpc 实现
        /// </summary>
        /// <param name="upc"></param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public string GetEpc(string upc, string serialNumber)
        {
            return EpcManager.UpcConvertEpc(this.Header, this.FilterValue, this.Partition, upc, serialNumber);
        }

        /// <summary>
        /// 解析EPC
        /// </summary>
        /// <param name="epc">24位十六进制的EPC</param>
        public static EpcManager Parse(string epc)
        {
            var epcBinary = Utils.HexToBinary(epc);

            var headerBinary = epcBinary.Substring(0, s_headerBits);
            var header = Convert.ToInt32(headerBinary, 2).ToString();

            var filterValueBinary = epcBinary.Substring(s_headerBits, s_filterValueBits);
            var filterValue = Convert.ToInt32(filterValueBinary, 2).ToString();

            var partitionBinary = epcBinary.Substring(s_headerBits + s_filterValueBits, s_partitionBits);
            var partition = Convert.ToInt32(partitionBinary, 2).ToString();
            var companyPrefixAndItemReferenceBinary = epcBinary.Substring(s_headerBits + s_filterValueBits + s_partitionBits, s_gtinBits);

            var serialNumberBinary = epcBinary.Substring(s_headerBits + s_filterValueBits + s_partitionBits + s_gtinBits, s_serialNbrBits);
            var serialNumber = Convert.ToInt64(serialNumberBinary, 2).ToString();

            EpcManager epcManager = new EpcManager(header,filterValue,partition);
            epcManager.GTIN = GtinManager.ToGtinManager(companyPrefixAndItemReferenceBinary, partition);
            epcManager.SerialNumber = serialNumber;
            return epcManager;
        }

        /*
        /// <summary>
        /// 根据EPC获取GTIN
        /// </summary>
        /// <param name="epc"></param>
        /// <returns></returns>
        public string GetGtin(string epc) 
        {
            var epcBinary = Utils.HexToBinary(epc);
            var partitionBinary = epcBinary.Substring(s_headerBits + s_filterValueBits,s_partitionBits);
            var partition = Convert.ToInt32(partitionBinary, 2).ToString();
            var companyPrefixAndItemReferenceBinary = epcBinary.Substring(s_headerBits + s_filterValueBits + s_partitionBits,s_gtinBits);

            return GtinManager.ToGtin(companyPrefixAndItemReferenceBinary, partition);
        }
         * */

        /// <summary>
        /// 根据现有EPC更改serailNumber
        /// </summary>
        /// <param name="epc">24位十六进制数据</param>
        /// <param name="serialNumber">序列号</param>
        /// <returns></returns>
        public string ToEpc(string epc, string serialNumber) 
        {
            var epcBinary = Utils.HexToBinary(epc);
            //var epcBinaryWithoutSerialNumber = epcBinary.Substring(s_headerBits + s_filterValueBits + s_partitionBits + s_gtinBits, s_serialNbrBits);
            var epcBinaryWithoutSerialNumber = epcBinary.Substring(0, s_headerBits + s_filterValueBits + s_partitionBits + s_gtinBits);
            var serialNumberBinary = Convert.ToString(long.Parse(serialNumber), 2).PadLeft(s_serialNbrBits, '0');

            return EpcManager.BinaryEpcToHex(epcBinaryWithoutSerialNumber + serialNumberBinary);
        }

        #region static method
        /// <summary>
        /// 转成24位十六进制EPC 二进制为96位 使用默认的 header filterValue Partition
        /// </summary>
        /// <param name="upc">UPC will convert to GTIN</param>
        /// <param name="serialNumber">erial Nbr assigned by EPC Mgr for item</param>
        /// <returns></returns>
        public static string UpcConvertEpc(string upc, string serialNumber)
        {
            return UpcConvertEpc(s_headerDefault, s_filterValueDefault, s_partitionDefault, upc, serialNumber);
        }

        /// <summary>
        /// 转成24位十六进制EPC 二进制为96位
        /// </summary>
        /// <param name="header">96 bit GTIN</param>
        /// <param name="filterValue">Selling Unit</param>
        /// <param name="partition">7 digit Mfg Nbr including number system characters</param>
        /// <param name="upc">UPC will convert to GTIN</param>
        /// <param name="serialNumber">Serial Nbr assigned by EPC Mgr for item</param>
        /// <returns></returns>
        public static string UpcConvertEpc(string header, string filterValue, string partition, string upc, string serialNumber)
        {
            var gtin = new GtinManager(upc, partition);
            var headerBinary = Convert.ToString(int.Parse(header), 2).PadLeft(s_headerBits, '0');
            var filterValueBinary = Convert.ToString(int.Parse(filterValue), 2).PadLeft(s_filterValueBits, '0');
            var partitionBinary = Convert.ToString(int.Parse(partition), 2).PadLeft(s_partitionBits, '0');
            var gtinBinary = gtin.ToBinaryString();
            var serialNumberBinary = Convert.ToString(long.Parse(serialNumber), 2).PadLeft(s_serialNbrBits, '0');

            var epcBinary = string.Format("{0}{1}{2}{3}{4}", headerBinary, filterValueBinary, partitionBinary, gtinBinary, serialNumberBinary);

            return EpcManager.BinaryEpcToHex(epcBinary);
        }
        #endregion

        /// <summary>
        /// 96位二进制EPC 转成24位十六进制EPC
        /// </summary>
        /// <param name="epcBinary"></param>
        /// <returns></returns>
        private static string BinaryEpcToHex(string epcBinary)
        {
            var epcHex = new StringBuilder(24);
            for (int i = 0; i < 96; i += 4)
            {
                epcHex.Append(Utils.BinaryToHex(epcBinary.Substring(i, 4)));
            }

            return epcHex.ToString();
        }
    }

    /// <summary>
    /// Global Trade Item Number
    /// </summary>
    public class GtinManager
    {
        private static readonly Dictionary<string, Partitiion> PartitionTable;
        private static readonly int s_gtinDigits;

        /// <summary>
        /// 
        /// </summary>
        public string Upc { get; private set; }
        /// <summary>
        /// UPC对应的GTIN
        /// </summary>
        public string Gtin { get; set; }
        public string Partition { get; private set; }
        /// <summary>
        /// Mfg Nbr including number system characters
        /// </summary>
        public string CompanyPrefix { get; set; }
        /// <summary>
        /// Item number assigned by EPC Mgr
        /// </summary>
        public string ItemReference { get; set; }

        static GtinManager()
        {
            PartitionTable = new Dictionary<string, Partitiion>
            {
                {"0", new Partitiion(0, 40, 12, 4, 1)},
                {"1", new Partitiion(1, 37, 11, 7, 2)},
                {"2", new Partitiion(2, 34, 10, 10, 3)},
                {"3", new Partitiion(3, 30, 9, 14, 4)},
                {"4", new Partitiion(4, 27, 8, 17, 5)},
                {"5", new Partitiion(5, 24, 7, 20, 6)},
                {"6", new Partitiion(6, 20, 6, 24, 7)}
            };

            s_gtinDigits = 14;
        }
        

        /// <summary>
        /// 根据UPC及partition获得EPC对应的 CompanyPrefix 和 ItemReference
        /// </summary>
        /// <param name="upc"></param>
        /// <param name="partition"></param>
        public GtinManager(string upc, string partition)
        {
            this.Upc = upc;
            this.Partition = partition;
            this.Gtin = GtinManager.ToGtin(upc);

            Identify();
        }

        private GtinManager(string partition)
        {
            this.Upc = string.Empty;
            this.Partition = partition;
        }

        internal static GtinManager ToGtinManager(string companyPrefixAndItemReferenceBinary,string partition)
        {
            GtinManager gtinManager = new GtinManager(partition);

            Partitiion _partition;
            if (PartitionTable.TryGetValue(partition, out _partition))
            {
                var companyPrefixBinary = companyPrefixAndItemReferenceBinary.Substring(0, _partition.CompanyPrefixBits);
                var itemReferenceBinary = companyPrefixAndItemReferenceBinary.Substring(_partition.CompanyPrefixBits, _partition.ItemReferenceBits);
                var companyPrefix = Convert.ToInt32(companyPrefixBinary, 2).ToString().PadLeft(_partition.CompanyPrefixDigits, '0');
                var itemReference = Convert.ToInt32(itemReferenceBinary, 2).ToString().PadLeft(_partition.ItemReferenceDigits, '0');
                var gtin = itemReferenceBinary.First().ToString() + companyPrefix + itemReference.Substring(1);
                var checkNumber = GenerateCheckNumber(gtin);

                gtinManager.Gtin = gtin + checkNumber;
                gtinManager.CompanyPrefix = companyPrefix;
                gtinManager.ItemReference = itemReference;
            }

            return gtinManager;
        }

        /// <summary>
        /// GTIN 分解成 CompanyPrefix 和 ItemReference
        /// </summary>
        private void Identify()
        {
            #region
            /*
            switch (this.Partition)
            {
                case "5":
                    var indicatorDigit = this.Gtin.First().ToString();
                    CompanyPrefix = Gtin.Substring(1, 7);
                    ItemReference = indicatorDigit + Gtin.Substring(8, 13);
                    break;
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "6":
                default:
                    CompanyPrefix = string.Empty;
                    ItemReference = string.Empty;
                    break;
            }
             * */
            #endregion

            Partitiion _partition;
            if (PartitionTable.TryGetValue(this.Partition, out _partition))
            {
                var indicatorDigit = this.Gtin.First().ToString();
                CompanyPrefix = Gtin.Substring(1, _partition.CompanyPrefixDigits);
                ItemReference = indicatorDigit + Gtin.Substring((_partition.CompanyPrefixDigits + 1),_partition.ItemReferenceDigits - 1);
            }
            else
            {
                CompanyPrefix = string.Empty;
                ItemReference = string.Empty;
            }
        }

        public static string ToGtin(string upc)
        {
            if (!GtinManager.IsValid(upc)) throw new InvalidCastException("upc is not valid");

            return upc.PadLeft(s_gtinDigits, '0');
        }
        /*
        public static string ToGtin(string companyPrefixAndItemReferenceBinary, string partition)
        {
            Partitiion _partition;
            if (PartitionTable.TryGetValue(partition, out _partition))
            {
                var companyPrefixBinary = companyPrefixAndItemReferenceBinary.Substring(0, _partition.CompanyPrefixBits);
                var itemReferenceBinary = companyPrefixAndItemReferenceBinary.Substring(_partition.CompanyPrefixBits, _partition.ItemReferenceBits);
                var companyPrefix = Convert.ToInt32(companyPrefixBinary, 2).ToString().PadLeft(_partition.CompanyPrefixDigits, '0');
                var itemReference = Convert.ToInt32(itemReferenceBinary, 2).ToString().PadLeft(_partition.ItemReferenceDigits, '0');
                var gtin = itemReferenceBinary.First().ToString() + companyPrefix + itemReference.Substring(1);
                var checkNumber = GenerateCheckNumber(gtin);

                return gtin + checkNumber;
            }

            return string.Empty;
        }
         * */

        /// <summary>
        /// 判断UPC对应的GTIN是否合法
        /// </summary>
        /// <param name="upc">带检测的UPC</param>
        /// <returns></returns>
        public static bool IsValid(string upc)
        {
            string gtin = upc.PadLeft(s_gtinDigits, '0');
            string orignCheckNumber = gtin.Last().ToString();
            string generatedheckNumber = GtinManager.GenerateCheckNumber(gtin.Substring(0, gtin.Length - 1));

            return string.Compare(orignCheckNumber, generatedheckNumber) == 0;
        }

        /// <summary>
        /// 获得GTIN的 Check Number
        /// </summary>
        /// <param name="gtinWithoutCheckNumber"></param>
        /// <returns></returns>
        public static string GenerateCheckNumber(string gtinWithoutCheckNumber)
        {
            int i = 1;
            int sum = 0;
            int checkNumber = 0;
            foreach (char c in gtinWithoutCheckNumber)
            {
                if (i % 2 == 0)
                {
                    sum += int.Parse(c.ToString()) * 1;
                }
                else
                {
                    sum += int.Parse(c.ToString()) * 3;
                }
                i++;
            }

            for (checkNumber = 0; checkNumber < 10; checkNumber++)
            {
                if ((sum + checkNumber) % 10 == 0)
                {
                    break;
                }
            }

            return checkNumber.ToString();
        }

        /// <summary>
        /// 将GTIN转成二进制 共44位
        /// </summary>
        /// <returns></returns>
        public string ToBinaryString()
        {
            Partitiion _partition;
            var companyPrefixBinary = string.Empty;
            var itemReferenceBinary = string.Empty;
            if (PartitionTable.TryGetValue(this.Partition, out _partition))
            {
                companyPrefixBinary = Convert.ToString(int.Parse(this.CompanyPrefix), 2).PadLeft(_partition.CompanyPrefixBits, '0');
                itemReferenceBinary = Convert.ToString(int.Parse(this.ItemReference), 2).PadLeft(_partition.ItemReferenceBits, '0');
            }
            else
            {
                companyPrefixBinary = string.Empty.PadLeft(_partition.CompanyPrefixBits, '0');
                itemReferenceBinary = string.Empty.PadLeft(_partition.ItemReferenceBits, '0');
            }

            return companyPrefixBinary + itemReferenceBinary;
        }

       
    }

    internal struct Partitiion
    {
        public Partitiion(int partitionNumber, int companyPrefixBits, int companyPrefixDigits, int itemReferenceBits, int itemReferenceDigits)
        {
            this.PartitionNumber = partitionNumber;
            this.CompanyPrefixBits = companyPrefixBits;
            this.CompanyPrefixDigits = companyPrefixDigits;
            this.ItemReferenceBits = itemReferenceBits;
            this.ItemReferenceDigits = itemReferenceDigits;
        }

        public int PartitionNumber;
        public int CompanyPrefixBits;
        public int CompanyPrefixDigits;
        public int ItemReferenceBits;
        public int ItemReferenceDigits;
    }

    /*
    /// <summary>
    /// Universal Product Code
    /// </summary>
    public class UpcManager
    {
        public UpcManager(){}
        public UpcManager(string value){this.Value = value;}

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                if (!UpcManager.IsValid(value))
                {
                    _value = string.Empty;
                    throw new ArgumentException("The UPC value is not valid.");
                }
                _value = value;
            }
        }

        public static bool IsValid(string value)
        {
            return true;
        }
        public static string GenerateCheckNumber(string valueWithoutCheckNbr)
        {
            return string.Empty; 
        }

        public GtinManager ToGtinManager(string partition)
        {
            return new GtinManager(this.Value, partition);
        }

        /// <summary>
        /// 返回Value值
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Value;
        }
    }
     */
}
