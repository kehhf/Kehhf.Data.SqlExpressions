//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql Unary expression class
//       CREATED: Kehhf on 2014/05/29
// LAST MODIFIED:  
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public sealed class SqlUnaryExpression : SqlExpression
    {
        private string _op;
        private SqlExpression _opnd;

        public SqlUnaryExpression(string op, SqlExpression opnd) {
            _op = op;
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
                return (_op + " " + opnd);
            }

            return base.ToString();
        }
    }
}
