//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql column expression class
//       CREATED: Kehhf on 2014/05/29
// LAST MODIFIED:  
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public sealed class SqlColumnExpression : SqlExpression
    {
        private string _columnAlias;
        private string _columnName;
        internal string _tableAlias;
        internal string _tableName;

        public SqlColumnExpression(string columnName, string tableName, string columnAlias, string tableAlias) {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentException("Column name can not be empty.", "columnName");

            _columnName = columnName;
            _tableName = tableName;
            _columnAlias = columnAlias;
            _tableAlias = tableAlias;
        }

        public override string ToString() {
            string result = null;

            if (!string.IsNullOrEmpty(_tableName)) {
                result = (!string.IsNullOrEmpty(_tableAlias) ? _tableAlias : _tableName) + ".";
            }

            result += _columnName;

            if (!string.IsNullOrEmpty(_columnAlias)) {
                result = string.Concat(result, " AS ", _columnAlias);
            };

            return result;
        }
    }
}
