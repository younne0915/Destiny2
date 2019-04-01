using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMgr : SingletonMono<TimeMgr>
{
    private bool m_IsTimeScale = false;
    private float m_TimeScaleEndTime = 0;

    public void SetTimeScale(float scale, float continueTime)
    {
        m_IsTimeScale = true;
        Time.timeScale = scale;
        m_TimeScaleEndTime = Time.realtimeSinceStartup + continueTime;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (m_IsTimeScale)
        {
            if(Time.realtimeSinceStartup > m_TimeScaleEndTime)
            {
                Time.timeScale = 1;
                m_IsTimeScale = false;
            }
        }
    }
}
