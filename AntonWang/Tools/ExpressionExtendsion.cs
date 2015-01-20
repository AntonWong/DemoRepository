using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{

    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExpressionExtendsion
    {
        #region  方法名:And Expression<Func<T, bool>>扩展方法-添加多项条件
        /// <summary>
        /// 添加多项条件
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="first">Lamda表达式1</param>
        /// <param name="second">Lamda表达式2</param>
        /// <returns>Lamda表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        #region private ParameterRebinder

        private class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
                Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }

        #endregion
        #region AndInvoke
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_flow, Expression<Func<T, bool>> expression2)
        {

            var invokedExpression = Expression.Invoke(expression2, exp_flow.Parameters.Cast<System.Linq.Expressions.Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.Or(exp_flow.Body, invokedExpression), exp_flow.Parameters);

        }

        public static Expression<Func<T, bool>> AndInvoke<T>(this Expression<Func<T, bool>> exp_flow, Expression<Func<T, bool>> expression2)
        {

            var invokedExpression = Expression.Invoke(expression2, exp_flow.Parameters.Cast<System.Linq.Expressions.Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.And(exp_flow.Body, invokedExpression), exp_flow.Parameters);

        }
        #endregion
        #endregion
    }
}
