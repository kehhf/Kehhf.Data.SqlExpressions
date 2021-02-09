//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql literal expression class
//       CREATED: Kehhf on 2014/05/29
// LAST MODIFIED:  
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public sealed class SqlLiteralExpression<T> : SqlExpression
    {
        private T _value;

        public SqlLiteralExpression(T value) {
            _value = value;
        }

        public override string ToString() {
            return SqlLiteralUtils.ToLiteral<T>(_value);
        }
    }
}
