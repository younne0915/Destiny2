using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class EventComponent : YouYouBaseComponent
    {
        private EventManager m_EventManager;

        public SocketEvent socketEvent
        {
            get { return m_EventManager.socketEvent; }
        }

        public CommonEvent commonEvent
        {
            get { return m_EventManager.commonEvent; }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            m_EventManager = new EventManager();
        }

        public override void Shutdown()
        {
            base.Shutdown();
            m_EventManager.Dispose();
        }
    }
}
