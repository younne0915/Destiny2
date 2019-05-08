using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class TimeAction
    {
        public bool IsRunning
        {
            get;
            private set;
        }

        private float m_CurrRunTime;

        private int m_CurrLoop;

        /// <summary>
        /// 循环次数 （-1表示无限循环，0也会循环一次）
        /// </summary>
        private int m_Loop;

        private float m_DelayTime;

        private float m_Interval;

        private Action m_OnStart;

        private Action<int> m_OnUpdate;

        private Action m_OnComplete;

        public TimeAction Init(float delayTime, float interval, int loop, Action onStart, Action<int> onUpdate, Action onComplete)
        {
            m_DelayTime = delayTime;
            m_Interval = interval;
            m_Loop = loop;
            m_OnStart = onStart;
            m_OnUpdate = onUpdate;
            m_OnComplete = onComplete;
            return this;
        }

        public void Run()
        {
            GameEntry.Time.RegisterTimeAction(this);

            m_CurrRunTime = Time.time;
        }

        public void Pause()
        {
            IsRunning = false;
        }

        public void Stop()
        {
            if(m_OnComplete != null)
            {
                m_OnComplete();
            }

            IsRunning = false;
            GameEntry.Time.RemoveAction(this);
        }

        public void OnUpdate()
        {
            if(!IsRunning && Time.time > m_CurrRunTime + m_DelayTime)
            {
                IsRunning = true;
                m_CurrRunTime = Time.time;

                if(m_OnStart != null)
                {
                    m_OnStart();
                }
            }

            if (!IsRunning) return;

            if(Time.time > m_CurrRunTime)
            {
                m_CurrRunTime = Time.time + m_Interval;

                if(m_OnUpdate != null)
                {
                    m_OnUpdate(m_Loop - m_CurrLoop);
                }

                if(m_Loop > -1)
                {
                    m_CurrLoop++;
                    if(m_CurrLoop >= m_Loop)
                    {
                        Stop();
                    }
                }
            }
        }
    }
}
