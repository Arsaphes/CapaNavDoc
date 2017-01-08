using System;
using System.Collections.Generic;

namespace CapaNavDoc.Classes
{
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _expression;


        public LambdaComparer(Func<T, T, bool> lambda)
        {
            _expression = lambda;
        }


        public bool Equals(T x, T y)
        {
            return _expression(x, y);
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}