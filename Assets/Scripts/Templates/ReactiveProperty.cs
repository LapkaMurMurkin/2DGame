using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveProperty<T>
{
    public event Action<T> Changed;
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Changed?.Invoke(_value);
        }
    }

    public ReactiveProperty(T initializeValue)
    {
        Value = initializeValue;
    }
}
