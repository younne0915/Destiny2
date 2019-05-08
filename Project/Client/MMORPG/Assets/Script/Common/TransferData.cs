using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[XLua.LuaCallCSharp]
public class TransferData
{
	public TransferData()
    {
        m_PutValues = new Dictionary<string, object>();
    }

    private Dictionary<string, object> m_PutValues;
    public Dictionary<string, object> PutValues
    {
        get { return m_PutValues; }
    }

    public void SetValue<TM>(string key, TM value)
    {
        m_PutValues[key] = value;
    }

    public TM GetValue<TM>(string key)
    {
        if (m_PutValues.ContainsKey(key))
        {
            return (TM)m_PutValues[key];
        }
        return default(TM);
    }

    public object GetValue(string key)
    {
        if (m_PutValues.ContainsKey(key))
        {
            return m_PutValues[key];
        }
        return null;
    }
}
