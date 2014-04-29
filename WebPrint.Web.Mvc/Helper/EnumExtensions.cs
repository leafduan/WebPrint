using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using WebPrint.Framework;

namespace WebPrint.Web.Mvc.Helper
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this Enum value)
        {
            return ToSelectList(value.GetType(), string.Empty);
        }

        public static IEnumerable<SelectListItem> ToSelectList(this Enum value, string selectedValue)
        {
            /*
            return Enum
                .GetNames(e.GetType())
                .Select(s => new SelectListItem {Text = s, Value = s, Selected = (s == selectedValue)});
            */

            return ToSelectList(value.GetType(), selectedValue);
        }

        public static IEnumerable<SelectListItem> ToSelectList(this Type enumType)
        {
            return ToSelectList(enumType, string.Empty);
        }

        public static IEnumerable<SelectListItem> ToSelectList(this Type enumType, string selectedValue)
        {
            if (!enumType.IsEnum) throw new InvalidOperationException("argument enumType is not a Enum");

            return Enum
                .GetValues(enumType)
                .Cast<Enum>()
                .Select(value => new SelectListItem
                    {
                        Text = value.GetDescription(),
                        Value = value.ToString(),
                        Selected = value.ToString().EqualTo(selectedValue, true)
                    });
        }
    }
}