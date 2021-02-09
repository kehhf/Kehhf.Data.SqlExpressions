//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql binary expression class
//       CREATED: Kehhf on 2014/05/29
// LAST MODIFIED:  
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public sealed class SqlBinaryExpression : SqlExpression
    {
        private string _op;
        private SqlExpression _opndLeft;
        private SqlExpression _opndRight;

        public SqlBinaryExpression(string op, SqlExpression opndLeft, SqlExpression opndRight) {
            _op = op;
            _opndLeft = opndLeft;
            _opndRight = opndRight;
        }

        public override IEnumerable<SqlExpression> ExpressionTree {
            get {
                yield return _opndLeft;

                foreach (SqlExpression item in _opndLeft.ExpressionTree) {
                    yield return item;
                }

                yield return _opndRight;

                foreach (SqlExpression item in _opndRight.ExpressionTree) {
                    yield return item;
                }
            }
        }

        public override string ToString() {
            string left = _opndLeft.ToString();
            string right = _opndRight.ToString();

            if (!string.IsNullOrEmpty(left)) {
                if (!string.IsNullOrEmpty(right)) {
                    return string.Concat(left, " ", _op, " ", right);
                } else {
                    return left;
                }
            } else {
                if (!string.IsNullOrEmpty(right)) {
                    return right;
                }
            }

            return base.ToString();
        }
    }
}
