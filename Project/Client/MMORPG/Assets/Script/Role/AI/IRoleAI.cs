//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:56:33
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 角色AI接口
/// </summary>
public interface IRoleAI 
{
    /// <summary>
    /// 当前控制的角色
    /// </summary>
    RoleCtrl CurrRole
    {
        get;
        set;
    }

    /// <summary>
    /// 执行AI
    /// </summary>
    void DoAI();
}