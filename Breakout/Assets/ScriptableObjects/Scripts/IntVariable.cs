using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IntVariableObject", fileName = "New IntVariable")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public int Value;

    public event Action<int> RuntimeValueChanged;

    [NonSerialized]
    private int runtimeValue;

    public int RuntimeValue 
    { 
        get
        {
            return runtimeValue;
        }

        set
        {
            if (value != runtimeValue)
            {
                runtimeValue = value;
                RuntimeValueChanged?.Invoke(runtimeValue);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = Value;
    }

    public void OnBeforeSerialize()
    {
    }
}
