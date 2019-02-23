//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 22:26:02
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using System;

public class GlobalInit : MonoBehaviour 
{
    public delegate void OnReceiveProtoHandler(ushort protoCode, byte[] buffer);

    //定义委托
    public OnReceiveProtoHandler OnReceiveProto;
    #region 常量
    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";

    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";

    /// <summary>
    /// 账户服务器地址
    /// </summary>
    public const string WebAccountUrl = "http://172.17.128.171:5510/";

    public const string SocketIP = "172.17.128.171";

    public const ushort Port = 1011;

    #endregion

    public static GlobalInit Instance;

    /// <summary>
    /// 玩家注册时候的昵称
    /// </summary>
    [HideInInspector]
    public string CurrRoleNickName;

    /// <summary>
    /// 当前玩家
    /// </summary>
    [HideInInspector]
    public RoleCtrl CurrPlayer;

    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    private long m_StartServerTime;

    [HideInInspector]
    public long CurrServerTime
    {
        get
        {
            return m_StartServerTime + (long)RealTime.time;
        }
    }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start ()
	{
        NetWorkHttp.Instance.SendData(WebAccountUrl + "api/Time", OnTimeCallback);
	}

    private void OnTimeCallback(RetValue obj)
    {
        if (!obj.HasError)
        {
            m_StartServerTime = long.Parse(obj.Value.ToString());
        }
    }
}