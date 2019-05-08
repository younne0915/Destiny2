using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class EventManager : ManagerBase, System.IDisposable
    {
        public SocketEvent socketEvent
        {
            get;
            private set;
        }

        public CommonEvent commonEvent
        {
            get;
            private set;
        }

        public EventManager()
        {
            socketEvent = new SocketEvent();
            commonEvent = new CommonEvent();
        }

        public void Dispose()
        {
        }
    }
}