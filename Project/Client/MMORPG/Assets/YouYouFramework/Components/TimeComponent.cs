using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class TimeComponent : YouYouBaseComponent, IUpdateComponent
    {
        private TimeManager m_TimeManager;

        protected override void OnAwake()
        {
            base.OnAwake();

            GameEntry.RegisterUpdateComponent(this);
            m_TimeManager = new TimeManager();
        }

        public void RegisterTimeAction(TimeAction action)
        {
            m_TimeManager.RegisterTimeAction(action);
        }

        public void RemoveAction(TimeAction action)
        {
            m_TimeManager.RemoveAction(action);
        }

        public void OnUpdate()
        {
            m_TimeManager.OnUpdate();
        }

        public override void Shutdown()
        {
            m_TimeManager.Dispose();
        }

    }
}