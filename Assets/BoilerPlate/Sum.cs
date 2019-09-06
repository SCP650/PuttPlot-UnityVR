using System;

namespace TypeUtil
{
    public class Sum<T1, T2>
    {
        public T Match<T>(Func<T1,T> l, Func<T2,T> r)
        {
            if (this is Inr<T1, T2>)
            {
                return r(((Inr<T1,T2>)this).value);
            }

            if (this is Inl<T1, T2>)
            {
                return l(((Inl<T1, T2>) this).value);
            }
            
            return default(T);
        }
        
        public static Sum<T1,T2> Inr<T1,T2>(T2 value) {return new Inr<T1, T2>(value);}
    }
}