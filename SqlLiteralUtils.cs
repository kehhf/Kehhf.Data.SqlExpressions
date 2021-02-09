//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql literal utilities
//       CREATED: Kehhf on 2014/05/29
// LAST MODIFIED:  
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public static class SqlLiteralUtils
    {
        public static string JoinLiterals(IEnumerable<string> list, string separator = ",", bool appendParenthese = true) {
            string result = string.Join(separator, list);

            if (appendParenthese) {
                result = "(" + result + ")";
            }

            return result;
        }

        public static string ToLiteral(DateTime value) {
            return ("'" + value.ToString(value.Ticks % 0xc92a69c000L == 0 ? "yyyy-MM-dd" : "yyyy-MM-dd HH:mm:ss.fff") + "'");
        }

        public static string ToLiteral(string value) {
            return ("'" + value.Replace("'", "''") + "'");
        }

        public static string ToLiteral<T>(object value) {
            string result = null;

            switch (Type.GetTypeCode(typeof(T))) {
                case TypeCode.Char:
                    result = ToLiteral(value.ToString());
                    break;
                case TypeCode.DateTime:
                    result = ToLiteral((DateTime)value);
                    break;
                case TypeCode.String:
                    result = ToLiteral((string)value);
                    break;
                default:
                    result = value.ToString();
                    break;
            }

            return result;
        }

        public static IEnumerable<string> ToLiteral(IEnumerable<string> list) {
            return list.Select(x => ToLiteral(x));
        }
    }
}
