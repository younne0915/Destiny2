//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-01 22:26:02
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    private const string m_Ip = "http://192.168.216.8";

    private static string m_WebAccountUrl = "";
    /// <summary>
    /// 账户服务器地址
    /// </summary>
    public static string WebAccountUrl
    {
        get
        {
            if (string.IsNullOrEmpty(m_WebAccountUrl))
            {
                m_WebAccountUrl = string.Format("{0}:5510/", m_Ip);
            }
            return m_WebAccountUrl;
        }
    }

    private static string m_CDNUrl = "";
    public static string CDNUrl
    {
        get
        {
            if (string.IsNullOrEmpty(m_CDNUrl))
            {
                m_CDNUrl = string.Format("{0}:4250/", m_Ip);
            }
            return m_CDNUrl;
        }
    }

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

    [HideInInspector]
    public RoleInfoMainPlayer MainPlayerInfo;

    public Shader T4MShader;

    public Shader SkyboxShader;

    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    private long m_StartServerTime;

    [HideInInspector]
    public long CurrHttpServerTime
    {
        get
        {
            return m_StartServerTime + (long)RealTime.time;
        }
    }

    [HideInInspector]
    public RetGameServerEntity CurrentSelectGameServer;

    [HideInInspector]
    public RetAccountEntity CurrentAccount;

    [HideInInspector]
    public float PingValue;

    [HideInInspector]
    public long BeganGameServerTime;

    [HideInInspector]
    public float CheckTime;


    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start ()
	{
        NetWorkHttp.Instance.SendData(WebAccountUrl + "api/Time", OnTimeCallback);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GlobalInit.Instance == null) return;
            Transform trans = GlobalInit.Instance.CurrPlayer.transform;
            string pos = string.Format("{0}_{1}_{2}_{3}", trans.position.x, trans.position.y, trans.position.z, trans.rotation.eulerAngles.y);
            AppDebug.Log("位置信息 = " + pos);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (GlobalInit.Instance == null) return;
            SceneManager.LoadScene("TestLua");
        }
    }

    private void OnTimeCallback(RetValue obj)
    {
        if (obj != null && !obj.HasError)
        {
            m_StartServerTime = long.Parse(obj.Value.ToString());
        }
    }

    public long GetCurrServetTime()
    {
        return (long)(Time.realtimeSinceStartup * 1000 - CheckTime) + BeganGameServerTime;
    }
}