//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql parametric expression class
//       CREATED: Kehhf on 2020/09/20
// LAST MODIFIED:  
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public sealed class SqlParametricExpression<T> : SqlExpression, ISqlParametric
    {
        public SqlParametricExpression(string parameterName, T value) {
            ParameterName = parameterName;
            Value = value;
            DbType = _dbTypeMap[typeof(T)];
        }

        public DbType DbType { get; private set; }

        public string ParameterName { get; private set; }

        public object Value { get; private set; }

        public override string ToString() {
            return ParameterName;
        }

        private static readonly Dictionary<Type, DbType> _dbTypeMap = new Dictionary<Type, DbType>() {
            { typeof(DateTime), DbType.DateTime2 },
            { typeof(decimal), DbType.Decimal },
            { typeof(int), DbType.Int32 },
            { typeof(string), DbType.String }
        };
    }

    public interface ISqlParametric
    {
        DbType DbType { get; }

        string ParameterName { get; }

        object Value { get; }
    }
}
