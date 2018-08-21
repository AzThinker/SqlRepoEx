// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Atk.AtkExpression
{
    /// <summary>
    ///重写一个表达式树,并将其中引用变量转换成常量
    ///去除所附加的类信息
    /// </summary>
    internal static class AtkPartialEvaluator
    {
        /// <summary>
        /// Performs evaluation And replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression Eval(System.Linq.Expressions.Expression expression)
        {
            return Eval(expression, null);
        }

        /// <summary>
        /// Performs evaluation replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <param name="fnCanBeEvaluated">A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression Eval(System.Linq.Expressions.Expression expression, Func<System.Linq.Expressions.Expression, bool> fnCanBeEvaluated)
        {
            if (fnCanBeEvaluated == null)
                fnCanBeEvaluated = AtkPartialEvaluator.CanBeEvaluatedLocally;
            return SubtreeEvaluator.Eval(Nominator.Nominate(fnCanBeEvaluated, expression), expression);
        }

        private static bool CanBeEvaluatedLocally(System.Linq.Expressions.Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }

        /// <summary>
        /// Evaluates And replaces sub-trees when first candidate is reached (top-down)
        /// </summary>
        class SubtreeEvaluator : ExpressionVisitor
        {
            HashSet<System.Linq.Expressions.Expression> candidates;

            private SubtreeEvaluator(HashSet<System.Linq.Expressions.Expression> candidates)
            {
                this.candidates = candidates;
            }

            internal static Expression Eval(HashSet<System.Linq.Expressions.Expression> candidates, System.Linq.Expressions.Expression exp)
            {
                return new SubtreeEvaluator(candidates).Visit(exp);
            }

            protected override Expression Visit(System.Linq.Expressions.Expression exp)
            {
                if (exp == null)
                {
                    return null;
                }

                if (this.candidates.Contains(exp))
                {
                    return this.Evaluate(exp);
                }

                return base.Visit(exp);
            }

            private Expression Evaluate(System.Linq.Expressions.Expression e)
            {
                Type type = e.Type;

                // check for nullable converts & strip them
                if (e.NodeType == ExpressionType.Convert)
                {
                    var u = (UnaryExpression)e;
                    if (AtkTypeHelper.GetNonNullableType(u.Operand.Type) == AtkTypeHelper.GetNonNullableType(type))
                    {
                        e = ((UnaryExpression)e).Operand;
                    }
                }

                // if we now just have a constant, return it
                if (e.NodeType == ExpressionType.Constant)
                {
                    var ce = (ConstantExpression)e;

                    // if we've lost our nullable typeness add it back
                    if (e.Type != type && AtkTypeHelper.GetNonNullableType(e.Type) == AtkTypeHelper.GetNonNullableType(type))
                    {
                        e = ce = System.Linq.Expressions.Expression.Constant(ce.Value, type);
                    }

                    return e;
                }

                var me = e as MemberExpression;
                if (me != null)
                {
                    // member accesses off of constant's are common, and yet since these partial evals
                    // are never re-used, using reflection to access the member is faster than compiling  
                    // and invoking a lambda
                    var ce = me.Expression as ConstantExpression;
                    if (ce != null)
                    {
                        return System.Linq.Expressions.Expression.Constant(Atk.AtkExpression.AtkReflectionExtensions.GetValue(me.Member, ce.Value), type);
                    }
                }

                if (type.IsValueType)
                {
                    e = System.Linq.Expressions.Expression.Convert(e, typeof(object));
                }

                Expression<Func<object>> lambda = System.Linq.Expressions.Expression.Lambda<Func<object>>(e);
#if NOREFEMIT
                Func<object> fn = ExpressionEvaluator.CreateDelegate(lambda);
#else
                Func<object> fn = lambda.Compile();
#endif
                return System.Linq.Expressions.Expression.Constant(fn(), type);
            }
        }

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        class Nominator : ExpressionVisitor
        {
            Func<System.Linq.Expressions.Expression, bool> fnCanBeEvaluated;
            HashSet<System.Linq.Expressions.Expression> candidates;
            bool cannotBeEvaluated;

            private Nominator(Func<System.Linq.Expressions.Expression, bool> fnCanBeEvaluated)
            {
                this.candidates = new HashSet<System.Linq.Expressions.Expression>();
                this.fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal static HashSet<System.Linq.Expressions.Expression> Nominate(Func<System.Linq.Expressions.Expression, bool> fnCanBeEvaluated, System.Linq.Expressions.Expression expression)
            {
                Nominator nominator = new Nominator(fnCanBeEvaluated);
                nominator.Visit(expression);
                return nominator.candidates;
            }

            protected override Expression VisitConstant(ConstantExpression c)
            {
                return base.VisitConstant(c);
            }

            protected override Expression Visit(System.Linq.Expressions.Expression expression)
            {
                if (expression != null)
                {
                    bool saveCannotBeEvaluated = this.cannotBeEvaluated;
                    this.cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!this.cannotBeEvaluated)
                    {
                        if (this.fnCanBeEvaluated(expression))
                        {
                            this.candidates.Add(expression);
                        }
                        else
                        {
                            this.cannotBeEvaluated = true;
                        }
                    }
                    this.cannotBeEvaluated |= saveCannotBeEvaluated;
                }
                return expression;
            }
        }
    }
}