using UnityEngine;
using System.Collections;

public class DeviceUtil
{
    public static string DeviceUniqueIdentifier
    {
        get
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }

    public static string DeviceModel
    {
        get
        {
#if UNITY_IPHONE
            return UnityEngine.iOS.Device.generation.ToString();
#else
            return SystemInfo.deviceModel;
#endif
        }
    }
}
