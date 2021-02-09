//+---------------------------------------------------------------------+
//   DESCRIPTION: Sql expression class
//       CREATED: Kehhf on 2014/05/29
// LAST MODIFIED: Kehhf on 2020/09/20
//+---------------------------------------------------------------------+

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kehhf.Data.SqlExpressions
{
    public class SqlExpression
    {
        public static readonly SqlExpression Empty = new SqlExpression();

        protected int _version = 0;
        protected int _versionPET = 0;
        protected List<SqlExpression> _expressionTree = null;

        protected SqlExpression() {
            _version++;
        }

        public virtual IEnumerable<SqlExpression> ExpressionTree {
            get {
                yield return null;
            }
        }

        public IEnumerable<ISqlParametric> GetSqlParametrics() {
            return PersistExpressionTree().Where(x => x is ISqlParametric).Cast<ISqlParametric>();
        }

        public List<SqlExpression> PersistExpressionTree() {
            if (_version > _versionPET) {
                _expressionTree = ExpressionTree.ToList();
                _versionPET = _version;
            }

            return _expressionTree;
        }

        public void SetTableAlias(params string[] nameAliasPairs) {
            int pairs = (nameAliasPairs.Length >> 1 << 1);

            if (pairs == 0) { return; }

            IEnumerable<SqlColumnExpression> columns = PersistExpressionTree().Where(x => x is SqlColumnExpression).Cast<SqlColumnExpression>();

            foreach (SqlColumnExpression item in columns) {
                for (int i = 0; i < pairs; i += 2) {
                    if (item._tableName == nameAliasPairs[i]) {
                        item._tableAlias = nameAliasPairs[i + 1];
                    }
                }
            }
        }

        public override string ToString() {
            return "";
        }

        #region << Instance help method >>
        public SqlExpression And(SqlExpression opndRight) {
            return And(this, opndRight);
        }

        public SqlExpression BitwiseAnd(SqlExpression opndRight) {
            return BitwiseAnd(this, opndRight);
        }

        public SqlExpression BitwiseAndAssign(SqlExpression opndRight) {
            return BitwiseAndAssign(this, opndRight);
        }

        public SqlExpression BitwiseNot() {
            return BitwiseNot(this);
        }

        public SqlExpression BitwiseOr(SqlExpression opndRight) {
            return BitwiseOr(this, opndRight);
        }

        public SqlExpression BitwiseOrAssign(SqlExpression opndRight) {
            return BitwiseOrAssign(this, opndRight);
        }

        public SqlExpression BitwiseXOr(SqlExpression opndRight) {
            return BitwiseXOr(this, opndRight);
        }

        public SqlExpression BitwiseXOrAssign(SqlExpression opndRight) {
            return BitwiseXOrAssign(this, opndRight);
        }

        public SqlExpression Comma(SqlExpression opndRight) {
            return Comma(this, opndRight);
        }

        public SqlExpression Equal(SqlExpression opndRight) {
            return Equal(this, opndRight);
        }

        public SqlExpression Exists() {
            return Exists(this);
        }

        public SqlExpression GreaterThan(SqlExpression opndRight) {
            return GreaterThan(this, opndRight);
        }

        public SqlExpression GreaterThanOrEqual(SqlExpression opndRight) {
            return GreaterThanOrEqual(this, opndRight);
        }

        public SqlExpression In(SqlExpression opndRight) {
            return In(this, opndRight);
        }

        public SqlExpression Is(SqlExpression opndRight) {
            return Is(this, opndRight);
        }

        public SqlExpression LessThan(SqlExpression opndRight) {
            return LessThan(this, opndRight);
        }

        public SqlExpression LessThanOrEqual(SqlExpression opndRight) {
            return LessThanOrEqual(this, opndRight);
        }

        public SqlExpression Like(SqlExpression opndRight) {
            return Like(this, opndRight);
        }

        public SqlExpression Merge(SqlExpression opndRight) {
            return Merge(this, opndRight);
        }

        public SqlExpression Not() {
            return Not(this);
        }

        public SqlExpression NotEqual(SqlExpression opndRight) {
            return NotEqual(this, opndRight);
        }

        public SqlExpression NotExists() {
            return NotExists(this);
        }

        public SqlExpression NotIn(SqlExpression opndRight) {
            return NotIn(this, opndRight);
        }

        public SqlExpression NotLike(SqlExpression opndRight) {
            return NotLike(this, opndRight);
        }

        public SqlExpression Or(SqlExpression opndRight) {
            return Or(this, opndRight);
        }

        public SqlExpression Parentheses() {
            return Parentheses(this);
        }

        public SqlExpression Where() {
            return Where(this);
        }
        #endregion

        #region << Class help method >>
        public static SqlExpression And(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("AND", opndLeft, opndRight);
        }

        public static SqlExpression BitwiseAnd(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("&", opndLeft, opndRight);
        }

        public static SqlExpression BitwiseAndAssign(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("&=", opndLeft, opndRight);
        }

        public static SqlExpression BitwiseNot(SqlExpression opnd) {
            return new SqlUnaryExpression("~", opnd);
        }

        public static SqlExpression BitwiseOr(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("|", opndLeft, opndRight);
        }

        public static SqlExpression BitwiseOrAssign(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("|=", opndLeft, opndRight);
        }

        public static SqlExpression BitwiseXOr(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("^", opndLeft, opndRight);
        }

        public static SqlExpression BitwiseXOrAssign(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("^=", opndLeft, opndRight);
        }

        public static SqlExpression Column(string columnName, string tableName = null, string columnAlias = null, string tableAlias = null) {
            return new SqlColumnExpression(columnName, tableName, columnAlias, tableAlias);
        }

        public static SqlExpression Comma(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression(",", opndLeft, opndRight);
        }

        public static SqlExpression Equal(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("=", opndLeft, opndRight);
        }

        public static SqlExpression Exists(SqlExpression opnd) {
            return new SqlUnaryExpression("EXISTS", opnd);
        }

        public static SqlExpression GreaterThan(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression(">", opndLeft, opndRight);
        }

        public static SqlExpression GreaterThanOrEqual(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression(">=", opndLeft, opndRight);
        }

        public static SqlExpression In(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("IN", opndLeft, opndRight);
        }

        public static SqlExpression Is(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("IS", opndLeft, opndRight);
        }

        public static SqlExpression LessThan(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("<", opndLeft, opndRight);
        }

        public static SqlExpression LessThanOrEqual(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("<=", opndLeft, opndRight);
        }

        public static SqlExpression Like(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("LIKE", opndLeft, opndRight);
        }

        public static SqlExpression Literal<T>(T value) {
            return new SqlLiteralExpression<T>(value);
        }

        public static SqlExpression Merge(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("", opndLeft, opndRight);
        }

        public static SqlExpression Not(SqlExpression opnd) {
            return new SqlUnaryExpression("NOT", opnd);
        }

        public static SqlExpression NotEqual(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("<>", opndLeft, opndRight);
        }

        public static SqlExpression NotExists(SqlExpression opnd) {
            return new SqlUnaryExpression("NOT EXISTS", opnd);
        }

        public static SqlExpression NotIn(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("NOT IN", opndLeft, opndRight);
        }

        public static SqlExpression NotLike(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("NOT LIKE", opndLeft, opndRight);
        }

        public static SqlExpression Or(SqlExpression opndLeft, SqlExpression opndRight) {
            return new SqlBinaryExpression("OR", opndLeft, opndRight);
        }

        public static SqlExpression Parametric<T>(string parameterName, T value) {
            return new SqlParametricExpression<T>(parameterName, value);
        }

        public static SqlExpression Parentheses(SqlExpression opnd) {
            return new SqlParenthesesExpression(opnd);
        }

        public static SqlExpression Where(SqlExpression opnd) {
            return new SqlUnaryExpression("WHERE", opnd);
        }
        #endregion
    }
}
