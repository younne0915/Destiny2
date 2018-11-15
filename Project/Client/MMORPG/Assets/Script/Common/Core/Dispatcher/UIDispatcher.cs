using UnityEngine;
using System.Collections;

public class UIDispatcher : DispatcherBase<UIDispatcher, object[], string>
{
    public void Dispatch(string key)
    {
        Dispatch(key, null);
    }
}
