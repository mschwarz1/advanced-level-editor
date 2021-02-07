﻿using Hertzole.ALE;
using System;
using UnityEngine;

public class EnumTester : MonoBehaviour, IExposedToLevelEditor
{
    public enum TestEnum { Test1, Test3, Consistency, No }

    [SerializeField]
    private TestEnum enumTest = TestEnum.Test3;
    [SerializeField]
    private TestEnum[] enumArrayTest = null;

    public event Action<int, object> OnValueChanged;

    public string ComponentName
    {
        get
        {
            return "EnumTester";
        }
    }

    public string TypeName
    {
        get
        {
            return typeof(EnumTester).FullName;
        }
    }

    public int Order
    {
        get
        {
            return 0;
        }
    }

    public ExposedProperty[] GetProperties()
    {
        return new ExposedProperty[]
        {
            new ExposedEnum(0, typeof(TestEnum), "enumTest", null, true, false),
            new ExposedEnum(1, typeof(TestEnum), "enumArrayTest", null, true, true)
        };
    }

    public object GetValue(int id)
    {
        if (id == 0)
        {
            return enumTest;
        }
        else if (id == 1)
        {
            return enumArrayTest;
        }
        else
        {
            throw new ArgumentException("No with " + id);
        }
    }

    public void SetValue(int id, object value, bool notify)
    {
        bool changed = false;

        if (id == 0)
        {
            if ((TestEnum)value != enumTest)
            {
                enumTest = (TestEnum)value;
                changed = true;
            }
        }
        else if (id == 1)
        {
            TestEnum[] array = Array.ConvertAll((object[])value, (object para0) => (TestEnum)para0);
            if (enumArrayTest != array)
            {
                enumArrayTest = array;
                changed = true;
            }
        }
        else
        {
            throw new ArgumentException("No with " + id);
        }

        if (notify && changed)
        {
            OnValueChanged?.Invoke(id, value);
        }
    }

    public Type GetValueType(int id)
    {
        if (id == 0)
        {
            return typeof(TestEnum);
        }
        else if (id == 1)
        {
            return typeof(TestEnum);
        }
        else
        {
            throw new ArgumentException("No with " + id);
        }
    }
}
