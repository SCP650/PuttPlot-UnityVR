namespace TypeUtil
{
    class Inl<T1, T2> : Sum<T1, T2>
    {
        public T1 value;

        public Inl(T1 value)
        {
            this.value = value;
        }
    }
}