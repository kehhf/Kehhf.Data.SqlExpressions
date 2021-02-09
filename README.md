Expressions for building sql statement
==
Pass WHERE clause in an OO way
--
   
### For example:
```
private SqlExpression BuildSearchExpression(DateTime logTimeFrom, DateTime logTimeTo, string userCode, bool appendWhere = true) {
    SqlExpression expression;

    expression = SqlExpression.And(
        SqlExpression.Column("LogTime", "").GreaterThanOrEqual(SqlExpression.Parametric<DateTime>("@logTimeFrom", logTimeFrom)),
        SqlExpression.Column("LogTime", "").LessThanOrEqual(SqlExpression.Parametric<DateTime>("@logTimeTo", logTimeTo))
    ).Parentheses();

    if (!string.IsNullOrEmpty(search.UserCode)) {
        expression = expression.And(SqlExpression.Column("UserCode", "").Equal(SqlExpression.Parametric<string>("@userCode", userCode)));
    }

    return (!appendWhere ? expression : SqlExpression.Where(expression));
}
```
### After call
```
SqlExpression expression = BuildSearchExpression(search);
expression.ToString()；
```
### Then return string like
```
WHERE (LogTime >= @logTimeFrom AND LogTime <= @logTimeTo) AND UserCode = @userCode
```
### Extract the parametric for building DbParameters
```
IEnumerable<ISqlParametric> list = expression.GetSqlParametrics()；
IEnumerable<DbParameter> parameters = list.Select(x => ( ... ));
```