namespace TypeUtil
{
    class Inr<T1, T2> : Sum<T1, T2>
    {
        public T2 value;

        public Inr(T2 value)
        {
            this.value = value;
        }
    }
}