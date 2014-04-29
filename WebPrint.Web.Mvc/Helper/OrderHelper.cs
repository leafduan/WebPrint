using System;
using System.Globalization;
using WebPrint.Framework;

namespace WebPrint.Web.Mvc.Helper
{
    public static class OrderHelper
    {
        private const string OrderSystem = "WP";
        private const string JnoFormat = "{0}{1}{2}";
        private const string DateTimeFormat = "yyyyMMdd";
        private const int OrderIdLength = 7;

        public static string AsJobNo(this int orderId)
        {
            var date = DateTime.Now.ToString(DateTimeFormat);
            var id = orderId.ToString(CultureInfo.InvariantCulture).PadLeft(OrderIdLength, '0');
            return JnoFormat.Formatting(OrderSystem, date, id);
        }

        public static int AsOrderId(this string jno)
        {
            var prefixLength = OrderSystem.Length + DateTimeFormat.Length;

            if (jno.IsNullOrEmpty() || jno.Length <= prefixLength)
                return 0;

            return jno
                .Substring(prefixLength, jno.Length - prefixLength)
                .AsInt();
        }
    }
}
