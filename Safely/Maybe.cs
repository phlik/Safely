using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safely
{
    public static class Maybe
    {
        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator)
            where TInput : class
        {
            return (o == null) ? null : (evaluator(o) ? o : null);
        }

        public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator)
               where TInput : class
        {
            return (o == null) ? null : evaluator(o) ? null : o;
        }

        public static TInput IfDo<TInput>(this TInput o, Func<TInput, bool> evaluator, Action<TInput> action)
            where TInput : class
        {
            if (o == null) return null;
            if (evaluator(o))
            {
                action(o);
            }
            return o;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        public static IEnumerable<TInput> Each<TInput>(this IEnumerable<TInput> o, Action<TInput> action)
            where TInput : class
        {
            if (o == null) return null;
            o.AsParallel().ForAll(action);
            return o;
        }

        public static TInput Recover<TInput>(this TInput o, Func<TInput> evaluator)
            where TInput : class
        {
            return o ?? evaluator();
        }

        public static TResult Let<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, Func<TResult> failEvaluator = null)
            where TInput : class
        {
            return (o == null) ? (failEvaluator != null ? failEvaluator() : default(TResult)) : evaluator(o);
        }

        public static TResult Let<TKey, TResult>(this IDictionary<TKey, TResult> o, TKey key, Func<TResult> failEvaluator = null)
        {
            return (o == null || !o.ContainsKey(key)) ? (failEvaluator != null ? failEvaluator() : default(TResult)) : o[key];
        }
        
        public static TResult Let<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failValue)
            where TInput : class
        {
            return (o == null) ? failValue : evaluator(o);
        }

        public static TResult Let<TKey, TResult>(this IDictionary<TKey, TResult> o, TKey key, TResult failValue)
        {
            return (o == null || !o.ContainsKey(key)) ? failValue : o[key];
        }
    }
}
