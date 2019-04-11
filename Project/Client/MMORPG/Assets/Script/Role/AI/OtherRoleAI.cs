using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRoleAI : IRoleAI
{
    private Vector3 m_TargetPos;
    private long m_ServerTime;
    private long m_NeedTime;

    public RoleCtrl CurrRole
    {
        get;
        set;
    }

    public OtherRoleAI(RoleCtrl roleCtrl)
    {
        CurrRole = roleCtrl;
    }

    public void DoAI()
    {
        
    }

    public void MoveTo(Vector3 targetPos, long serverTime, int needTime)
    {
        m_TargetPos = targetPos;
        m_ServerTime = serverTime;
        m_NeedTime = needTime;

        if(CurrRole!= null && CurrRole.Seeker != null)
        {
            CurrRole.Seeker.StartPath(CurrRole.transform.position, m_TargetPos, p => AstarPathFinish(p));
        }
    }

    private void AstarPathFinish(Path p)
    {
        if (!p.error)
        {
            CurrRole.AStarPath = (ABPath)p;
            if (Vector3.Distance(CurrRole.AStarPath.endPoint, new Vector3(CurrRole.AStarPath.originalEndPoint.x, CurrRole.AStarPath.endPoint.y, CurrRole.AStarPath.originalEndPoint.z)) > 0.5f)
            {
                AppDebug.Log("不能到达目标点");
                CurrRole.AStarPath = null;
            }
            else
            {
                float dis = GameUtil.GetTotalDistance(p.vectorPath);
                long meNeedTime = m_NeedTime + m_ServerTime - GlobalInit.Instance.GetCurrServetTime();
                if (meNeedTime <= 0) meNeedTime = 100;
                CurrRole.ModifySpeed = Mathf.Clamp(dis / (meNeedTime * 0.001f), 0, 20f);
                CurrRole.CurrRoleFSMMgr.ChangeState(RoleState.Run);
            }
            CurrRole.AStarCurrWavePointIndex = 1;
        }
        else
        {
            AppDebug.Log("寻路出错");
            CurrRole.AStarPath = null;
        }
    }
}
