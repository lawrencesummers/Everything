using System;

internal class Class7<T1, T2> : Class6<T1>
{
    private T2 gparam_1;

    public Class7(T1 item1, T2 item2) : base(item1)
    {
        this.gparam_1 = item2;
    }

    public T2 method_1()
    {
        return this.gparam_1;
    }
}

