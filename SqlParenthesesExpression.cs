//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql Parentheses expression class
//       CREATED: Kehhf on 2014/05/29
// LAST MODIFIED:  
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public sealed class SqlParenthesesExpression : SqlExpression
    {
        private SqlExpression _opnd;

        public SqlParenthesesExpression(SqlExpression opnd) {
            _opnd = opnd;
        }

        public override IEnumerable<SqlExpression> ExpressionTree {
            get {
                yield return _opnd;

                foreach (SqlExpression item in _opnd.ExpressionTree) {
                    yield return item;
                }
            }
        }

        public override string ToString() {
            string opnd = _opnd.ToString();

            if (!string.IsNullOrEmpty(opnd)) {
                return ("(" + opnd + ")");
            }

            return base.ToString();
        }
    }
}
